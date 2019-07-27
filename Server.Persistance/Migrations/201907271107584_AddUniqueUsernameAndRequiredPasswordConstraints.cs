namespace Server.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueUsernameAndRequiredPasswordConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Users", "Username", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Username" });
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
        }
    }
}
