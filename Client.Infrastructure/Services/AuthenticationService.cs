using System.Diagnostics;
using System.Threading.Tasks;
using Client.Core;
using Common;

namespace Client.Infrastructure
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthChannelFactory _factory;
        private readonly ILogger _logger;

        public AuthenticationService(IAuthChannelFactory factory, ILogger logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public Task<bool> IsLoggedInAsync(string username)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                return proxy.IsLoggedIn(username);
            });
        }

        public Task<string> LoginAsync(string username, string password)
        {
            _factory.Username = username;
            _factory.Password = password;

            return Task.Run(() =>
            {
                var proxy = GetProxy();
                string roleName = proxy.Login(username, password);
                _logger.Info($"{username}'s role: {roleName}");
                return roleName;
            });
        }

        public Task LogoutAsync()
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                proxy.Logout(_factory.Username);
                _factory.Username = "";
                _factory.Password = "";
            });
        }

        private IAuthService GetProxy()
        {
            return _factory.GetChannelFactory<IAuthService>(Ports.AuthServicePort).CreateChannel();
        }
    }
}