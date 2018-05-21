namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thongtinggg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThongTins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(),
                        NoiDung = c.String(),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ThongTins");
        }
    }
}
