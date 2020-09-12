namespace Buycars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Image", c => c.Binary(nullable: false));
        }
    }
}
