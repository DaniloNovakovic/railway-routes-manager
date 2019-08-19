using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Common;
using Server.Core;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LocationService : ILocationService
    {
        private static readonly object Mutex = new object();
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
            lock (Mutex)
            {
                return AddNew(entity);
            }
        }

        public LocationDto Get(int key)
        {
            Location location;

            lock (Mutex)
            {
                _logger.Debug($"Getting location {key}...");
                location = _unitOfWork.Locations.Get(key);
            }

            return _mapper.Map<LocationDto>(location);
        }

        public IEnumerable<LocationDto> GetAll()
        {
            IEnumerable<Location> locations;

            lock (Mutex)
            {
                _logger.Debug("Getting all locations...");
                locations = _unitOfWork.Locations.GetAll();
            }

            return locations.Select(location => _mapper.Map<LocationDto>(location)).ToList();
        }

        public void Remove(int key)
        {
            lock (Mutex)
            {
                _logger.Debug($"Removing Location {key}...");

                var location = _unitOfWork.Locations.Get(key);

                if (location is null)
                {
                    _logger.Warn($"Location {key} not found!");
                    throw new NotFoundException();
                }

                _unitOfWork.Locations.Remove(location);
                _unitOfWork.SaveChanges();

                _logger.Info($"Removed Location {key}");
            }
        }

        public void Update(int key, LocationDto entity)
        {
            lock (Mutex)
            {
                _logger.Debug($"Updating location {key}...");

                var location = _unitOfWork.Locations.Get(key);

                if (location is null)
                {
                    _logger.Warn($"Could not find location {key}!");
                    entity.Id = key;
                    AddNew(entity);
                    return;
                }

                _mapper.Map(entity, location);
                _unitOfWork.SaveChanges();

                _logger.Info($"Updated location {key}");
            }
        }

        private int AddNew(LocationDto entity)
        {
            _logger.Debug("Adding new location...");

            var location = _mapper.Map<Location>(entity);

            var addedLocation = _unitOfWork.Locations.Add(location);
            _unitOfWork.SaveChanges();

            _logger.Info($"Added location {addedLocation.Id}!");
            return addedLocation.Id;
        }
    }
}