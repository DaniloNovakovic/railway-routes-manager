using System.Data.Entity.ModelConfiguration;
using Server.Core;

namespace Server.Persistance.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username).HasMaxLength(50).IsRequired();
            HasIndex(u => u.Username).IsUnique();
            Property(u => u.Password).HasMaxLength(50).IsRequired();
        }
    }
}