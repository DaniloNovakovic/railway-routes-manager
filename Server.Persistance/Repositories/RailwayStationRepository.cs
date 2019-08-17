using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Server.Core;

namespace Server.Persistance
{
    public class RailwayStationRepository : Repository<RailwayStation>, IRailwayStationRepository
    {
        private readonly ApplicationDbContext _context;

        public RailwayStationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override RailwayStation Get(params object[] keyValues)
        {
            int id = (int)keyValues[0];

            return GetStations().SingleOrDefault(route => route.Id == id);
        }

        public override RailwayStation Get(Expression<Func<RailwayStation, bool>> filter)
        {
            return GetStations().FirstOrDefault(filter);
        }

        public override IEnumerable<RailwayStation> GetAll(Expression<Func<RailwayStation, bool>> filter = null)
        {
            var query = GetStations();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        private IQueryable<RailwayStation> GetStations()
        {
            return _context.RailwayStations
                .Include(rs => rs.RailwayPlatforms)
                .Include(rs => rs.Location.Country);
        }
    }
}