using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class RailwayStationsViewModel : ViewModelBase
    {
        private readonly IRailwayStationService _stationService;
        private BindableBase _formViewModel;
        private bool _isDialogOpen;

        public RailwayStationsViewModel(
            IRailwayStationService stationService,
            ILogger logger) : base(logger)
        {
            _stationService = stationService;

            RailwayStations = new ObservableCollection<RailwayStationModel>();
            AddCommand = new DelegateCommand(ShowAddStationForm);
            EditRouteCommand = new DelegateCommand<RailwayStationModel>(ShowEditStationForm);
            RefreshCommand = new DelegateCommand(async () => await RefreshStationsAsync());
            RemoveRouteCommand = new DelegateCommand<RailwayStationModel>(async (route) => await RemoveStationAsync(route));
        }

        public ICommand AddCommand { get; }

        public ICommand EditRouteCommand { get; }

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

        public ICommand RemoveRouteCommand { get; }

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

        public Task RemoveStationAsync(RailwayStationModel station)
        {
            return SafeExecuteAsync(async () =>
            {
                // todo...
                await RefreshStationsAsync();
            });
        }

        private async void OnStationSubmited()
        {
            IsDialogOpen = false;
            await RefreshStationsAsync();
        }

        private void ShowAddStationForm()
        {
            // TODO...
            IsDialogOpen = true;
        }

        private void ShowEditStationForm(RailwayStationModel station)
        {
            // TODO...
            IsDialogOpen = true;
        }
    }
}