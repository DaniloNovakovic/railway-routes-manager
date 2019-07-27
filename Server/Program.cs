using System;
using System.Threading.Tasks;
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
                var provider = Startup.GetServiceProvider(unitOfWork);
                var hosts = Startup.GetHosts(provider);

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
    }
}