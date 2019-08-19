using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Client.Core;

namespace Client.Infrastructure
{
    public class RouteService : IRouteService
    {
        private readonly IAuthChannelFactory _factory;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ushort _port = Common.Ports.RouteServicePort;

        public RouteService(IAuthChannelFactory factory, IMapper mapper, ILogger logger)
        {
            _factory = factory;
            _mapper = mapper;
            _logger = new AuthLoggerDecorator(logger, _factory.Username);
        }

        public Task<int> AddRouteAsync(RouteModel route)
        {
            var routeDto = _mapper.Map<Common.RouteDto>(route);

            return Task.Run(() =>
            {
                var proxy = GetProxy();
                int id = proxy.Add(routeDto);
                _logger.Info($"Added route {id}");
                return id;
            });
        }

        public Task<IEnumerable<RouteModel>> GetAllRoutesAsync()
        {
            return Task.Run(() =>
            {
                _logger.Debug("Getting all routes...");
                var proxy = GetProxy();
                var routeDtos = proxy.GetAll();
                return routeDtos.Select(routeDto => _mapper.Map<RouteModel>(routeDto));
            });
        }

        public Task<RouteModel> GetRouteAsync(int key)
        {
            return Task.Run(() =>
            {
                _logger.Debug($"Getting route {key}");
                var proxy = GetProxy();
                var dto = proxy.Get(key);
                return _mapper.Map<RouteModel>(dto);
            });
        }

        public Task RemoveRouteAsync(int key)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                proxy.Remove(key);
                _logger.Info($"Removed route {key}");
            });
        }

        public Task ResurrectAsync(RouteModel route)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                proxy.Resurrect(route.Id);
                _logger.Info($"Resurrected route {route.Id}");
            });
        }

        public Task UpdateRouteAsync(RouteModel route)
        {
            var routeDto = _mapper.Map<Common.RouteDto>(route);

            return Task.Run(() =>
            {
                var proxy = GetProxy();
                proxy.Update(route.Id, routeDto);
                _logger.Info($"Updated route {route.Id}");
            });
        }

        private Common.IRouteService GetProxy()
        {
            return _factory.GetChannelFactory<Common.IRouteService>(_port).CreateChannel();
        }
    }
}