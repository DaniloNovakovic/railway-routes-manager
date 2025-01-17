﻿using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;
using Prism.Events;

namespace Client.ViewModels
{
    public class AddRouteFormViewModel : RouteFormViewModelBase
    {
        private readonly ICommandManager _commandManager;
        private readonly Action _onRouteAdded;
        private readonly IRouteService _routeService;

        public AddRouteFormViewModel(
            IRouteService routeService,
            IRailwayStationService stationService,
            ICommandManager commandManager,
            ILogger logger,
            Action onRouteAdded = null,
            IEventAggregator eventAggregator = null) : base(stationService, logger, eventAggregator)
        {
            _routeService = routeService;
            _onRouteAdded = onRouteAdded;
            _commandManager = commandManager;
        }

        public override Task OnSubmitAsync()
        {
            return SafeExecuteAsync(
                @try: async () =>
                {
                    CanSubmit = false;
                    var command = new AddRouteCommand(_routeService, RouteModel);
                    await _commandManager.ExecuteAsync(command);
                    OnRouteAdded();
                },
                @finally: UpdateCanSubmit);
        }

        private void OnRouteAdded()
        {
            _onRouteAdded?.Invoke();
        }
    }
}