namespace Client.Core
{
    public interface IAuthenticationService
    {
        bool IsLoggedIn(string username);

        void Login(string username, string password);

        void Logout(string username);
    }
}