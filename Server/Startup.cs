using System.Collections.Generic;
using System.ServiceModel;
using AutoMapper;
using Common;
using Server.Core;

namespace Server
{
    public static class Startup
    {
        public static IEnumerable<ICommunicationObject> GetHosts(IServiceProvider provider)
        {
            var factory = GetServiceHostFactory(provider);

            return new List<ICommunicationObject>
            {
                factory.GetServiceHost<IAuthService>(Ports.AuthServicePort),
                factory.GetServiceHost<IRouteService>(Ports.RouteServicePort),
                factory.GetServiceHost<IRailwayStationService>(Ports.RailwayStationServicePort),
                factory.GetServiceHost<IUserService>(Ports.UserServicePort),
                factory.GetServiceHost<ILocationService>(Ports.LocationServicePort),
            };
        }

        public static IServiceProvider GetServiceProvider(IUnitOfWork unitOfWork, ILogger logger)
        {
            var mapper = AutoMapperFactory.GetAutoMapper();
            var provider = new ServiceProvider();

            provider.Register<ILogger>(logger);
            provider.Register<IUnitOfWork>(unitOfWork);
            provider.Register<IMapper>(mapper);
            provider.Register<IAuthService>(new AuthService(unitOfWork, logger));
            provider.Register<IRouteService>(new RouteService(unitOfWork, mapper, logger));
            provider.Register<IRailwayStationService>(new RailwayStationService(unitOfWork, mapper, logger));
            provider.Register<IUserService>(new UserService(unitOfWork, mapper, logger));
            provider.Register<ILocationService>(new LocationService(unitOfWork, mapper, logger));

            return provider;
        }

        private static IAuthServiceHostFactoryFacade GetServiceHostFactory(IServiceProvider provider)
        {
            var unitOfWork = provider.Resolve<IUnitOfWork>();
            var logger = provider.Resolve<ILogger>();
            var validator = new CustomUserNamePasswordValidator(unitOfWork, logger);
            return new AuthServiceHostFactoryFacade(new AuthServiceHostFactory(validator), provider);
        }
    }
}