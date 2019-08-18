using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface IRailwayPlatformService
    {
        Task<IEnumerable<RailwayPlatformModel>> GetAllPlatformsAsync();
    }
}