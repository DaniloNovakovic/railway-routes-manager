using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IRouteService : IServiceBase<int, RouteDto>, IResurrectable<int>
    {
    }
}