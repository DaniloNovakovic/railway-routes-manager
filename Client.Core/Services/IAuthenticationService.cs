namespace Client.Core
{
    public interface IAuthenticationService
    {
        bool IsLoggedIn(string username);

        string Login(string username, string password);

        void Logout(string username);
    }
}