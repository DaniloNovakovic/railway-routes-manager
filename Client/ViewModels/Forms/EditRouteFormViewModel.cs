using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class EditRouteFormViewModel : RouteFormViewModel
    {
        private readonly Action _onRouteUpdated;
        private readonly IRouteService _routeService;

        public EditRouteFormViewModel(IRouteService routeService, IRailwayStationService stationService, RouteModel route, Action onRouteUpdated = null) : base(stationService, route)
        {
            _routeService = routeService;
            _onRouteUpdated = onRouteUpdated;
        }

        public override Task OnSubmitAsync()
        {
            return SafeExecuteAsync(
                @try: async () =>
                {
                    CanSubmit = false;
                    await _routeService.UpdateRouteAsync(RouteModel);
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