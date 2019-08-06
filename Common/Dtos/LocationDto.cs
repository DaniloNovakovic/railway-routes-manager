using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class LocationDto
    {
        [DataMember]
        public CountryDto Country { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}