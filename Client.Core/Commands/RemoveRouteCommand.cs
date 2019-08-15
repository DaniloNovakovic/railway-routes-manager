using System.Threading.Tasks;

namespace Client.Core.Commands
{
    public class RemoveRouteCommand : IUndoableCommand
    {
        private readonly RouteModel _route;
        private readonly IRouteService _routeService;

        public RemoveRouteCommand(IRouteService routeService, RouteModel route)
        {
            _routeService = routeService;
            _route = route;
        }

        public Task ExecuteAsync()
        {
            return _routeService.RemoveRouteAsync(_route.Id);
        }

        public Task UnExecuteAsync()
        {
            return _routeService.AddRouteAsync(_route);
        }
    }
}