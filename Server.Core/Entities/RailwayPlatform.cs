using System;
using Common;

namespace Server.Core
{
    public class RailwayPlatform : ILogical
    {
        public DateTime? DeletionDate { get; set; }
        public EntranceType EntranceType { get; set; }
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Name { get; set; }
        public RailwayStation RailwayStation { get; set; }
        public int? RailwayStationId { get; set; }
    }
}