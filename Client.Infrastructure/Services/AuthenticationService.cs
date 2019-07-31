using System.Diagnostics;
using System.Threading.Tasks;
using Client.Core;
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

        public Task<bool> IsLoggedInAsync(string username)
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<IAuthService>(Ports.AuthServicePort).CreateChannel();
                return proxy.IsLoggedIn(username);
            });
        }

        public Task<string> LoginAsync(string username, string password)
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

        public Task LogoutAsync()
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<IAuthService>(Ports.AuthServicePort).CreateChannel();
                proxy.Logout(_factory.Username);
                _factory.Username = "";
                _factory.Password = "";
            });
        }
    }
}