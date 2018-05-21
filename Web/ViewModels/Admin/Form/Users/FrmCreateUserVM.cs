using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels.Admin.Form.Users
{
    public class FrmCreateUserVM
    {
        public int ID { get; set; }
        [Required]
        [Display(Name="Tài khoản")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }
        [Required]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Số điện thoại")]
        public string DienThoai { get; set; }
        [Display(Name = "Ngày đăng ký")]
        public DateTime? NgayDK { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }
        [Display(Name = "Quyền hạn")]
        public int RoleID { get; set; }
        public bool? isDeleted { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }
        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        //
        [Display(Name = "Thành viên vip")]
        public bool IsVip{ get; set; }
    }
}