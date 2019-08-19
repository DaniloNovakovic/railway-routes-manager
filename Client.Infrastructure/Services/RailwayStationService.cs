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
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ushort _port = Common.Ports.RailwayStationServicePort;

        public RailwayStationService(IAuthChannelFactory factory, IMapper mapper, ILogger logger)
        {
            _factory = factory;
            _mapper = mapper;
            _logger = new AuthLoggerDecorator(logger, _factory.Username);
        }

        public Task<int> AddStationAsync(RailwayStationModel station)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var dto = _mapper.Map<Common.RailwayStationDto>(station);
                int id = proxy.Add(dto);
                _logger.Info($"Added station {id}");
                return id;
            });
        }

        public Task<IEnumerable<RailwayStationModel>> GetAllStationsAsync()
        {
            return Task.Run(() =>
            {
                _logger.Debug("Getting all stations...");
                var proxy = GetProxy();
                var stationDtos = proxy.GetAll();
                return stationDtos.Select(dto => _mapper.Map<RailwayStationModel>(dto));
            });
        }

        public Task<RailwayStationModel> GetStationAsync(int key)
        {
            return Task.Run(() =>
            {
                _logger.Debug($"Getting station {key}");
                var proxy = GetProxy();
                var stationDto = proxy.Get(key);
                return _mapper.Map<RailwayStationModel>(stationDto);
            });
        }

        public Task RemoveStationAsync(int key)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                proxy.Remove(key);
                _logger.Info($"Removed station {key}");
            });
        }

        public Task UpdateStationAsync(RailwayStationModel station)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var dto = _mapper.Map<Common.RailwayStationDto>(station);
                proxy.Update(station.Id, dto);
                _logger.Info($"Updated station {station.Id}");
            });
        }

        private Common.IRailwayStationService GetProxy()
        {
            return _factory.GetChannelFactory<Common.IRailwayStationService>(_port).CreateChannel();
        }
    }
}