using System;
using System.Collections.Generic;

namespace Server
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly IDictionary<Type, object> _services;

        public ServiceProvider()
        {
            _services = new Dictionary<Type, object>();
        }

        public void Register<TInterface>(object service)
        {
            Register(typeof(TInterface), service);
        }

        public void Register(Type interfaceType, object service)
        {
            var serviceType = service.GetType();

            if (!interfaceType.IsAssignableFrom(serviceType))
            {
                throw new ArgumentException($"{interfaceType.FullName} is not assignable from {serviceType.FullName}");
            }

            _services[interfaceType] = service;
        }

        public TInterface Resolve<TInterface>()
        {
            return (TInterface)Resolve(typeof(TInterface));
        }

        public object Resolve(Type interfaceType)
        {
            return _services.TryGetValue(interfaceType, out object value) ? value : null;
        }
    }
}