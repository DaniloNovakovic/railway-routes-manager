using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Common;
using Server.Core;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RailwayStationService : IRailwayStationService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RailwayStationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public int Add(RailwayStationDto entity)
        {
            _logger.Info("Adding new route...");

            var dbStation = _mapper.Map<RailwayStation>(entity);
            dbStation.RailwayPlatforms = GetPlatforms(entity);
            dbStation.Location = GetLocation(entity);

            var addedRoute = _unitOfWork.RailwayStations.Add(dbStation);
            _unitOfWork.SaveChanges();

            _logger.Info($"New route {addedRoute.Id} added!");
            return addedRoute.Id;
        }

        public RailwayStationDto Get(int key)
        {
            _logger.Info($"Getting station {key}...");
            var station = _unitOfWork.RailwayStations.Get(key);
            return _mapper.Map<RailwayStationDto>(station);
        }

        public IEnumerable<RailwayStationDto> GetAll()
        {
            _logger.Info("Getting list of all stations...");
            var stations = _unitOfWork.RailwayStations.GetAll();
            return stations.Select(station => _mapper.Map<RailwayStationDto>(station));
        }

        public void Remove(int key)
        {
            _logger.Info($"Attempting to remove station {key}...");

            var station = _unitOfWork.RailwayStations.Get(key);

            if (station is null)
            {
                _logger.Warn($"Route {key} not found!");
                throw new NotFoundException();
            }

            _unitOfWork.RailwayStations.Remove(station);
            _unitOfWork.SaveChanges();
        }

        public void Update(int key, RailwayStationDto entity)
        {
            _logger.Info($"Updating station {key}...");

            var dtoStation = _unitOfWork.RailwayStations.Get(key);

            if (dtoStation == null)
            {
                _logger.Warn($"Requested station {key} does not exist!");
                entity.Id = key;
                Add(entity);
                return;
            }

            dtoStation.Name = entity.Name;

            if (entity.Location != null)
            {
                dtoStation.Location = GetLocation(entity);
            }

            if (entity.RailwayPlatforms != null && entity.RailwayPlatforms.Count > 0)
            {
                dtoStation.RailwayPlatforms = GetPlatforms(entity);
                dtoStation.NumberOfPlatforms = dtoStation.RailwayPlatforms.Count;
            }

            _unitOfWork.SaveChanges();
        }

        private Location GetLocation(RailwayStationDto entity)
        {
            return _unitOfWork.Locations.Get(entity.Location.Id);
        }

        private ICollection<RailwayPlatform> GetPlatforms(RailwayStationDto entity)
        {
            int[] ids = entity.RailwayPlatforms.Select(rp => rp.Id).ToArray();
            return _unitOfWork.RailwayPlatforms.GetAll(rp => ids.Contains(rp.Id)).ToList();
        }
    }
}