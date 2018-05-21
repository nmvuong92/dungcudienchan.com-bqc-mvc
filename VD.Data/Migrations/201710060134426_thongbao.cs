namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thongbao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThongBaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(nullable: false),
                        NoiDung = c.String(nullable: false),
                        IsHienThi = c.Boolean(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ThongBaos");
        }
    }
}
