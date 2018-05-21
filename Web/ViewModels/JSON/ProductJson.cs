using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.JSON
{
    public class DMJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public List<ProductJson> SPS { get; set; } 

        
    }
    public class ProductJson
    {
        public int Id { get; set; }
        public string MaSP { get; set; }
        public string ProductName { get; set; }
        public string Infomation { get; set; }
        public string ThumbnailImage { get; set; }
        public decimal Price { get; set; }
        public int ProductCatId { get; set; }
        public string DanhMuc { get; set; }
        public List<Img> HinhAnhs { get; set; }
    
        public bool MainProduct { get; set; }
      
        public bool ConHang { get; set; }
        public bool IsGiamGia { get; set; }
        public decimal GiamGiaCon { get; set; }
        public bool IsBanChay { get; set; }
        public int SLGioHang { get; set; }
      
    }

    public class Img
    {
        public string Thumb { get; set; }
        public string Full { get; set; }
    }
}