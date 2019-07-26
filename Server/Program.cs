using System;
using System.Threading.Tasks;
using Common;
using Server.Core;
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

                var provider = RegisterServices(unitOfWork);
                var userServiceHost = hostFactory.GetServiceHost<IUserService>(Ports.UserServicePort, provider.Resolve<IUserService>());
                var authServiceHost = hostFactory.GetServiceHost<IAuthService>(Ports.AuthServicePort, provider.Resolve<IAuthService>());

                Task.Run(() => userServiceHost.Open());
                Task.Run(() => authServiceHost.Open());

                Console.WriteLine("Press ENTER to close server...");
                Console.ReadLine();

                Task.Run(() => userServiceHost.Close());
                Task.Run(() => authServiceHost.Close());
            }
        }

        private static IServiceProvider RegisterServices(IUnitOfWork unitOfWork)
        {
            var provider = new ServiceProvider();
            provider.Register<IUserService>(new UserService(unitOfWork));
            provider.Register<IAuthService>(new AuthService(unitOfWork));
            return provider;
        }
    }
}