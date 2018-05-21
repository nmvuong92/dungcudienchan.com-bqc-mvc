
using System;
using System.ComponentModel.DataAnnotations;

namespace VD.Data.Entity.BQC
{
    public class ThongBao:BaseEntity
    {
        [Required]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }
        [Required]
        [Display(Name="Nội dung")]
        public string NoiDung { get; set; }
        [Display(Name = "Hiển thị trên web?")]
        public bool IsHienThi { get; set; }
       

        public static ThongBao[] seed()
        {
            return new ThongBao[]
            {
                new ThongBao()
                {
                    Id=1,
                    TieuDe = "Tiêu đề",
                    NoiDung = "<img src='http://txdaknong.daknong.edu.vn/wp-content/uploads/2016/11/th--ng-b--o-chieu-sinh-c--c-l---p-ng--n-h---n-copy.jpg'/>",
                    IsHienThi = true
                },
            };
        }
    }
}
