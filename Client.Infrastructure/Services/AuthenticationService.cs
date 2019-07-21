using System;
using Client.Core;

namespace Client.Infrastructure
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool IsLoggedIn(string username)
        {
            return false;
        }

        public void Login(string username, string password)
        {
            if (username != "admin" && password != "admin")
            {
                throw new ArgumentException("User not found!");
            }

            Console.WriteLine($"Login: {username} {password}");
        }

        public void Logout(string username)
        {
            Console.WriteLine($"Logout: {username}");
        }
    }
}