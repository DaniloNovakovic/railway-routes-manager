using System.ServiceModel;
using Common;
using Server.Core;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsLoggedIn(string username)
        {
            return true; // ?
        }

        public string Login(string username, string password)
        {
            var user = _unitOfWork.Users.Get(u => u.Username == username && u.Password == password);
            // ?
            return user.RoleName;
        }

        public void Logout(string username)
        {
            // ?
        }
    }
}