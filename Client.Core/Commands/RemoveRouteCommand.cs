using System.Threading.Tasks;

namespace Client.Core
{
    public class RemoveRouteCommand : IUndoableCommand
    {
        private readonly IRouteService _routeService;
        private RouteModel _removedRoute;
        private int _routeKey;

        public RemoveRouteCommand(IRouteService routeService, int routeKey)
        {
            _routeService = routeService;
            _routeKey = routeKey;
        }

        public async Task ExecuteAsync()
        {
            _removedRoute = await _routeService.GetRouteAsync(_routeKey);
            await _routeService.RemoveRouteAsync(_routeKey);
        }

        public async Task UnExecuteAsync()
        {
            _routeKey = await _routeService.AddRouteAsync(_removedRoute);
        }
    }
}