namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Phone", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
            AddColumn("dbo.Users", "Fullname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Fullname");
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "Phone");
        }
    }
}
