﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class RailwayStationsViewModel : ViewModelBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger _logger;
        private readonly IRailwayPlatformService _platformService;
        private readonly IRailwayStationService _stationService;
        private BindableBase _formViewModel;
        private bool _isDialogOpen;

        public RailwayStationsViewModel(
            IRailwayStationService stationService,
            IRailwayPlatformService platformService,
            ILocationService locationService,
            ILogger logger,
            IEventAggregator eventAggregator) : base(logger, eventAggregator)
        {
            _stationService = stationService;
            _platformService = platformService;
            _locationService = locationService;
            _logger = logger;

            RailwayStations = new ObservableCollection<RailwayStationModel>();

            // Station commands
            AddCommand = new DelegateCommand(ShowAddStationForm);
            EditStationCommand = new DelegateCommand<RailwayStationModel>(ShowEditStationForm);
            RemoveStationCommand = new DelegateCommand<RailwayStationModel>(async (route) => await RemoveStationAsync(route));

            RefreshCommand = new DelegateCommand(async () => await RefreshStationsAsync());

            // Platform commands
            AddPlatformCommand = new DelegateCommand<RailwayStationModel>(ShowAddPlatformForm);
            EditPlatformCommand = new DelegateCommand<RailwayPlatformModel>(ShowEditPlatformForm);
            RemovePlatformCommand = new DelegateCommand<RailwayPlatformModel>(async (platform) => await RemovePlatformAsync(platform));
        }

        public ICommand AddCommand { get; }

        public ICommand AddPlatformCommand { get; }

        public ICommand EditPlatformCommand { get; }

        public ICommand EditStationCommand { get; }

        public BindableBase FormViewModel
        {
            get { return _formViewModel; }
            set { SetProperty(ref _formViewModel, value); }
        }

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set { SetProperty(ref _isDialogOpen, value); }
        }

        public ObservableCollection<RailwayStationModel> RailwayStations { get; set; }

        public ICommand RefreshCommand { get; }

        public ICommand RemovePlatformCommand { get; }

        public ICommand RemoveStationCommand { get; }

        public override Task OnLoadedAsync()
        {
            return RefreshStationsAsync();
        }

        public Task RefreshStationsAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                var routes = await _stationService.GetAllStationsAsync();
                RailwayStations.Clear();
                RailwayStations.AddRange(routes);
            });
        }

        public Task RemovePlatformAsync(RailwayPlatformModel platform)
        {
            return SafeExecuteAsync(async () =>
            {
                await _platformService.RemovePlatformAsync(platform.Id);
                await RefreshStationsAsync();
            });
        }

        public Task RemoveStationAsync(RailwayStationModel station)
        {
            return SafeExecuteAsync(async () =>
            {
                await _stationService.RemoveStationAsync(station.Id);
                await RefreshStationsAsync();
            });
        }

        private async void OnFormSubmitted()
        {
            IsDialogOpen = false;
            await RefreshStationsAsync();
        }

        private void ShowAddPlatformForm(RailwayStationModel station)
        {
            var platform = new RailwayPlatformModel() { RailwayStationId = station.Id };
            FormViewModel = new AddPlatformFormViewModel(_platformService, _logger, platform, OnFormSubmitted, EventAggregator);
            IsDialogOpen = true;
        }

        private void ShowAddStationForm()
        {
            FormViewModel = new AddStationFormViewModel(_stationService, _locationService, _logger, OnFormSubmitted, EventAggregator);
            IsDialogOpen = true;
        }

        private void ShowEditPlatformForm(RailwayPlatformModel platform)
        {
            FormViewModel = new EditPlatformFormViewModel(_platformService, _logger, platform, OnFormSubmitted, EventAggregator);
            IsDialogOpen = true;
        }

        private void ShowEditStationForm(RailwayStationModel station)
        {
            FormViewModel = new EditStationFormViewModel(_stationService, _locationService, _logger, station, OnFormSubmitted, EventAggregator);
            IsDialogOpen = true;
        }
    }
}