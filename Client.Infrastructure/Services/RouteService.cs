using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Client.Core;

namespace Client.Infrastructure.Services
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

        public Task<IEnumerable<RouteModel>> GetAllRoutesAsync()
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<Common.IRouteService>(_port).CreateChannel();
                var routeDtos = proxy.GetAll();
                return routeDtos.Select(routeDto => _mapper.Map<RouteModel>(routeDto));
            });
        }
    }
}