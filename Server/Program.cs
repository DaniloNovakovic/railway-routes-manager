using System;
using System.Threading.Tasks;
using Common;
using Server.Persistance;

namespace Server
{
    internal static class Program
    {
        private const string DefaultConnectionName = "DefaultConnection";

        private static void Main()
        {
            var dbContext = new ApplicationDbContext(DefaultConnectionName);
            using (var unitOfWork = new UnitOfWork(dbContext))
            {
                var validator = new CustomUserNamePasswordValidator(unitOfWork);
                var hostFactory = new AuthServiceHostFactory(validator);

                var userService = new UserService(unitOfWork);
                var authService = new AuthService(unitOfWork);
                var userServiceHost = hostFactory.GetServiceHost<IUserService>(Ports.UserServicePort, userService);
                var authServiceHost = hostFactory.GetServiceHost<IAuthService>(Ports.AuthServicePort, authService);

                Task.Run(() => userServiceHost.Open());
                Task.Run(() => authServiceHost.Open());

                Console.WriteLine("Press ENTER to close server...");
                Console.ReadLine();

                Task.Run(() => userServiceHost.Close());
                Task.Run(() => authServiceHost.Close());
            }
        }
    }
}