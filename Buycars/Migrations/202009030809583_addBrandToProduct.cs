namespace Buycars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBrandToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Brand", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Brand");
        }
    }
}
