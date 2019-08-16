namespace Server.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletionDateColumnToRouteStationAndPlatformTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RailwayPlatforms", "DeletionDate", c => c.DateTime());
            AddColumn("dbo.RailwayStations", "DeletionDate", c => c.DateTime());
            AddColumn("dbo.Routes", "DeletionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "DeletionDate");
            DropColumn("dbo.RailwayStations", "DeletionDate");
            DropColumn("dbo.RailwayPlatforms", "DeletionDate");
        }
    }
}
