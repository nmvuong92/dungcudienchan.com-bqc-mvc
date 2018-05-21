namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg_guiemaildonhang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHangs", "IsDaGuiEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.DonHangs", "NoiDungGuiEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHangs", "NoiDungGuiEmail");
            DropColumn("dbo.DonHangs", "IsDaGuiEmail");
        }
    }
}
