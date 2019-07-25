using System;
using System.ServiceModel;

namespace Server
{
    public interface IAuthServiceHostFactory
    {
        ServiceHost GetServiceHost(ushort port, object service, Type contractType);
        ServiceHost GetServiceHost<TContractType>(ushort port, object service);
    }
}