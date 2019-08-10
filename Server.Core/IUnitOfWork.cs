using System;

namespace Server.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Country> Countries { get; }
        IRepository<Location> Locations { get; }
        IRepository<RailwayPlatform> RailwayPlatforms { get; }
        IRepository<RailwayStation> RailwayStations { get; }
        IRouteRepository Routes { get; }
        IRepository<User> Users { get; set; }

        int SaveChanges();
    }
}