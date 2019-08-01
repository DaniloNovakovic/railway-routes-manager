using AutoMapper;
using Common;
using Server.Core;

namespace Server
{
    public static class AutoMapperFactory
    {
        public static IMapper GetAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>()
                    .ForAllMembers(ApplyIgnoreNullOrEmptyConfiguration);
            });

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
    }
}