using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Client.Core;

namespace Client.Infrastructure
{
    public class RailwayStationService : IRailwayStationService
    {
        private readonly IAuthChannelFactory _factory;
        private readonly IMapper _mapper;
        private readonly ushort _port = Common.Ports.RailwayStationServicePort;

        public RailwayStationService(IAuthChannelFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Task<int> AddStationAsync(RailwayStationModel station)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var dto = _mapper.Map<Common.RailwayStationDto>(station);
                return proxy.Add(dto);
            });
        }

        public Task<IEnumerable<RailwayStationModel>> GetAllStationsAsync()
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var stationDtos = proxy.GetAll();
                return stationDtos.Select(dto => _mapper.Map<RailwayStationModel>(dto));
            });
        }

        public Task<RailwayStationModel> GetStationAsync(int key)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveStationAsync(int key)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                proxy.Remove(key);
            });
        }

        public Task UpdateStationAsync(RailwayStationModel station)
        {
            throw new System.NotImplementedException();
        }

        private Common.IRailwayStationService GetProxy()
        {
            return _factory.GetChannelFactory<Common.IRailwayStationService>(_port).CreateChannel();
        }
    }
}