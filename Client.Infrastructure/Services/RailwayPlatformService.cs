using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Client.Core;

namespace Client.Infrastructure
{
    public class RailwayPlatformService : IRailwayPlatformService
    {
        private readonly IAuthChannelFactory _factory;
        private readonly IMapper _mapper;
        private readonly ushort _port = Common.Ports.RailwayStationServicePort;

        public RailwayPlatformService(IAuthChannelFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Task<int> AddPlatformAsync(RailwayPlatformModel platform)
        {
            return Task.Run(() =>
            {
                var dto = _mapper.Map<Common.RailwayPlatformDto>(platform);
                var proxy = GetProxy();
                return proxy.Add(dto);
            });
        }

        public Task<IEnumerable<RailwayPlatformModel>> GetAllPlatformsAsync()
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var platformDtos = proxy.GetAll();
                return platformDtos.Select(dto => _mapper.Map<RailwayPlatformModel>(dto));
            });
        }

        public Task<RailwayPlatformModel> GetPlatformAsync(int key)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var dto = proxy.Get(key);
                return _mapper.Map<RailwayPlatformModel>(dto);
            });
        }

        public Task RemovePlatformAsync(int key)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                proxy.Remove(key);
            });
        }

        public Task UpdatePlatformAsync(RailwayPlatformModel station)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var dto = _mapper.Map<Common.RailwayPlatformDto>(station);
                proxy.Update(station.Id, dto);
            });
        }

        private Common.IRailwayPlatformService GetProxy()
        {
            return _factory.GetChannelFactory<Common.IRailwayPlatformService>(_port).CreateChannel();
        }
    }
}