namespace Server.Persistance.Migrations
{
    using System.Data.Entity.Migrations;
    using Common;
    using Server.Core;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method to avoid creating
            // duplicate seed data.

            context.Users.AddOrUpdate(new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin",
                FirstName = "Danilo",
                LastName = "Novakovic",
                RoleName = RoleNames.Admin
            });
        }
    }
}