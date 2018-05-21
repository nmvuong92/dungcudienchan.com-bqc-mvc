namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_setting_mg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "etmp_thong_bao_dat_hang", c => c.String());
            AddColumn("dbo.Settings", "etmp_thong_bao_dat_hang_title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "etmp_thong_bao_dat_hang_title");
            DropColumn("dbo.Settings", "etmp_thong_bao_dat_hang");
        }
    }
}
