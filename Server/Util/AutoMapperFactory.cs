using AutoMapper;
using Common;
using Server.Core;

namespace Server
{
    public static class AutoMapperFactory
    {
        public static IMapper GetAutoMapper()
        {
            var config = new MapperConfiguration(ConfigureMapper);

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }

        private static void ApplyIgnoreNullOrEmptyConfiguration<TSrc, TDest>(IMemberConfigurationExpression<TSrc, TDest, object> opts)
        {
            opts.Condition((_, __, srcMember) => !CheckIfNullOrEmpty(srcMember));
        }

        private static bool CheckIfNullOrEmpty(object srcMember)
        {
            return srcMember is string str ? string.IsNullOrEmpty(str) : srcMember == null;
        }

        private static void ConfigureMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, UserDto>();
            cfg.CreateMap<UserDto, User>()
                .ForAllMembers(ApplyIgnoreNullOrEmptyConfiguration);

            cfg.CreateMap<Country, CountryDto>();
            cfg.CreateMap<CountryDto, Country>();

            cfg.CreateMap<Location, LocationDto>(MemberList.Destination);
            cfg.CreateMap<LocationDto, Location>()
                .ForAllMembers(ApplyIgnoreNullOrEmptyConfiguration);

            cfg.CreateMap<RailwayPlatform, RailwayPlatformDto>(MemberList.Destination);
            cfg.CreateMap<RailwayPlatformDto, RailwayPlatform>(MemberList.Source)
                .ForAllMembers(ApplyIgnoreNullOrEmptyConfiguration);

            cfg.CreateMap<RailwayStation, RailwayStationDto>(MemberList.Destination);
            cfg.CreateMap<RailwayStationDto, RailwayStation>(MemberList.Source)
                .ForAllMembers(ApplyIgnoreNullOrEmptyConfiguration);

            cfg.CreateMap<Route, RouteDto>(MemberList.Destination);
            cfg.CreateMap<RouteDto, Route>(MemberList.Source)
                .ForAllMembers(ApplyIgnoreNullOrEmptyConfiguration);
        }
    }
}