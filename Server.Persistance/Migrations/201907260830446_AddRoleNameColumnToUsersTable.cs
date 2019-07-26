namespace Server.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleNameColumnToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RoleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RoleName");
        }
    }
}
