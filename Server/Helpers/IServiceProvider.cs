using System;

namespace Server
{
    public interface IServiceProvider
    {
        void Register(Type interfaceType, object service);

        void Register<TInterface>(object service);

        object Resolve(Type interfaceType);

        TInterface Resolve<TInterface>();
    }
}