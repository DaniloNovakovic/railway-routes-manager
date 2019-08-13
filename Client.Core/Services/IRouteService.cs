using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface IRouteService
    {
        Task<IEnumerable<RouteModel>> GetAllRoutesAsync();

        Task AddRouteAsync(RouteModel route);
    }
}