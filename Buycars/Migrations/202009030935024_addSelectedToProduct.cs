namespace Buycars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSelectedToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "isSelected", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "isSelected");
        }
    }
}
