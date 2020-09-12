namespace Buycars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCartProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Image = c.Binary(),
                        DateAdded = c.DateTime(nullable: false),
                        DateAddedToCart = c.DateTime(nullable: false),
                        IdentityUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUserId)
                .Index(t => t.IdentityUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartProducts", "IdentityUserId", "dbo.IdentityUsers");
            DropIndex("dbo.CartProducts", new[] { "IdentityUserId" });
            DropTable("dbo.CartProducts");
        }
    }
}
