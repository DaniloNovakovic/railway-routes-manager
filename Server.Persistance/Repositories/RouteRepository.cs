using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Server.Core;

namespace Server.Persistance.Repositories
{
    public class RouteRepository : LogicalRepository<Route>, IRouteRepository
    {
        private readonly ApplicationDbContext _context;

        public RouteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Route Get(params object[] keyValues)
        {
            int id = (int)keyValues[0];

            return GetRoutes().SingleOrDefault(route => route.Id == id);
        }

        public override Route Get(Expression<Func<Route, bool>> filter)
        {
            return GetRoutes().FirstOrDefault(filter);
        }

        public override IEnumerable<Route> GetAll(Expression<Func<Route, bool>> filter = null)
        {
            var query = GetRoutes();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        private IQueryable<Route> GetRoutes()
        {
            return _context.Routes
                .Include(route => route.RailwayStations)
                .Where(route => route.DeletionDate == null);
        }
    }
}