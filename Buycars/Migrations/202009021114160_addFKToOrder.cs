namespace Buycars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFKToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IdentityUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "IdentityUserId");
            AddForeignKey("dbo.Orders", "IdentityUserId", "dbo.IdentityUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "IdentityUserId", "dbo.IdentityUsers");
            DropIndex("dbo.Orders", new[] { "IdentityUserId" });
            DropColumn("dbo.Orders", "IdentityUserId");
        }
    }
}
