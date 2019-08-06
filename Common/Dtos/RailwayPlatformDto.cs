using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class RailwayPlatformDto
    {
        [DataMember]
        public EntranceType EntranceType { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Mark { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? RailwayStationId { get; set; }
    }
}