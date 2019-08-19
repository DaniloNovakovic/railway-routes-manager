using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Common;
using Server.Core;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RailwayPlatformService : IRailwayPlatformService
    {
        private readonly static object Mutex = new object();
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RailwayPlatformService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public int Add(RailwayPlatformDto entity)
        {
            lock (Mutex)
            {
                return AddNew(entity);
            }
        }

        public RailwayPlatformDto Get(int key)
        {
            _logger.Debug($"Getting platform {key}");

            var platform = _unitOfWork.RailwayPlatforms.Get(key);
            return _mapper.Map<RailwayPlatformDto>(platform);
        }

        public IEnumerable<RailwayPlatformDto> GetAll()
        {
            _logger.Debug("Getting all platforms...");

            var platforms = _unitOfWork.RailwayPlatforms.GetAll();
            return platforms.Select(platform => _mapper.Map<RailwayPlatformDto>(platform));
        }

        public void Remove(int key)
        {
            lock (Mutex)
            {
                _logger.Debug($"Removing platform {key}...");

                var platform = _unitOfWork.RailwayPlatforms.Get(key);
                if (platform is null)
                {
                    _logger.Warn($"Requested platform {key} not found!");
                    throw new NotFoundException();
                }

                _unitOfWork.RailwayPlatforms.Remove(platform);
                _unitOfWork.SaveChanges();

                _logger.Info($"Removed platform {key}");
            }
        }

        public void Update(int key, RailwayPlatformDto entity)
        {
            lock (Mutex)
            {
                _logger.Debug($"Updating platform {key}...");

                var platform = _unitOfWork.RailwayPlatforms.Get(key);

                if (platform is null)
                {
                    _logger.Warn($"Requested platform {key} does not exist!");
                    entity.Id = key;
                    AddNew(entity);
                    return;
                }

                platform.Mark = entity.Mark ?? platform.Mark;
                platform.Name = entity.Name ?? platform.Name;
                platform.RailwayStationId = entity.RailwayStationId ?? platform.RailwayStationId;
                platform.EntranceType = entity.EntranceType;

                _unitOfWork.SaveChanges();

                _logger.Info($"Updated platform {key}");
            }
        }

        private int AddNew(RailwayPlatformDto entity)
        {
            _logger.Debug("Adding new platform...");

            var platform = _mapper.Map<RailwayPlatform>(entity);

            var addedPlatform = _unitOfWork.RailwayPlatforms.Add(platform);
            _unitOfWork.SaveChanges();

            _logger.Info($"New platform {addedPlatform.Id} added!");

            return addedPlatform.Id;
        }
    }
}