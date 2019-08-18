using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationModel>> GetAllLocationsAsync();
    }
}