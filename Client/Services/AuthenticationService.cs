using System;

namespace Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool IsLoggedIn(string username)
        {
            return false;
        }

        public void Login(string username, string password)
        {
            Console.WriteLine($"Login: {username} {password}");
        }

        public void Logout(string username)
        {
            Console.WriteLine($"Logout: {username}");
        }
    }
}