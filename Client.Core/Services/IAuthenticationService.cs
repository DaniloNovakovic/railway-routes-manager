using System.Threading.Tasks;

namespace Client.Core
{
    public interface IAuthenticationService
    {
        Task<bool> IsLoggedInAsync(string username);

        Task<string> LoginAsync(string username, string password);

        Task LogoutAsync();
    }
}