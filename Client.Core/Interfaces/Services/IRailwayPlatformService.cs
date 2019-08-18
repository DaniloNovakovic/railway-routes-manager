using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface IRailwayPlatformService
    {
        Task<int> AddPlatformAsync(RailwayPlatformModel platform);

        Task<IEnumerable<RailwayPlatformModel>> GetAllPlatformsAsync();

        Task<RailwayPlatformModel> GetPlatformAsync(int key);

        Task RemovePlatformAsync(int key);

        Task UpdatePlatformAsync(RailwayPlatformModel platform);
    }
}