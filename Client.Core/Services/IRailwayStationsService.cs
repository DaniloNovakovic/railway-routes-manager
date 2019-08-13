using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface IRailwayStationService
    {
        Task<IEnumerable<RailwayStationModel>> GetAllStationsAsync();
    }
}