using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common;
using Server.Core;

namespace Server.Services
{
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
            throw new NotImplementedException();
        }

        public RouteDto Get(int key)
        {
            throw new NotImplementedException();
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