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
        private readonly IMapper _mapper;
        private readonly ushort _port = Common.Ports.RouteServicePort;

        public RouteService(IAuthChannelFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Task<int> AddRouteAsync(RouteModel route)
        {
            var routeDto = _mapper.Map<Common.RouteDto>(route);

            return Task.Run(() =>
            {
                var proxy = GetProxy();
                return proxy.Add(routeDto);
            });
        }

        public Task<IEnumerable<RouteModel>> GetAllRoutesAsync()
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var routeDtos = proxy.GetAll();
                return routeDtos.Select(routeDto => _mapper.Map<RouteModel>(routeDto));
            });
        }

        public Task<RouteModel> GetRouteAsync(int key)
        {
            return Task.Run(() =>
            {
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
            });
        }

        public Task UpdateRouteAsync(RouteModel route)
        {
            var routeDto = _mapper.Map<Common.RouteDto>(route);

            return Task.Run(() =>
            {
                var proxy = GetProxy();
                proxy.Update(route.Id, routeDto);
            });
        }

        private Common.IRouteService GetProxy()
        {
            return _factory.GetChannelFactory<Common.IRouteService>(_port).CreateChannel();
        }
    }
}