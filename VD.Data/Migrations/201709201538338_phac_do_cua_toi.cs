namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phac_do_cua_toi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhacDoCuaTois",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenPhacDo = c.String(),
                        Huyet = c.String(),
                        MoTa = c.String(),
                        UserId = c.Int(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhacDoCuaTois", "UserId", "dbo.Users");
            DropIndex("dbo.PhacDoCuaTois", new[] { "UserId" });
            DropTable("dbo.PhacDoCuaTois");
        }
    }
}
