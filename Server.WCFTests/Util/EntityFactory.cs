using Server.Core;

namespace Server.WCFTests.Util
{
    public static class EntityFactory
    {
        public static Country GetCountry()
        {
            return new Country() { Code = "CountryCode", Id = 1, Name = "Serbia" };
        }

        public static Location GetLocation()
        {
            var country = GetCountry();

            return new Location
            {
                Id = 1,
                Name = "Novi Sad",
                Country = country,
                CountryId = country.Id
            };
        }

        public static RailwayPlatform GetRailwayPlatform()
        {
            return new RailwayPlatform()
            {
                Id = 1,
                EntranceType = Common.EntranceType.Left,
                Mark = "PlatformMark",
                Name = "PlatformName"
            };
        }

        public static RailwayStation GetRailwayStation()
        {
            var location = GetLocation();

            return new RailwayStation
            {
                Id = 1,
                Location = location,
                LocationId = location.Id,
                Name = "StationName",
                NumberOfPlatforms = 0
            };
        }

        public static Route GetRoute()
        {
            var station = GetRailwayStation();

            return new Route()
            {
                Id = 1,
                Mark = "RouteMark",
                Name = "RouteName",
                RailwayStations = new[] { station }
            };
        }
    }
}