using System.Data.Entity.Migrations;
using System.Linq;
using Common;
using Server.Core;

namespace Server.Persistance
{
    public class ApplicationDbBootstrapper
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ApplicationDbBootstrapper(ApplicationDbContext context, ILogger logger = null)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Creates Database if not exist and seeds data if needed
        /// </summary>
        public void Initialize()
        {
            _context.Database.CreateIfNotExists();
            if (!_context.Users.Any())
            {
                _logger?.Info("Seeding data...");
                Seed();
                _context.SaveChanges();
                _logger?.Info("Finished seeding data.");
            }
        }

        /// <summary>
        /// Seeds the data without calling .SaveChanges
        /// </summary>
        public void Seed()
        {
            _logger?.Debug("Seeding users...");

            _context.Users.AddOrUpdate(new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin",
                FirstName = "Danilo",
                LastName = "Novakovic",
                RoleName = RoleNames.Admin
            }, new User
            {
                Id = 2,
                Username = "user",
                Password = "user",
                FirstName = "Danilo",
                LastName = "Novakovic",
                RoleName = RoleNames.RegularUser
            });

            _logger?.Debug("Seeding countries...");

            _context.Countries.AddOrUpdate(new[]
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

            _logger?.Debug("Seeding locations...");

            _context.Locations.AddOrUpdate(new[]
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

            _logger?.Debug("Seeding stations...");

            var stations = new[]
            {
                new RailwayStation
                {
                    Id = 1,
                    LocationId = 4,
                    Name = "Stanica Vrbas",
                    NumberOfPlatforms = 1
                },
                new RailwayStation
                {
                    Id = 2,
                    LocationId = 5,
                    Name = "Stanica Novi Sad",
                    NumberOfPlatforms = 2
                }
            };

            _context.RailwayStations.AddOrUpdate(stations);

            _logger?.Debug("Seeding platforms...");

            var platforms = new[]
            {
                new RailwayPlatform
                {
                    Id = 1,
                    RailwayStationId = 1,
                    EntranceType = EntranceType.Left,
                    Name = "Kolosek I",
                    Mark = "1"
                },
                new RailwayPlatform
                {
                    Id = 2,
                    RailwayStationId = 2,
                    EntranceType = EntranceType.Left,
                    Name = "Kolosek I",
                    Mark = "1"
                },
                new RailwayPlatform
                {
                    Id = 3,
                    RailwayStationId = 2,
                    EntranceType = EntranceType.Right,
                    Name = "Kolosek II",
                    Mark = "2"
                }
            };

            _context.RailwayPlatforms.AddOrUpdate(platforms);

            _logger?.Debug("Seeding routes...");

            var routes = new[]
            {
                new Route
                {
                    Id = 1,
                    Mark = "Ruta 345",
                    Name = "Ruta Kralja Petra Velikog",
                    RailwayStations = stations
                }
            };

            _context.Routes.AddOrUpdate(routes);
        }
    }
}