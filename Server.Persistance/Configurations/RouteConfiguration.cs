using System.Data.Entity.ModelConfiguration;
using Server.Core;

namespace Server.Persistance.Configurations
{
    public class RouteConfiguration : EntityTypeConfiguration<Route>
    {
        public RouteConfiguration()
        {
            Property(r => r.Name).HasMaxLength(150).IsRequired();
            Property(r => r.Mark).HasMaxLength(20).IsRequired();
            HasMany(r => r.RailwayStations).WithMany(s => s.Routes);
        }
    }
}