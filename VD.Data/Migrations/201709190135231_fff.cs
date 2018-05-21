namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Confs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 254, unicode: false),
                        Description = c.String(),
                        IsHtmlEditor = c.Boolean(nullable: false),
                        IsImg = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        Content = c.String(),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);
            
            CreateTable(
                "dbo.Counters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Langs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 150),
                        Flag = c.String(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        LogTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LogTypes", t => t.LogTypeId, cascadeDelete: true)
                .Index(t => t.LogTypeId);
            
            CreateTable(
                "dbo.LogExceptions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ExceptionMsg = c.String(),
                        ExceptionType = c.String(),
                        ExceptionSource = c.String(),
                        ExceptionURL = c.String(),
                        Logdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Logs", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.LogTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        UserStatusId = c.Int(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.UserStatus", t => t.UserStatusId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserStatusId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        sys_email = c.String(),
                        sys_email_robot = c.String(),
                        sys_email_robot_pw = c.String(),
                        smpt_port = c.Int(nullable: false),
                        smpt_host = c.String(),
                        smpt_enable_ssl = c.Boolean(nullable: false),
                        smpt_use_default_credentials = c.Boolean(nullable: false),
                        ddl_row_per_page = c.String(),
                        max_num_top_job = c.Int(nullable: false),
                        max_korean_candidates_hompage = c.Int(nullable: false),
                        row_per_page = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        is_reply_contact = c.Boolean(nullable: false),
                        etmp_reply_contact = c.String(),
                        etmp_reply_contact_title = c.String(),
                        etmp_reply_employer_register_content = c.String(),
                        etmp_reply_employer_register_title = c.String(),
                        etmp_reply_employer_register_content_send_admin = c.String(),
                        etmp_reply_employer_register_title_send_admin = c.String(),
                        etmp_reply_cadidate_register_content = c.String(),
                        etmp_reply_cadidate_register_title = c.String(),
                        etmp_reply_cadidate_register_content_send_admin = c.String(),
                        etmp_reply_cadidate_register_title_send_admin = c.String(),
                        etmp_reply_purchase_wallet_content = c.String(),
                        etmp_reply_purchase_wallet_title = c.String(),
                        etmp_statis_job_cs_title = c.String(),
                        etmp_statis_job_cs_content = c.String(),
                        fee_score_post_recruitment = c.Int(nullable: false),
                        fee_score_filter_profile = c.Int(nullable: false),
                        fee_score_label_urgent = c.Int(nullable: false),
                        fee_score_label_hot = c.Int(nullable: false),
                        fee_score_label_new = c.Int(nullable: false),
                        fee_score_label_top = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Permission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Permission_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Permission_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserStatusId", "dbo.UserStatus");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RememberMes", "UserID", "dbo.Users");
            DropForeignKey("dbo.RolePermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.RolePermissions", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Logs", "LogTypeId", "dbo.LogTypes");
            DropForeignKey("dbo.LogExceptions", "Id", "dbo.Logs");
            DropIndex("dbo.RolePermissions", new[] { "Permission_Id" });
            DropIndex("dbo.RolePermissions", new[] { "Role_Id" });
            DropIndex("dbo.RememberMes", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "UserStatusId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.LogExceptions", new[] { "Id" });
            DropIndex("dbo.Logs", new[] { "LogTypeId" });
            DropIndex("dbo.Confs", new[] { "Key" });
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Settings");
            DropTable("dbo.UserStatus");
            DropTable("dbo.RememberMes");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            DropTable("dbo.LogTypes");
            DropTable("dbo.LogExceptions");
            DropTable("dbo.Logs");
            DropTable("dbo.Langs");
            DropTable("dbo.Counters");
            DropTable("dbo.Confs");
        }
    }
}
