using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class RailwayStationDto
    {
        public RailwayStationDto()
        {
            RailwayPlatforms = new List<RailwayPlatformDto>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public LocationDto Location { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int NumberOfPlatforms { get; set; }

        [DataMember]
        public List<RailwayPlatformDto> RailwayPlatforms { get; set; }
    }
}