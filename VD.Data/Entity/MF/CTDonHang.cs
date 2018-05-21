
using System.ComponentModel.DataAnnotations.Schema;

namespace VD.Data.Entity.MF
{
    public class CTDonHang:BaseEntity
    {
        public int SanPhamId { get; set; }
        [ForeignKey("SanPhamId")]
        public virtual Product SanPham { get; set; }

        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string Link { get; set; }
        public decimal ThanhTien { get; set; }

        //donhang
        public int DonHangID { get; set; }
        [ForeignKey("DonHangID")]
        public virtual DonHang DonHang { get; set; }


    }
}
