namespace Buycars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReview : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                        IdentityUserId = c.String(maxLength: 128),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUserId)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.IdentityUserId)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Reviews", "IdentityUserId", "dbo.IdentityUsers");
            DropIndex("dbo.Reviews", new[] { "Product_Id" });
            DropIndex("dbo.Reviews", new[] { "IdentityUserId" });
            DropTable("dbo.Reviews");
        }
    }
}
