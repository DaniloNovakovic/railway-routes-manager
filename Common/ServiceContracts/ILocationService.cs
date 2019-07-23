using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface ILocationService : IServiceBase<int, LocationDto>
    {
    }
}