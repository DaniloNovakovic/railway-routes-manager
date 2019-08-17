using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface IRouteService
    {
        /// <summary>
        /// Returns Key of the added entity
        /// </summary>
        Task<int> AddRouteAsync(RouteModel route);

        Task<IEnumerable<RouteModel>> GetAllRoutesAsync();

        Task<RouteModel> GetRouteAsync(int key);

        Task RemoveRouteAsync(int key);

        Task UpdateRouteAsync(RouteModel route);

        Task ResurrectAsync(RouteModel route);
    }
}