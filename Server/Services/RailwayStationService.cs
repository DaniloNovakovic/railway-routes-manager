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

        public RailwayStationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(RailwayStationDto entity)
        {
            throw new System.NotImplementedException();
        }

        public RailwayStationDto Get(int key)
        {
            var station = _unitOfWork.RailwayStations.Get(key);
            return _mapper.Map<RailwayStationDto>(station);
        }

        public IEnumerable<RailwayStationDto> GetAll()
        {
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