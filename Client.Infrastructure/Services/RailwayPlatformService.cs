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
        private readonly ILogger _logger;

        public RailwayPlatformService(IAuthChannelFactory factory, IMapper mapper, ILogger logger)
        {
            _factory = factory;
            _mapper = mapper;
            _logger = new AuthLoggerDecorator(logger, _factory.Username);
        }

        public Task<int> AddPlatformAsync(RailwayPlatformModel platform)
        {
            return Task.Run(() =>
            {
                var dto = _mapper.Map<Common.RailwayPlatformDto>(platform);
                var proxy = GetProxy();
                int id = proxy.Add(dto);
                _logger.Info($"Added platform {id}");
                return id;
            });
        }

        public Task<IEnumerable<RailwayPlatformModel>> GetAllPlatformsAsync()
        {
            return Task.Run(() =>
            {
                _logger.Debug("Getting list of platforms...");
                var proxy = GetProxy();
                var platformDtos = proxy.GetAll();
                return platformDtos.Select(dto => _mapper.Map<RailwayPlatformModel>(dto));
            });
        }

        public Task<RailwayPlatformModel> GetPlatformAsync(int key)
        {
            return Task.Run(() =>
            {
                _logger.Debug($"Getting platform {key}");
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
                _logger.Info($"Removed platform {key}");
            });
        }

        public Task UpdatePlatformAsync(RailwayPlatformModel station)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var dto = _mapper.Map<Common.RailwayPlatformDto>(station);
                proxy.Update(station.Id, dto);
                _logger.Info($"Updated platform {station.Id}");
            });
        }

        private Common.IRailwayPlatformService GetProxy()
        {
            return _factory.GetChannelFactory<Common.IRailwayPlatformService>(_port).CreateChannel();
        }
    }
}