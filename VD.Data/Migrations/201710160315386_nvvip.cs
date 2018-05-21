namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nvvip : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "NgayVip", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "NgayVip", c => c.DateTime(nullable: false));
        }
    }
}
