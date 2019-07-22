namespace Server.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirstAndLastNameColumnsToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
