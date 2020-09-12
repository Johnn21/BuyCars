namespace Buycars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Req : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "IdentityUserId", "dbo.IdentityUsers");
            DropIndex("dbo.Products", new[] { "IdentityUserId" });
            AlterColumn("dbo.Products", "IdentityUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "IdentityUserId");
            AddForeignKey("dbo.Products", "IdentityUserId", "dbo.IdentityUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "IdentityUserId", "dbo.IdentityUsers");
            DropIndex("dbo.Products", new[] { "IdentityUserId" });
            AlterColumn("dbo.Products", "IdentityUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Products", "IdentityUserId");
            AddForeignKey("dbo.Products", "IdentityUserId", "dbo.IdentityUsers", "Id", cascadeDelete: true);
        }
    }
}
