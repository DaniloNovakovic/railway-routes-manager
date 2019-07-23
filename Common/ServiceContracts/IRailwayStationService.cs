using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IRailwayStationService : IServiceBase<int, RailwayStationDto>
    {
    }
}