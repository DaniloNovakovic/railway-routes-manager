﻿using System.Diagnostics;
using System.Threading.Tasks;
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

        public Task<bool> IsLoggedIn(string username)
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<IAuthService>(Ports.AuthServicePort).CreateChannel();
                return proxy.IsLoggedIn(username);
            });
        }

        public Task<string> Login(string username, string password)
        {
            _factory.Username = username;
            _factory.Password = password;
            return Task.Run(() =>
            {
                var channelFactory = _factory.GetChannelFactory<IAuthService>(Ports.AuthServicePort);
                var proxy = channelFactory.CreateChannel();

                string roleName = proxy.Login(username, password);
                Trace.TraceInformation($"{username}'s role: {roleName}");
                return roleName;
            });
        }

        public Task Logout(string username)
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<IAuthService>(Ports.AuthServicePort).CreateChannel();
                proxy.Logout(username);
            });
        }
    }
}