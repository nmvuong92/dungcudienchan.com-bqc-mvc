using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.Admin.ViewModels.MF
{
    public class ProductCatCRUD
    {
        public int Id { get; set; }
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        [Display(Name = "Mô tả danh mục")]
        public string Description { get; set; }

        [Display(Name = "Hiển thị trang sản phẩm?")]
        public bool ShowInProduct { get; set; }
        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }
    }
}