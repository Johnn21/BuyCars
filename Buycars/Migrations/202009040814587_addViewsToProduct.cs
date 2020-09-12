namespace Buycars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addViewsToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Views", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Views");
        }
    }
}
