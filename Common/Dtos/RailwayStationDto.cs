using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class RailwayStationDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public LocationDto Location { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int NumberOfPlatforms { get; set; }
    }
}