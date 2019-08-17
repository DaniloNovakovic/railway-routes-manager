using System.ServiceModel;
using Common;
using Server.Core;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AuthService : IAuthService
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public bool IsLoggedIn(string username)
        {
            _logger.Debug($"{username} is logged in");
            return true;
        }

        public string Login(string username, string password)
        {
            var user = _unitOfWork.Users.Get(u => u.Username == username && u.Password == password);
            _logger.Debug($"{username}'s role: {user.RoleName}");
            return user.RoleName;
        }

        public void Logout(string username)
        {
            _logger.Debug($"{username} logged out!");
        }
    }
}