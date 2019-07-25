using System;
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
                var host = hostFactory.GetServiceHost<IUserService>(Ports.UserServicePort, userService);
                host.Open();

                Console.WriteLine("Press ENTER to close server...");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}