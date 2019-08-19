using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Common;
using Server.Core;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RouteService : IRouteService
    {
        private static readonly object Mutex = new object();
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public int Add(RouteDto entity)
        {
            lock (Mutex)
            {
                _logger.Debug($"Add route {entity.Id} request...");

                var dbRoute = _unitOfWork.Routes.GetDeleted(entity.Id);
                if (dbRoute != null)
                {
                    OverwriteExisting(entity, dbRoute);
                    return entity.Id;
                }
                return AddNew(entity);
            }
        }

        public RouteDto Get(int key)
        {
            Route route;

            lock (Mutex)
            {
                _logger.Debug($"Getting Route {key}...");
                route = _unitOfWork.Routes.Get(key);
            }

            return _mapper.Map<RouteDto>(route);
        }

        public IEnumerable<RouteDto> GetAll()
        {
            IEnumerable<Route> routes;

            lock (Mutex)
            {
                _logger.Debug($"Getting list of all routes...");
                routes = _unitOfWork.Routes.GetAll();
            }

            return routes.Select(route => _mapper.Map<RouteDto>(route));
        }

        public void Remove(int key)
        {
            lock (Mutex)
            {
                _logger.Debug($"Attempting to remove route {key}...");

                var route = _unitOfWork.Routes.Get(key);

                if (route is null)
                {
                    _logger.Warn($"Route {key} not found!");
                    throw new NotFoundException();
                }

                _unitOfWork.Routes.Remove(route);
                _unitOfWork.SaveChanges();

                _logger.Info($"Removed route {key}");
            }
        }

        public void Resurrect(int key)
        {
            lock (Mutex)
            {
                TryRessurect(key);
            }
        }

        public void Update(int key, RouteDto entity)
        {
            lock (Mutex)
            {
                _logger.Debug($"Updating Route {key}...");

                TryRessurect(key);

                var route = _unitOfWork.Routes.Get(key);

                route.Mark = entity.Mark ?? route.Mark;
                route.Name = entity.Name ?? route.Name;

                if (entity.RailwayStations != null)
                {
                    route.RailwayStations = GetStations(entity);
                }

                _unitOfWork.SaveChanges();

                _logger.Info($"Updated Route {key}");
            }
        }

        private int AddNew(RouteDto entity)
        {
            _logger.Debug("Adding new route...");

            var route = _mapper.Map<Route>(entity);
            route.RailwayStations = GetStations(entity);

            var addedRoute = _unitOfWork.Routes.Add(route);
            _unitOfWork.SaveChanges();

            _logger.Info($"New route {entity.Id} added!");

            return addedRoute.Id;
        }

        private List<RailwayStation> GetStations(RouteDto entity)
        {
            _logger.Debug($"Getting list of all stations...");

            int[] ids = entity.RailwayStations.Select(s => s.Id).ToArray();
            return _unitOfWork.RailwayStations.GetAll(station => ids.Contains(station.Id)).ToList();
        }

        private void OverwriteExisting(RouteDto entity, Route dbRoute)
        {
            _logger.Debug($"Overwriting existing (logically deleted) route {dbRoute.Id}...");

            _mapper.Map(entity, dbRoute);
            dbRoute.DeletionDate = null;

            _unitOfWork.SaveChanges();

            _logger.Info($"Overwrote route {dbRoute.Id}!");
        }

        private void TryRessurect(int key)
        {
            _logger.Debug($"Resurrecting Route {key} if it is logically deleted...");

            bool resurrected = _unitOfWork.Routes.Resurrect(key);
            _unitOfWork.SaveChanges();

            if (resurrected)
            {
                _logger.Info($"Resurrected route {key}");
            }
        }
    }
}