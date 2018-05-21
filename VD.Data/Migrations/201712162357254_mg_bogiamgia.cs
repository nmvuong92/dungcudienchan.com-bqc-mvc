namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg_bogiamgia : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "IsGiamGia");
            DropColumn("dbo.Products", "GiamGiaCon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "GiamGiaCon", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Products", "IsGiamGia", c => c.Boolean(nullable: false));
        }
    }
}
