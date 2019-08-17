using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
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

            var dbContext = GetDbContext(logger, DefaultConnectionName);

            using (var unitOfWork = new UnitOfWork(dbContext))
            {
                var provider = Startup.GetServiceProvider(unitOfWork, logger);
                var hosts = Startup.GetHosts(provider);

                HostStartup.StartHosts(hosts, logger);

                logger.Info("Press ENTER to close server...");
                Console.ReadLine();

                HostStartup.CloseHosts(hosts, logger);
            }
        }

        private static ApplicationDbContext GetDbContext(Log4NetLogger logger, string nameOrConnectionString)
        {
            logger.Info("Starting up database...");
            var dbContext = new ApplicationDbContext(nameOrConnectionString);
            dbContext.Database.CreateIfNotExists();
            return dbContext;
        }
    }
}