using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class AddRouteFormViewModel : RouteFormViewModel
    {
        private readonly Action _onRouteAdded;
        private readonly IRouteService _routeService;

        public AddRouteFormViewModel(IRouteService routeService, IRailwayStationService stationService, Action onRouteAdded = null) : base(stationService)
        {
            _routeService = routeService;
            _onRouteAdded = onRouteAdded;
        }

        public override Task OnSubmitAsync()
        {
            return SafeExecuteAsync(
                @try: async () =>
                {
                    CanSubmit = false;
                    await _routeService.AddRouteAsync(RouteModel);
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