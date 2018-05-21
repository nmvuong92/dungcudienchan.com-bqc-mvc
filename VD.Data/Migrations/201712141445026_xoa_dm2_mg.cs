namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xoa_dm2_mg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductCatId2", "dbo.ProductCats");
            DropIndex("dbo.Products", new[] { "ProductCatId2" });
            DropColumn("dbo.Products", "ProductCatId2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductCatId2", c => c.Int());
            CreateIndex("dbo.Products", "ProductCatId2");
            AddForeignKey("dbo.Products", "ProductCatId2", "dbo.ProductCats", "Id");
        }
    }
}
