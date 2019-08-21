using System;
using System.Security.Principal;
using Server.Core;
using Server.Persistance;

namespace Server
{
    public static class Program
    {
        private const string DefaultConnectionName = "DefaultConnection";

        public static void Main()
        {
            var logger = new Log4NetLogger();

            logger.Info($"Running as {WindowsIdentity.GetCurrent().Name}");

            var dbContext = GetDbContext(DefaultConnectionName, logger);

            using (var unitOfWork = new UnitOfWork(dbContext))
            {
                var provider = Startup.GetServiceProvider(unitOfWork, logger);
                var hosts = Startup.GetHosts(provider);

                HostStartup.StartHosts(hosts, logger);

                Console.WriteLine("Press ENTER to close server...");
                Console.ReadLine();

                HostStartup.CloseHosts(hosts, logger);
            }
        }

        private static ApplicationDbContext GetDbContext(string nameOrConnectionString, ILogger logger)
        {
            logger.Info("Starting up database...");

            var dbContext = new ApplicationDbContext(nameOrConnectionString);
            var bootstrapper = new ApplicationDbBootstrapper(dbContext, logger);
            bootstrapper.Initialize();

            logger.Info("Database started");

            return dbContext;
        }
    }
}