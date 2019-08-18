using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class RouteDto
    {
        public RouteDto()
        {
            RailwayStations = new List<RailwayStationDto>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Mark { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<RailwayStationDto> RailwayStations { get; set; }
    }
}