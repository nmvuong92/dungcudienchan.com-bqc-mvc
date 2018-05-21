namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_masp_mg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MaSP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "MaSP");
        }
    }
}
