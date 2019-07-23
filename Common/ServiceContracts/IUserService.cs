using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IUserService : IServiceBase<int, UserDto>
    {
    }
}