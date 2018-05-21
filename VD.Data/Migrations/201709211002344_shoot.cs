namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoot : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PhacDoCuaTois", "TenPhacDo", c => c.String(nullable: false));
            AlterColumn("dbo.PhacDoCuaTois", "Huyet", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PhacDoCuaTois", "Huyet", c => c.String());
            AlterColumn("dbo.PhacDoCuaTois", "TenPhacDo", c => c.String());
        }
    }
}
