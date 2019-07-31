using AutoMapper;
using Client.Core;
using Common;
using Prism.Ioc;

namespace Client.Extensions
{
    public static class AutoMapperContainerRegistryExtension
    {
        public static void RegisterAutoMapper(this IContainerRegistry containerRegistry)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserModel, UserDto>(MemberList.Destination);
                cfg.CreateMap<UserDto, UserModel>(MemberList.Source);
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();
            containerRegistry.RegisterInstance(mapper);
        }
    }
}