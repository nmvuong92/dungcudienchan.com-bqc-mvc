using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.ViewModels
{
    public class ThanhToanVM
    {
        [Display(Name = "Danh xưng")]
        public string XungDanh { get; set; }


        [Display(Name = "Họ (*)")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public string Ho { get; set; }


        [Display(Name = "Tên (*)")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public string Ten { get; set; }


        [Display(Name = "Địa chỉ (*)")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public string DiaChi { get; set; }
     

        [Display(Name = "Điện thoại (*)")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public string SDT { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public string Email { get; set; }


        [Display(Name = "Tỉnh thành")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public int ProvinceId { get; set; }


        [Display(Name = "Phường xã")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public int WardId { get; set; }


        [Display(Name = "Quận huyện")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public int DistrictId { get; set; }

        //hình thức thanh toán
         [Display(Name = "Hình thức thanh toán")]
        public int HTTTID { get; set; }


        //
        public IEnumerable<SelectListItem> ddlProvince { get; set; }
        public IEnumerable<SelectListItem> ddlDistrict { get; set; }
        public IEnumerable<SelectListItem> ddlWard { get; set; }
        public IEnumerable<SelectListItem> ddlHttt { get; set; }
        public IEnumerable<SelectListItem> ddlXungDanh { get; set; }
    }

}