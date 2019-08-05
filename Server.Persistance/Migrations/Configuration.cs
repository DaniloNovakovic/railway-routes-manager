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

            // Seed Users
            context.Users.AddOrUpdate(new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin",
                FirstName = "Danilo",
                LastName = "Novakovic",
                RoleName = RoleNames.Admin
            });

            // Seed Countries
            context.Countries.AddOrUpdate(new[]
            {
                new Country{Id = 1, Code = "me", Name = "Montenegro"},
                new Country{Id = 2, Code = "rs", Name = "Serbia"},
                new Country{Id = 3, Code = "hr", Name = "Croatia"},
                new Country{Id = 4, Code = "bg", Name = "Bulgaria"},
                new Country{Id = 5, Code = "hu", Name = "Hungary"},
                new Country{Id = 6, Code = "at", Name = "Austria"},
                new Country{Id = 7, Code = "sk", Name = "Slovakia"},
                new Country{Id = 8, Code = "si", Name = "Slovenia"},
            });

            // Seed Locations
            context.Locations.AddOrUpdate(new[]
            {
                new Location(){Id = 1, CountryId = 1, Name = "Podgorica"},
                new Location(){Id = 2, CountryId = 1, Name = "Cetinje"},
                new Location(){Id = 3, CountryId = 1, Name = "Bar"},
                new Location(){Id = 4, CountryId = 2, Name = "Vrbas"},
                new Location(){Id = 5, CountryId = 2, Name = "Novi Sad"},
                new Location(){Id = 6, CountryId = 2, Name = "Beograd"},
                new Location(){Id = 7, CountryId = 2, Name = "Subotica"},
                new Location(){Id = 8, CountryId = 3, Name = "Zagreb"},
                new Location(){Id = 9, CountryId = 3, Name = "Split"},
                new Location(){Id = 10, CountryId = 3, Name = "Rijeka"},
                new Location(){Id = 11, CountryId = 4, Name = "Asenovgrad"},
                new Location(){Id = 12, CountryId = 4, Name = "Dimitrovgrad"},
                new Location(){Id = 13, CountryId = 5, Name = "Budimpesta"},
                new Location(){Id = 14, CountryId = 5, Name = "Debrecen"},
            });
        }
    }
}