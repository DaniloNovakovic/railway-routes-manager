﻿using System;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(RouteDto entity)
        {
            int[] ids = entity.RailwayStations.Select(s => s.Id).ToArray();
            var stations = _unitOfWork.RailwayStations.GetAll(station => ids.Contains(station.Id)).ToList();

            var route = _mapper.Map<Route>(entity);
            route.RailwayStations = stations;

            _unitOfWork.Routes.Add(route);
            _unitOfWork.SaveChanges();
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
            throw new NotImplementedException();
        }

        public void Update(int key, RouteDto entity)
        {
            throw new NotImplementedException();
        }
    }
}