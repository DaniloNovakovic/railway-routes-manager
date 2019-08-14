using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface IRouteService
    {
        Task AddRouteAsync(RouteModel route);

        Task<IEnumerable<RouteModel>> GetAllRoutesAsync();

        Task RemoveRouteAsync(int key);
    }
}