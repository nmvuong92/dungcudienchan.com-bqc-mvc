namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_user_mg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "XungDanh", c => c.String());
            AddColumn("dbo.Users", "Ho", c => c.String());
            AddColumn("dbo.Users", "Ten", c => c.String());
            AddColumn("dbo.Users", "ProvinceId", c => c.Int());
            AddColumn("dbo.Users", "WardId", c => c.Int());
            AddColumn("dbo.Users", "DistrictId", c => c.Int());
            CreateIndex("dbo.Users", "ProvinceId");
            CreateIndex("dbo.Users", "WardId");
            CreateIndex("dbo.Users", "DistrictId");
            AddForeignKey("dbo.Users", "DistrictId", "dbo.District", "Id");
            AddForeignKey("dbo.Users", "ProvinceId", "dbo.Province", "Id");
            AddForeignKey("dbo.Users", "WardId", "dbo.Ward", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "WardId", "dbo.Ward");
            DropForeignKey("dbo.Users", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Users", "DistrictId", "dbo.District");
            DropIndex("dbo.Users", new[] { "DistrictId" });
            DropIndex("dbo.Users", new[] { "WardId" });
            DropIndex("dbo.Users", new[] { "ProvinceId" });
            DropColumn("dbo.Users", "DistrictId");
            DropColumn("dbo.Users", "WardId");
            DropColumn("dbo.Users", "ProvinceId");
            DropColumn("dbo.Users", "Ten");
            DropColumn("dbo.Users", "Ho");
            DropColumn("dbo.Users", "XungDanh");
        }
    }
}
