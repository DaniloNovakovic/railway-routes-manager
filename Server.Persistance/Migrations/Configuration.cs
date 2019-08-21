namespace Server.Persistance.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // This method will be called after migrating to the latest version.

            new ApplicationDbBootstrapper(context).Seed();
        }
    }
}