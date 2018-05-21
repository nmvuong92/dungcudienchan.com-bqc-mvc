namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chuthichvup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ChuThichVip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ChuThichVip");
        }
    }
}
