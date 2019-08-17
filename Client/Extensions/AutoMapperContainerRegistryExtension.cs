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
            var config = new MapperConfiguration(ConfigureAutoMapper);

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();
            containerRegistry.RegisterInstance(mapper);
        }

        private static void ConfigureAutoMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserModel, UserDto>(MemberList.Destination);
            cfg.CreateMap<UserDto, UserModel>(MemberList.Source);

            cfg.CreateMap<CountryModel, CountryDto>(MemberList.Destination);
            cfg.CreateMap<CountryDto, CountryModel>(MemberList.Source);

            cfg.CreateMap<LocationModel, LocationDto>(MemberList.Destination);
            cfg.CreateMap<LocationDto, LocationModel>(MemberList.Source);

            cfg.CreateMap<RailwayStationModel, RailwayStationDto>(MemberList.Destination);
            cfg.CreateMap<RailwayStationDto, RailwayStationModel>(MemberList.Source);

            cfg.CreateMap<RailwayPlatformModel, RailwayPlatformDto>(MemberList.Destination);
            cfg.CreateMap<RailwayPlatformDto, RailwayPlatformModel>(MemberList.Source);

            cfg.CreateMap<RouteModel, RouteDto>(MemberList.Destination);
            cfg.CreateMap<RouteDto, RouteModel>(MemberList.Source);
        }
    }
}