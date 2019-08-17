using System.ServiceModel;

namespace Server
{
    public interface IAuthServiceHostFactoryFacade
    {
        ServiceHost GetServiceHost<TContractType>(ushort port);
    }
}