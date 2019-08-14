namespace Server.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRouteConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Routes", "Mark", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Routes", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Routes", "Name", c => c.String());
            AlterColumn("dbo.Routes", "Mark", c => c.String());
        }
    }
}
