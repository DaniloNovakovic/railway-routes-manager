﻿using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public int Add(RouteDto entity)
        {
            var dbRoute = _unitOfWork.Routes.GetDeleted(entity.Id);
            if (dbRoute != null)
            {
                OverwriteExisting(entity, dbRoute);
                return entity.Id;
            }
            return AddNew(entity);
        }

        public RouteDto Get(int key)
        {
            var route = _unitOfWork.Routes.Get(key);
            return _mapper.Map<RouteDto>(route);
        }

        public IEnumerable<RouteDto> GetAll()
        {
            var routes = _unitOfWork.Routes.GetAll();
            return routes.Select(route => _mapper.Map<RouteDto>(route));
        }

        public void Remove(int key)
        {
            var route = _unitOfWork.Routes.Get(key);

            if (route is null)
            {
                throw new NotFoundException();
            }

            _unitOfWork.Routes.Remove(route);
            _unitOfWork.SaveChanges();
        }

        public void Resurrect(int key)
        {
            _unitOfWork.Routes.Resurrect(key);
            _unitOfWork.SaveChanges();
        }

        public void Update(int key, RouteDto entity)
        {
            Resurrect(key);

            var route = _unitOfWork.Routes.Get(key);

            route.Mark = entity.Mark ?? route.Mark;
            route.Name = entity.Name ?? route.Name;

            if (entity.RailwayStations != null)
            {
                route.RailwayStations = GetStations(entity);
            }

            _unitOfWork.SaveChanges();
        }

        private int AddNew(RouteDto entity)
        {
            var route = _mapper.Map<Route>(entity);
            route.RailwayStations = GetStations(entity);

            var addedRoute = _unitOfWork.Routes.Add(route);
            _unitOfWork.SaveChanges();

            return addedRoute.Id;
        }

        private List<RailwayStation> GetStations(RouteDto entity)
        {
            int[] ids = entity.RailwayStations.Select(s => s.Id).ToArray();
            return _unitOfWork.RailwayStations.GetAll(station => ids.Contains(station.Id)).ToList();
        }

        private void OverwriteExisting(RouteDto entity, Route dbRoute)
        {
            _mapper.Map(entity, dbRoute);
            dbRoute.DeletionDate = null;

            _unitOfWork.SaveChanges();
        }
    }
}