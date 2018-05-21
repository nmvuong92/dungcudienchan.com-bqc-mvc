using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels.Form;

namespace Web.Areas.Admin.ViewModels.MF
{
    public class ProductCRUD : BaseInput
    {
        [Display(Name = "Mã SP")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Use letters and number only please")]
        public string MaSP { get; set; }

        [Required]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
        [Display(Name = "Thông tin chi tiết")]
        [AllowHtml]
        public string Infomation { get; set; }
        [Display(Name = "Ảnh hiển thị")]
        public string ThumbnailImage { get; set; }
        [Display(Name = "Giá bán")]
        public decimal Price { get; set; }
        [Required]

        [Display(Name = "Danh mục")]
        public int ProductCatId { get; set; }


        public int? ImgTmpId { get; set; }

        [Display(Name = "Sản phẩm chính?")]
        public bool MainProduct { get; set; }
        [Display(Name = "Còn hàng?")]
        public bool ConHang { get; set; }
       
        [Display(Name = "Bán chạy?")]
        public bool IsBanChay { get; set; }

    }
}