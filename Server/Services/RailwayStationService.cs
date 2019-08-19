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
        private static readonly object Mutex = new object();
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
            lock (Mutex)
            {
                return AddNew(entity);
            }
        }

        public RailwayStationDto Get(int key)
        {
            RailwayStation station;

            lock (Mutex)
            {
                _logger.Debug($"Getting station {key}...");
                station = _unitOfWork.RailwayStations.Get(key);
            }

            return _mapper.Map<RailwayStationDto>(station);
        }

        public IEnumerable<RailwayStationDto> GetAll()
        {
            IEnumerable<RailwayStation> stations;

            lock (Mutex)
            {
                _logger.Debug("Getting list of all stations...");
                stations = _unitOfWork.RailwayStations.GetAll();
            }

            return stations.Select(station => _mapper.Map<RailwayStationDto>(station));
        }

        public void Remove(int key)
        {
            lock (Mutex)
            {
                _logger.Debug($"Attempting to remove station {key}...");

                var station = _unitOfWork.RailwayStations.Get(key);

                if (station is null)
                {
                    _logger.Warn($"Route {key} not found!");
                    throw new NotFoundException();
                }

                _unitOfWork.RailwayStations.Remove(station);
                _unitOfWork.SaveChanges();

                _logger.Info($"Removed station {key}");
            }
        }

        public void Update(int key, RailwayStationDto entity)
        {
            lock (Mutex)
            {
                _logger.Debug($"Updating station {key}...");

                var station = _unitOfWork.RailwayStations.Get(key);

                if (station == null)
                {
                    _logger.Warn($"Requested station {key} does not exist!");
                    entity.Id = key;
                    AddNew(entity);
                    return;
                }

                station.Name = entity.Name;

                if (entity.Location != null)
                {
                    station.Location = GetLocation(entity);
                }

                if (entity.RailwayPlatforms != null && entity.RailwayPlatforms.Count > 0)
                {
                    station.RailwayPlatforms = GetPlatforms(entity);
                    station.NumberOfPlatforms = station.RailwayPlatforms.Count;
                }

                _unitOfWork.SaveChanges();

                _logger.Info($"Updated station {key}");
            }
        }

        private int AddNew(RailwayStationDto entity)
        {
            _logger.Debug("Adding new station...");

            var station = _mapper.Map<RailwayStation>(entity);
            station.RailwayPlatforms = GetPlatforms(entity);
            station.Location = GetLocation(entity);

            var addedStation = _unitOfWork.RailwayStations.Add(station);
            _unitOfWork.SaveChanges();

            _logger.Info($"New station {addedStation.Id} added!");
            return addedStation.Id;
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