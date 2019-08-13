using System.ServiceModel;

namespace Server
{
    public class AuthServiceHostFactoryFacade : IAuthServiceHostFactoryFacade
    {
        private readonly IAuthServiceHostFactory _factory;
        private readonly IServiceProvider _provider;

        public AuthServiceHostFactoryFacade(IAuthServiceHostFactory factory, IServiceProvider provider)
        {
            _factory = factory;
            _provider = provider;
        }

        public ServiceHost GetServiceHost<TContractType>(ushort port)
        {
            var service = _provider.Resolve<TContractType>();
            return _factory.GetServiceHost<TContractType>(port, service);
        }
    }
}