using System.Data.Entity;
using Server.Core;

namespace Server.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RailwayPlatform> RailwayPlatforms { get; set; }
        public DbSet<RailwayStation> RailwayStations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}