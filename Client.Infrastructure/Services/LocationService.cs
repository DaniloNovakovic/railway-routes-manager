using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Client.Core;

namespace Client.Infrastructure
{
    public class LocationService : ILocationService
    {
        private readonly IAuthChannelFactory _factory;
        private readonly IMapper _mapper;
        private readonly ushort _port = Common.Ports.LocationServicePort;

        public LocationService(IAuthChannelFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Task<IEnumerable<LocationModel>> GetAllLocationsAsync()
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var locationDtos = proxy.GetAll();
                return locationDtos.Select(dto => _mapper.Map<LocationModel>(dto));
            });
        }

        private Common.ILocationService GetProxy()
        {
            return _factory.GetChannelFactory<Common.ILocationService>(_port).CreateChannel();
        }
    }
}