namespace Buycars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberAndStockToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "inStock", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "inStock");
            DropColumn("dbo.Products", "Number");
        }
    }
}
