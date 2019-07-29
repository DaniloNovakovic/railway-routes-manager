using System.Threading.Tasks;

namespace Client.Core
{
    public interface IAuthenticationService
    {
        Task<bool> IsLoggedIn(string username);

        Task<string> Login(string username, string password);

        Task Logout();
    }
}