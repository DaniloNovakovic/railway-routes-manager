using System.Collections.Generic;

namespace Server.Core
{
    public class Route
    {
        public Route()
        {
            RailwayStations = new HashSet<RailwayStation>();
        }

        public int Id { get; set; }
        public string Mark { get; set; }
        public string Name { get; set; }
        public ICollection<RailwayStation> RailwayStations { get; set; }
    }
}