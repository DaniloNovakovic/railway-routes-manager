using System.Threading.Tasks;

namespace Client.Core.Commands
{
    public class EditRouteCommand : IUndoableCommand
    {
        private readonly RouteModel _newRoute;
        private readonly IRouteService _routeService;
        private RouteModel _oldRoute;

        public EditRouteCommand(IRouteService routeService, RouteModel newRoute)
        {
            _routeService = routeService;
            _newRoute = newRoute;
        }

        public async Task ExecuteAsync()
        {
            _oldRoute = await _routeService.GetRouteAsync(_newRoute.Id);
            await _routeService.UpdateRouteAsync(_newRoute);
        }

        public Task UnExecuteAsync()
        {
            return _routeService.UpdateRouteAsync(_oldRoute);
        }
    }
}