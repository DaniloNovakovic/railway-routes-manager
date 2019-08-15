﻿using System.Threading.Tasks;

namespace Client.Core
{
    public class AddRouteCommand : IUndoableCommand
    {
        private readonly RouteModel _routeModel;
        private readonly IRouteService _routeService;

        public AddRouteCommand(IRouteService routeService, RouteModel routeModel)
        {
            _routeService = routeService;
            _routeModel = routeModel;
        }

        public Task ExecuteAsync()
        {
            return _routeService.AddRouteAsync(_routeModel);
        }

        public Task UnExecuteAsync()
        {
            return _routeService.RemoveRouteAsync(_routeModel.Id);
        }
    }
}