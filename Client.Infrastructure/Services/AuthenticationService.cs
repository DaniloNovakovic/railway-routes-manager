using System;
using Client.Core;
using Client.Infrastructure.Helpers;
using Common;

namespace Client.Infrastructure
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthChannelFactory _factory;

        public AuthenticationService(IAuthChannelFactory factory)
        {
            _factory = factory;
        }

        public bool IsLoggedIn(string username)
        {
            return false;
        }

        public void Login(string username, string password)
        {
            _factory.Username = username;
            _factory.Password = password;
            var channelFactory = _factory.GetChannelFactory<IUserService>(Ports.UserServicePort);
            var proxy = channelFactory.CreateChannel();

            proxy.Login(username, password);
        }

        public void Logout(string username)
        {
            Console.WriteLine($"Logout: {username}");
        }
    }
}