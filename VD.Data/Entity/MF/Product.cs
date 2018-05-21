
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using effts;
using VD.Lib;

namespace VD.Data.Entity.MF
{
    public class Product : BaseEntity
    {
        [Display(Name = "Mã SP")]
        public string MaSP { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
        [Display(Name = "Thông tin chi tiết")]
        public string Infomation { get; set; }
        [Display(Name = "Ảnh hiển thị")]
        public string ThumbnailImage { get; set; }

        [Display(Name = "Giá bán")]
        public decimal? Price { get; set; }

        [Display(Name = "Danh mục")]
        public int ProductCatId { get; set; }
        [ForeignKey("ProductCatId")]
        public virtual ProductCat ProductCat { get; set; }


        //
        [Display(Name = "Ảnh")]
        public int? ImgTmpId { get; set; }
        [ForeignKey("ImgTmpId")]
        public virtual ImgTmp ImgTmp { get; set; }

        [Display(Name = "Là SP Chính?")]
        public bool MainProduct { get; set; }

        [Display(Name = "Còn hàng?")]
        public bool ConHang { get; set; }
       
        [Display(Name = "Bán chạy?")]
        public bool IsBanChay { get; set; }

        private static Random rd = new Random();
        public static Product[] seed()
        {

            List<Product> l = new List<Product>();
            for (int i = 10; i < 60; i++)
            {

                Product p = new Product();
                p.MaSP = myNumbers.RandomString(5);
                p.ConHang = true;
                p.Id = i;
                p.ImgTmpId = 1;
                p.ProductName = "Sản phẩm mới " + p.MaSP;
                p.ProductCatId = rd.Next(1, 5);
               

                p.ThumbnailImage = "/Content/dccd/assets/img/23-570x407.png";
                p.Price = rd.Next(1000, 250000);
              

                p.Infomation = "<b>Best product!</b> <br/>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quaerat quod voluptate consequuntur ad quasi, dolores obcaecati ex aliquid, dolor provident accusamus omnis dolorum ipsam";
                l.Add(p);
            }
            return l.ToArray();
        }
    }
}
