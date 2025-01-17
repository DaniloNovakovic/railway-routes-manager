﻿using System;
using System.Collections.Generic;

namespace Server.Core
{
    public class RailwayStation : ILogical
    {
        public RailwayStation()
        {
            Routes = new HashSet<Route>();
            RailwayPlatforms = new HashSet<RailwayPlatform>();
        }

        public DateTime? DeletionDate { get; set; }
        public int Id { get; set; }
        public Location Location { get; set; }
        public int? LocationId { get; set; }
        public string Name { get; set; }
        public int NumberOfPlatforms { get; set; }
        public ICollection<RailwayPlatform> RailwayPlatforms { get; set; }
        public ICollection<Route> Routes { get; set; }
    }
}