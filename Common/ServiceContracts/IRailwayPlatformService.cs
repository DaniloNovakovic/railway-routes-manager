using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IRailwayPlatformService : IServiceBase<int, RailwayPlatformDto>
    {
    }
}