using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface IRailwayStationService
    {
        Task<int> AddStationAsync(RailwayStationModel station);

        Task<IEnumerable<RailwayStationModel>> GetAllStationsAsync();

        Task<RailwayStationModel> GetStationAsync(int key);

        Task RemoveStationAsync(int key);

        Task UpdateStationAsync(RailwayStationModel station);
    }
}