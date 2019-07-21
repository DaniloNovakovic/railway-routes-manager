using Server.Core;

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
            RailwayStations = new Repository<RailwayStation>(context);
            Routes = new Repository<Route>(context);
        }

        public IRepository<Country> Countries { get; }
        public IRepository<Location> Locations { get; }
        public IRepository<RailwayPlatform> RailwayPlatforms { get; }
        public IRepository<RailwayStation> RailwayStations { get; }
        public IRepository<Route> Routes { get; }

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