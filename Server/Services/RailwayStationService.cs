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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public RailwayStationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public int Add(RailwayStationDto entity)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public void Update(int key, RailwayStationDto entity)
        {
            throw new System.NotImplementedException();
        }
    }
}