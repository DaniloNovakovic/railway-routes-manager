using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Password { get; set; } // empty if unauthorized

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
}