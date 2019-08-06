using System.Linq;
using AutoMapper;
using Common;
using Server.Core;
using Server.WCFTests.Util;
using Xunit;

namespace Server.Tests
{
    public class AutoMapperFactoryTests
    {
        private readonly IMapper _mapper;

        public AutoMapperFactoryTests()
        {
            _mapper = AutoMapperFactory.GetAutoMapper();
        }

        [Fact]
        public void CountryDtoMap_IsValid()
        {
            var country = EntityFactory.GetCountry();

            var dto = _mapper.Map<CountryDto>(country);

            AssertEqual(country, dto);
        }

        [Fact]
        public void LocationDtoMap_IsValid()
        {
            var location = EntityFactory.GetLocation();

            var dto = _mapper.Map<LocationDto>(location);

            AssertEqual(location, dto);
        }

        [Fact]
        public void PlatformMap_IsValid()
        {
            var platform = EntityFactory.GetRailwayPlatform();

            var dto = _mapper.Map<RailwayPlatformDto>(platform);

            AssertEqual(platform, dto);
        }

        [Fact]
        public void RouteMap_IsValid()
        {
            var route = EntityFactory.GetRoute();

            var dto = _mapper.Map<RouteDto>(route);

            AssertEqual(route, dto);
        }

        [Fact]
        public void StationMap_IsValid()
        {
            var station = EntityFactory.GetRailwayStation();

            var dto = _mapper.Map<RailwayStationDto>(station);

            AssertEqual(station, dto);
        }

        private void AssertEqual(Route route, RouteDto dto)
        {
            Assert.Equal(route.Id, dto.Id);
            Assert.Equal(route.Mark, dto.Mark);
            Assert.Equal(route.Name, dto.Name);
            Assert.All(route.RailwayStations, station =>
            {
                var stationDto = dto.RailwayStations.Single(s => s.Id == station.Id);
                AssertEqual(station, stationDto);
            });
        }

        private void AssertEqual(RailwayStation station, RailwayStationDto dto)
        {
            Assert.Equal(station.Id, dto.Id);
            Assert.Equal(station.Name, dto.Name);
            Assert.Equal(station.NumberOfPlatforms, dto.NumberOfPlatforms);
            AssertEqual(station.Location, dto.Location);
        }

        private void AssertEqual(RailwayPlatform platform, RailwayPlatformDto dto)
        {
            Assert.Equal(platform.Id, dto.Id);
            Assert.Equal(platform.Mark, dto.Mark);
            Assert.Equal(platform.Name, dto.Name);
            Assert.Equal(platform.RailwayStationId, dto.RailwayStationId);
        }

        private void AssertEqual(Location location, LocationDto dto)
        {
            Assert.Equal(location.Id, dto.Id);
            Assert.Equal(location.Name, dto.Name);
            AssertEqual(location.Country, dto.Country);
        }

        private void AssertEqual(Country country, CountryDto dto)
        {
            Assert.Equal(country.Code, dto.Code);
            Assert.Equal(country.Id, dto.Id);
            Assert.Equal(country.Name, dto.Name);
        }
    }
}