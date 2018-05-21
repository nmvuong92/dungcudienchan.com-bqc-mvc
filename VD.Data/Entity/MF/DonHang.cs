
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VD.Data.Entity.MF
{
    public class DonHang : BaseEntity
    {
        [Display(Name = "Danh xưng")]
        public string XungDanh { get; set; }

        [Display(Name = "Họ (*)")]
        public string Ho { get; set; }

        [Display(Name = "Tên (*)")]
        public string Ten { get; set; }

        [Display(Name = "Địa chỉ (*)")]
        public string DiaChi { get; set; }


        [Display(Name = "Điện thoại (*)")]
        public string SDT { get; set; }

        public string Email { get; set; }



        [Required]
        [Display(Name = "SL sản phẩm")]
        public int SoLuongSanPham { get; set; }
        [Required]
        public decimal TongTienHang { get; set; }

        [Display(Name = "Giá ship")]
        public decimal GiaShip { get; set; }


        [Required]
        public int PTVat { get; set; }
        [Required]
        [Display(Name = "Tiền VAT")]
        public decimal TienVat { get; set; }
        [Required]
        public decimal ThanhTien { get; set; }





        //hình thức thanh toán
        [Display(Name = "Hình thức thanh toán")]
        public int HTTTID { get; set; }
        [ForeignKey("HTTTID")]
        public virtual HTTT HTTT { get; set; }

        //chi tiết đơn hàng
        public virtual ICollection<CTDonHang> CTDonHangs { get; set; }


        [Display(Name = "Tỉnh thành")]
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }

        [Display(Name = "Phường xã")]
        public int WardId { get; set; }
        [ForeignKey("WardId")]
        public virtual Ward Ward { get; set; }

        [Display(Name = "Quận huyện")]
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }




        //trạng thái giao hàng
        [Display(Name = "Trạng thái giao hàng")]
        public int TrangThaiGiaoHangId { get; set; }
        [ForeignKey("TrangThaiGiaoHangId")]
        public virtual TrangThaiGiaoHang TrangThaiGiaoHang { get; set; }



        //trạng thái thanh toán
        [Display(Name = "Trạng thái thanh toán")]
        public int TrangThaiThanhToanId { get; set; }
        [ForeignKey("TrangThaiThanhToanId")]
        public virtual TrangThaiThanhToan TrangThaiThanhToan { get; set; }

        [Display(Name = "Ghi chú đơn hàng")]
        public string GhiChuDonHang { get; set; }


        //đơn hàng của user nào?
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Display(Name = "Đã gửi email?")]
        public bool IsDaGuiEmail { get; set; }
        [Display(Name = "Nội dung email")]
        public string NoiDungGuiEmail { get; set; }
    }
}
