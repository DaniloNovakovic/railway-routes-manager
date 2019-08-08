using System.Collections.Generic;
using System.ServiceModel;
using Common;
using Server.Core;

namespace Server
{
    public static class Startup
    {
        public static IEnumerable<ICommunicationObject> GetHosts(IServiceProvider provider)
        {
            var hostFactory = provider.Resolve<IAuthServiceHostFactory>();

            return new List<ICommunicationObject>
            {
                hostFactory.GetServiceHost<IAuthService>(Ports.AuthServicePort, provider.Resolve<IAuthService>()),
                hostFactory.GetServiceHost<IRouteService>(Ports.RouteServicePort, provider.Resolve<IRouteService>()),
                hostFactory.GetServiceHost<IUserService>(Ports.UserServicePort, provider.Resolve<IUserService>()),
            };
        }

        public static IServiceProvider GetServiceProvider(IUnitOfWork unitOfWork)
        {
            var mapper = AutoMapperFactory.GetAutoMapper();
            var validator = new CustomUserNamePasswordValidator(unitOfWork);
            var provider = new ServiceProvider();

            provider.Register<IAuthServiceHostFactory>(new AuthServiceHostFactory(validator));
            provider.Register<IAuthService>(new AuthService(unitOfWork));
            provider.Register<IRouteService>(new RouteService(unitOfWork, mapper));
            provider.Register<IUserService>(new UserService(unitOfWork, mapper));

            return provider;
        }
    }
}