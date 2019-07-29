using System.ServiceModel;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IAuthService
    {
        [OperationContract]
        bool IsLoggedIn(string username);

        [OperationContract]
        string Login(string username, string password);

        [OperationContract]
        void Logout(string username);
    }
}