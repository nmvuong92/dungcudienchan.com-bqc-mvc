namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class del_Rememberme : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RememberMes", "UserID", "dbo.Users");
            DropIndex("dbo.RememberMes", new[] { "UserID" });
            DropTable("dbo.RememberMes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RememberMes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        CookieKey = c.String(),
                        CookieValue = c.String(),
                        Time = c.DateTime(),
                        TimeExpires = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.RememberMes", "UserID");
            AddForeignKey("dbo.RememberMes", "UserID", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
