using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Runtime.InteropServices;

namespace VD.Data.Entity.MF
{
    public class ProductCat : BaseEntity
    {
        public ProductCat()
        {
            this.ParentId = null;
            this.Order = 1;
            this.Products = new Collection<Product>();
        }
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        [Display(Name = "Mô tả danh mục")]
        public string Description { get; set; }
        [Display(Name = "Hiển thị danh sách sản phẩm?")]
        public bool ShowInProduct { get; set; }
        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }
        [Display(Name = "Thứ tự")]
        public int Order { get; set; }

        [ForeignKey("ParentId")]
        public virtual ProductCat Parent { get; set; }

        public virtual ICollection<ProductCat> Cats { get; set; }

        [InverseProperty("ProductCat")]

        public virtual ICollection<Product> Products { get; set; }



        public static ProductCat[] seed()
        {
            var parent = new List<ProductCat>();
            parent.Add(new ProductCat()
            {
                Id = 1,
                Name = "Dụng cụ diện chẩn",
                ShowInProduct = true,
                Description = "Mô tả danh mục"

            });
            parent.Add(new ProductCat()
            {
                Id = 2,
                Name = "Sách diện chẩn",
                ShowInProduct = true,
                Description = "Mô tả danh mục"
            });
            parent.Add(new ProductCat()
            {
                Id = 3,
                Name = "Sản phẩm khác",
                Description = "Mô tả danh mục"
            });
           
            parent.Add(new ProductCat()
            {
                Id = 4,
                Name = "Khóa học",
                ParentId = 3,
                Description = "Mô tả danh mục"
            });
            return parent.ToArray();
        }
    }
}
