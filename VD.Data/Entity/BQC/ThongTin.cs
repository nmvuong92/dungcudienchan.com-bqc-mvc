using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.Data.Entity.BQC
{
    public class ThongTin : BaseEntity
    {
        [Display(Name = "Tiêu đê")]
        public string TieuDe { get; set; }
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        public static ThongTin[] seed()
        {
            var l = new List<ThongTin>();
            l.Add(new ThongTin()
            {
                Id=1,
                TieuDe = "Thông tin huyệt BQC",
                NoiDung = "<br/>Nội dung</br><b>YAYAYA<b>"
            });
            return l.ToArray();
        }
    }
}
