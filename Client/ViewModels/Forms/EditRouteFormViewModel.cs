using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class EditRouteFormViewModel : RouteFormViewModel
    {
        private readonly ICommandManager _commandManager;
        private readonly Action _onRouteUpdated;
        private readonly IRouteService _routeService;

        public EditRouteFormViewModel(
            IRouteService routeService,
            IRailwayStationService stationService,
            ICommandManager commandManager,
            ILogger logger,
            RouteModel route,
            Action onRouteUpdated = null) : base(stationService, logger, route)
        {
            _routeService = routeService;
            _onRouteUpdated = onRouteUpdated;
            _commandManager = commandManager;
        }

        public override Task OnSubmitAsync()
        {
            return SafeExecuteAsync(
                @try: async () =>
                {
                    CanSubmit = false;
                    var command = new EditRouteCommand(_routeService, RouteModel);
                    await _commandManager.ExecuteAsync(command);
                    OnRouteUpdated();
                },
                @finally: UpdateCanSubmit);
        }

        private void OnRouteUpdated()
        {
            _onRouteUpdated?.Invoke();
        }
    }
}