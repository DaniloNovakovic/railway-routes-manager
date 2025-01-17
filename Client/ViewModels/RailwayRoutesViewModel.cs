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
    public class RailwayRoutesViewModel : ViewModelBase
    {
        private readonly ICommandManager _commandManager;
        private readonly IRouteService _routeService;
        private readonly IRailwayStationService _stationService;
        private BindableBase _formViewModel;
        private bool _isDialogOpen;

        public RailwayRoutesViewModel(
            IRouteService routeService,
            IRailwayStationService stationService,
            ICommandManager commandManager,
            ILogger logger,
            IEventAggregator eventAggregator) : base(logger, eventAggregator)
        {
            _routeService = routeService;
            _stationService = stationService;
            _commandManager = commandManager;
            _commandManager.CommandExecuted += OnCommandExecuted;

            Routes = new ObservableCollection<RouteModel>();
            AddCommand = new DelegateCommand(ShowAddRouteForm);
            DuplicateRouteCommand = new DelegateCommand<RouteModel>(async (route) => await DuplicateRouteAsync(route));
            EditRouteCommand = new DelegateCommand<RouteModel>(ShowEditRouteForm);
            RemoveRouteCommand = new DelegateCommand<RouteModel>(async (route) => await RemoveRouteAsync(route));

            RefreshCommand = new DelegateCommand(async () => await RefreshRoutesAsync());
            UndoCommand = new DelegateCommand(async () => await UndoAsync());
            RedoCommand = new DelegateCommand(async () => await RedoAsync());
        }

        public ICommand AddCommand { get; }

        public bool CanRedo => _commandManager.CanRedo();

        public bool CanUndo => _commandManager.CanUndo();

        public ICommand DuplicateRouteCommand { get; }

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

        public ICommand RedoCommand { get; }

        public ICommand RefreshCommand { get; }

        public ICommand RemoveRouteCommand { get; }

        public ObservableCollection<RouteModel> Routes { get; set; }

        public ICommand UndoCommand { get; }

        public Task DuplicateRouteAsync(RouteModel route)
        {
            return SafeExecuteAsync(async () =>
            {
                var command = new AddRouteCommand(_routeService, route);
                await _commandManager.ExecuteAsync(command);
                await RefreshRoutesAsync();
            });
        }

        public override Task OnLoadedAsync()
        {
            UpdateCanUndoRedo();

            return RefreshRoutesAsync();
        }

        public Task RedoAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                await _commandManager.RedoAsync();
                await RefreshRoutesAsync();
            });
        }

        public Task RefreshRoutesAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                var routes = await _routeService.GetAllRoutesAsync();
                Routes.Clear();
                Routes.AddRange(routes);
            });
        }

        public Task RemoveRouteAsync(RouteModel route)
        {
            return SafeExecuteAsync(async () =>
            {
                var command = new RemoveRouteCommand(_routeService, route);
                await _commandManager.ExecuteAsync(command);
                await RefreshRoutesAsync();
            });
        }

        public Task UndoAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                await _commandManager.UndoAsync();
                await RefreshRoutesAsync();
            });
        }

        private void OnCommandExecuted(object sender, System.EventArgs e)
        {
            UpdateCanUndoRedo();
        }

        private async void OnRouteSubmited()
        {
            IsDialogOpen = false;
            await RefreshRoutesAsync();
        }

        private void ShowAddRouteForm()
        {
            FormViewModel = new AddRouteFormViewModel(_routeService, _stationService, _commandManager, Logger, OnRouteSubmited, EventAggregator);
            IsDialogOpen = true;
        }

        private void ShowEditRouteForm(RouteModel route)
        {
            var routeCopy = route.Clone() as RouteModel ?? new RouteModel();
            FormViewModel = new EditRouteFormViewModel(_routeService, _stationService, _commandManager, Logger, routeCopy, OnRouteSubmited, EventAggregator);
            IsDialogOpen = true;
        }

        private void UpdateCanUndoRedo()
        {
            RaisePropertyChanged(nameof(CanUndo));
            RaisePropertyChanged(nameof(CanRedo));
        }
    }
}