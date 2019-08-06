using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class CountryDto
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}