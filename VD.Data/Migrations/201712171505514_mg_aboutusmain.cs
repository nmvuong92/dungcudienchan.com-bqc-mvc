namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg_aboutusmain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "AboutUsMainId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "AboutUsMainId");
        }
    }
}
