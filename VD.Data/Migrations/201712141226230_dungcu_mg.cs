namespace VD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dungcu_mg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SEOTitle = c.String(),
                        Content = c.String(),
                        Description = c.String(),
                        ImageThumbnail = c.String(),
                        CategoryId = c.Int(nullable: false),
                        LuotXem = c.Int(nullable: false),
                        FontAwesomeIcon = c.String(),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Catalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Url = c.String(),
                        Target = c.String(),
                        ThuTu = c.String(),
                        HinhAnh = c.String(nullable: false),
                        CssClassDiv = c.String(),
                        CssClass = c.String(),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HoTen = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        TieuDe = c.String(nullable: false),
                        NoiDung = c.String(nullable: false),
                        SDT = c.String(),
                        DiaChi = c.String(),
                        CompanyName = c.String(),
                        GhiChu = c.String(),
                        KetQuaGuiMail = c.Boolean(nullable: false),
                        LoiGuiMail = c.String(),
                        ContactStatusId = c.Int(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactStatus", t => t.ContactStatusId, cascadeDelete: true)
                .Index(t => t.ContactStatusId);
            
            CreateTable(
                "dbo.ContactStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Html = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CTDonHangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SanPhamId = c.Int(nullable: false),
                        TenSanPham = c.String(),
                        HinhAnh = c.String(),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Link = c.String(),
                        ThanhTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DonHangID = c.Int(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DonHangs", t => t.DonHangID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.SanPhamId, cascadeDelete: true)
                .Index(t => t.SanPhamId)
                .Index(t => t.DonHangID);
            
            CreateTable(
                "dbo.DonHangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XungDanh = c.String(),
                        Ho = c.String(),
                        Ten = c.String(),
                        DiaChi = c.String(),
                        SDT = c.String(),
                        Email = c.String(),
                        SoLuongSanPham = c.Int(nullable: false),
                        TongTienHang = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GiaShip = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PTVat = c.Int(nullable: false),
                        TienVat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ThanhTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HTTTID = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        WardId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        TrangThaiGiaoHangId = c.Int(nullable: false),
                        TrangThaiThanhToanId = c.Int(nullable: false),
                        GhiChuDonHang = c.String(),
                        UserId = c.Int(),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.District", t => t.DistrictId, cascadeDelete: true)
                .ForeignKey("dbo.HTTTs", t => t.HTTTID, cascadeDelete: true)
                .ForeignKey("dbo.Province", t => t.ProvinceId, cascadeDelete: true)
                .ForeignKey("dbo.TrangThaiGiaoHangs", t => t.TrangThaiGiaoHangId, cascadeDelete: true)
                .ForeignKey("dbo.TrangThaiThanhToans", t => t.TrangThaiThanhToanId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Ward", t => t.WardId, cascadeDelete: true)
                .Index(t => t.HTTTID)
                .Index(t => t.ProvinceId)
                .Index(t => t.WardId)
                .Index(t => t.DistrictId)
                .Index(t => t.TrangThaiGiaoHangId)
                .Index(t => t.TrangThaiThanhToanId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Type = c.String(maxLength: 50),
                        LatiLongTude = c.String(maxLength: 50),
                        ProvinceId = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        IsPublished = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        GiaShip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Province", t => t.ProvinceId, cascadeDelete: false)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Type = c.String(maxLength: 20),
                        TelephoneCode = c.Int(),
                        ZipCode = c.String(maxLength: 20),
                        CountryCode = c.String(maxLength: 2),
                        SortOrder = c.Int(),
                        IsPublished = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        GiaShip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ward",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.String(maxLength: 50),
                        LatiLongTude = c.String(maxLength: 50),
                        DistrictID = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        IsPublished = c.Boolean(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.District", t => t.DistrictID, cascadeDelete: false)
                .Index(t => t.DistrictID);
            
            CreateTable(
                "dbo.HTTTs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrangThaiGiaoHangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                        CssLabel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrangThaiThanhToans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                        CssLabel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Infomation = c.String(),
                        ThumbnailImage = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        ProductCatId = c.Int(nullable: false),
                        ProductCatId2 = c.Int(),
                        ImgTmpId = c.Int(),
                        MainProduct = c.Boolean(nullable: false),
                        ConHang = c.Boolean(nullable: false),
                        IsGiamGia = c.Boolean(nullable: false),
                        GiamGiaCon = c.Decimal(precision: 18, scale: 2),
                        IsBanChay = c.Boolean(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImgTmps", t => t.ImgTmpId)
                .ForeignKey("dbo.ProductCats", t => t.ProductCatId, cascadeDelete: true)
                .ForeignKey("dbo.ProductCats", t => t.ProductCatId2)
                .Index(t => t.ProductCatId)
                .Index(t => t.ProductCatId2)
                .Index(t => t.ImgTmpId);
            
            CreateTable(
                "dbo.ImgTmps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImgTmpDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullImage = c.String(),
                        Thumbnail = c.String(),
                        IsMain = c.Boolean(nullable: false),
                        ImgTmpId = c.Int(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImgTmps", t => t.ImgTmpId, cascadeDelete: true)
                .Index(t => t.ImgTmpId);
            
            CreateTable(
                "dbo.ProductCats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ShowInProduct = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        Order = c.Int(nullable: false),
                        IsSys = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCats", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CTDonHangs", "SanPhamId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCatId2", "dbo.ProductCats");
            DropForeignKey("dbo.Products", "ProductCatId", "dbo.ProductCats");
            DropForeignKey("dbo.ProductCats", "ParentId", "dbo.ProductCats");
            DropForeignKey("dbo.Products", "ImgTmpId", "dbo.ImgTmps");
            DropForeignKey("dbo.ImgTmpDetails", "ImgTmpId", "dbo.ImgTmps");
            DropForeignKey("dbo.DonHangs", "WardId", "dbo.Ward");
            DropForeignKey("dbo.DonHangs", "UserId", "dbo.Users");
            DropForeignKey("dbo.DonHangs", "TrangThaiThanhToanId", "dbo.TrangThaiThanhToans");
            DropForeignKey("dbo.DonHangs", "TrangThaiGiaoHangId", "dbo.TrangThaiGiaoHangs");
            DropForeignKey("dbo.DonHangs", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.DonHangs", "HTTTID", "dbo.HTTTs");
            DropForeignKey("dbo.DonHangs", "DistrictId", "dbo.District");
            DropForeignKey("dbo.Ward", "DistrictID", "dbo.District");
            DropForeignKey("dbo.District", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.CTDonHangs", "DonHangID", "dbo.DonHangs");
            DropForeignKey("dbo.Contacts", "ContactStatusId", "dbo.ContactStatus");
            DropForeignKey("dbo.Articles", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ProductCats", new[] { "ParentId" });
            DropIndex("dbo.ImgTmpDetails", new[] { "ImgTmpId" });
            DropIndex("dbo.Products", new[] { "ImgTmpId" });
            DropIndex("dbo.Products", new[] { "ProductCatId2" });
            DropIndex("dbo.Products", new[] { "ProductCatId" });
            DropIndex("dbo.Ward", new[] { "DistrictID" });
            DropIndex("dbo.District", new[] { "ProvinceId" });
            DropIndex("dbo.DonHangs", new[] { "UserId" });
            DropIndex("dbo.DonHangs", new[] { "TrangThaiThanhToanId" });
            DropIndex("dbo.DonHangs", new[] { "TrangThaiGiaoHangId" });
            DropIndex("dbo.DonHangs", new[] { "DistrictId" });
            DropIndex("dbo.DonHangs", new[] { "WardId" });
            DropIndex("dbo.DonHangs", new[] { "ProvinceId" });
            DropIndex("dbo.DonHangs", new[] { "HTTTID" });
            DropIndex("dbo.CTDonHangs", new[] { "DonHangID" });
            DropIndex("dbo.CTDonHangs", new[] { "SanPhamId" });
            DropIndex("dbo.Contacts", new[] { "ContactStatusId" });
            DropIndex("dbo.Articles", new[] { "CategoryId" });
            DropTable("dbo.ProductCats");
            DropTable("dbo.ImgTmpDetails");
            DropTable("dbo.ImgTmps");
            DropTable("dbo.Products");
            DropTable("dbo.TrangThaiThanhToans");
            DropTable("dbo.TrangThaiGiaoHangs");
            DropTable("dbo.HTTTs");
            DropTable("dbo.Ward");
            DropTable("dbo.Province");
            DropTable("dbo.District");
            DropTable("dbo.DonHangs");
            DropTable("dbo.CTDonHangs");
            DropTable("dbo.ContactStatus");
            DropTable("dbo.Contacts");
            DropTable("dbo.Catalogs");
            DropTable("dbo.Categories");
            DropTable("dbo.Articles");
        }
    }
}
