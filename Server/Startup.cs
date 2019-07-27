﻿using System.Collections.Generic;
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
            var hostFactory = provider.Resolve<IAuthServiceHostFactory>();

            return new List<ICommunicationObject>
            {
                hostFactory.GetServiceHost<IUserService>(Ports.UserServicePort, provider.Resolve<IUserService>()),
                hostFactory.GetServiceHost<IAuthService>(Ports.AuthServicePort, provider.Resolve<IAuthService>())
            };
        }

        public static IServiceProvider GetServiceProvider(IUnitOfWork unitOfWork)
        {
            var mapper = GetAutoMapper();
            var validator = new CustomUserNamePasswordValidator(unitOfWork);
            var provider = new ServiceProvider();

            provider.Register<IAuthServiceHostFactory>(new AuthServiceHostFactory(validator));
            provider.Register<IUserService>(new UserService(unitOfWork, mapper));
            provider.Register<IAuthService>(new AuthService(unitOfWork));

            return provider;
        }

        private static IMapper GetAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}