namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shooot2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RolePermissions", newName: "PermissionRoles");
            DropPrimaryKey("dbo.PermissionRoles");
            CreateTable(
                "dbo.LichSuGiaoDiches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tengiaodich = c.String(),
                        menhgia = c.String(),
                        ngaygiaodich = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LichSuNapThes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tenthe = c.String(),
                        menhgia = c.Int(nullable: false),
                        ngay = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddPrimaryKey("dbo.PermissionRoles", new[] { "Permission_Id", "Role_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LichSuNapThes", "UserId", "dbo.Users");
            DropForeignKey("dbo.LichSuGiaoDiches", "UserId", "dbo.Users");
            DropIndex("dbo.LichSuNapThes", new[] { "UserId" });
            DropIndex("dbo.LichSuGiaoDiches", new[] { "UserId" });
            DropPrimaryKey("dbo.PermissionRoles");
            DropTable("dbo.LichSuNapThes");
            DropTable("dbo.LichSuGiaoDiches");
            AddPrimaryKey("dbo.PermissionRoles", new[] { "Role_Id", "Permission_Id" });
            RenameTable(name: "dbo.PermissionRoles", newName: "RolePermissions");
        }
    }
}
