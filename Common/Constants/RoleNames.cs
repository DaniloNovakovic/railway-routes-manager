using System.Collections.Generic;

namespace Common
{
    public static class RoleNames
    {
        public const string Admin = "Administrator";
        public const string RegularUser = "RegularUser";

        public static IEnumerable<string> AsEnumerable()
        {
            return new List<string>
            {
                Admin,
                RegularUser
            };
        }
    }
}