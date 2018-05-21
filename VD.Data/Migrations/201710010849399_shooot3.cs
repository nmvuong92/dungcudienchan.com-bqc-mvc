namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shooot3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LichSuGiaoDiches", "menhgia", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LichSuGiaoDiches", "menhgia", c => c.String());
        }
    }
}
