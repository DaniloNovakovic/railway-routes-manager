using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common;
using Server.Core;

namespace Server.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LocationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public int Add(LocationDto entity)
        {
            throw new NotImplementedException();
        }

        public LocationDto Get(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocationDto> GetAll()
        {
            _logger.Info("Getting all locations...");

            var locations = _unitOfWork.Locations.GetAll();
            return locations.Select(location => _mapper.Map<LocationDto>(location)).ToList();
        }

        public void Remove(int key)
        {
            throw new NotImplementedException();
        }

        public void Update(int key, LocationDto entity)
        {
            throw new NotImplementedException();
        }
    }
}