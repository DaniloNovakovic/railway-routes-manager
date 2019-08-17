using Server.Core;
using Server.Persistance.Repositories;

namespace Server.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Countries = new Repository<Country>(context);
            Locations = new Repository<Location>(context);
            RailwayPlatforms = new Repository<RailwayPlatform>(context);
            RailwayStations = new RailwayStationRepository(context);
            Routes = new RouteRepository(context);
            Users = new Repository<User>(context);
        }

        public IRepository<Country> Countries { get; }
        public IRepository<Location> Locations { get; }
        public IRepository<RailwayPlatform> RailwayPlatforms { get; }
        public IRailwayStationRepository RailwayStations { get; }
        public IRouteRepository Routes { get; }
        public IRepository<User> Users { get; set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}