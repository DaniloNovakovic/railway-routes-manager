using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Common;
using Server.Core;
using Server.Persistance;

namespace Server
{
    public static class Program
    {
        private const string DefaultConnectionName = "DefaultConnection";

        public static void Main()
        {
            var dbContext = new ApplicationDbContext(DefaultConnectionName);

            using (var unitOfWork = new UnitOfWork(dbContext))
            {
                var provider = RegisterServices(unitOfWork);
                var hosts = GetHosts(provider);

                foreach (var host in hosts)
                {
                    Task.Run(() => host.Open());
                }

                Console.WriteLine("Press ENTER to close server...");
                Console.ReadLine();

                foreach (var host in hosts)
                {
                    Task.Run(() => host.Close());
                }
            }
        }

        private static IEnumerable<ICommunicationObject> GetHosts(IServiceProvider provider)
        {
            var hostFactory = provider.Resolve<IAuthServiceHostFactory>();

            return new List<ICommunicationObject>
            {
                hostFactory.GetServiceHost<IUserService>(Ports.UserServicePort, provider.Resolve<IUserService>()),
                hostFactory.GetServiceHost<IAuthService>(Ports.AuthServicePort, provider.Resolve<IAuthService>())
            };
        }

        private static IServiceProvider RegisterServices(IUnitOfWork unitOfWork)
        {
            var provider = new ServiceProvider();

            var validator = new CustomUserNamePasswordValidator(unitOfWork);

            provider.Register<IAuthServiceHostFactory>(new AuthServiceHostFactory(validator));
            provider.Register<IUserService>(new UserService(unitOfWork));
            provider.Register<IAuthService>(new AuthService(unitOfWork));

            return provider;
        }
    }
}