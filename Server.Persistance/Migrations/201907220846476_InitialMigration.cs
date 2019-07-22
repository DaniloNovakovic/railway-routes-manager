namespace Server.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.RailwayPlatforms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntranceType = c.Int(nullable: false),
                        Mark = c.String(),
                        Name = c.String(),
                        RailwayStationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RailwayStations", t => t.RailwayStationId)
                .Index(t => t.RailwayStationId);
            
            CreateTable(
                "dbo.RailwayStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(),
                        Name = c.String(),
                        NumberOfPlatforms = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mark = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteRailwayStations",
                c => new
                    {
                        Route_Id = c.Int(nullable: false),
                        RailwayStation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Route_Id, t.RailwayStation_Id })
                .ForeignKey("dbo.Routes", t => t.Route_Id, cascadeDelete: true)
                .ForeignKey("dbo.RailwayStations", t => t.RailwayStation_Id, cascadeDelete: true)
                .Index(t => t.Route_Id)
                .Index(t => t.RailwayStation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteRailwayStations", "RailwayStation_Id", "dbo.RailwayStations");
            DropForeignKey("dbo.RouteRailwayStations", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.RailwayPlatforms", "RailwayStationId", "dbo.RailwayStations");
            DropForeignKey("dbo.RailwayStations", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "CountryId", "dbo.Countries");
            DropIndex("dbo.RouteRailwayStations", new[] { "RailwayStation_Id" });
            DropIndex("dbo.RouteRailwayStations", new[] { "Route_Id" });
            DropIndex("dbo.RailwayStations", new[] { "LocationId" });
            DropIndex("dbo.RailwayPlatforms", new[] { "RailwayStationId" });
            DropIndex("dbo.Locations", new[] { "CountryId" });
            DropTable("dbo.RouteRailwayStations");
            DropTable("dbo.Users");
            DropTable("dbo.Routes");
            DropTable("dbo.RailwayStations");
            DropTable("dbo.RailwayPlatforms");
            DropTable("dbo.Locations");
            DropTable("dbo.Countries");
        }
    }
}
