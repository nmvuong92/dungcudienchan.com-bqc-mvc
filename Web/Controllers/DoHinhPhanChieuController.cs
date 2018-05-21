using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.BQC;
using Web.Security;

namespace Web.Controllers
{
  
    public class DoHinhPhanChieuController : BaseController
    {
        // GET: DoHinhPhanChieu
        [HttpGet]
        public string LayChuThich(string ten_file_html)
        {
            string _rs = "";
            string search_html_file =
                 System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc-html/" + ten_file_html);
            if (System.IO.File.Exists(search_html_file))
            {
                _rs = System.IO.File.ReadAllText(search_html_file);
            }
            return _rs;
        }
        public ActionResult Index()
        {
            List<DoHinhPhanChieuModel> model = new List<DoHinhPhanChieuModel>()
            {
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_1.jpg",FullImage = "/Content/bqc/newdhinh_1.jpg",Title = "1. ĐỒ HÌNH PHẢN CHIẾU THÁI CỰC ĐỒ TRÊN MẶT"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_2.jpg",FullImage = "/Content/bqc/newdhinh_2.jpg",Title = "2. ĐỒ HÌNH PHẢN CHIẾU NGOẠI VI CƠ THỂ TRÊN MẶT"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_3.jpg",FullImage = "/Content/bqc/newdhinh_3.jpg",Title = "3. ĐỒ HÌNH PHẢN CHIẾU NGOẠI VI CƠ THỂ TRÊN TRÁN & TAI"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_4.jpg",FullImage = "/Content/bqc/newdhinh_4.jpg",Title = "4. ĐỒ HÌNH PHẢN CHIẾU NGOẠI VI CƠ THỂ TRÊN MẶT & TRÊN DA ĐẦU"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_5.jpg",FullImage = "/Content/bqc/newdhinh_5.jpg",Title = "5. ĐỒ HÌNH PHẢN CHIẾU NỘI TẠNG TRÊN MẶT"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_6.jpg",FullImage = "/Content/bqc/newdhinh_6.jpg",Title = "6. ĐỒ HÌNH PHẢN CHIẾU CỘT SỐNG TRÊN MẶT"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_7.jpg",FullImage = "/Content/bqc/newdhinh_7.jpg",Title = "7. ĐỒ HÌNH PHẢN CHIẾU TIM & NÃO TRÊN MẶT"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_8.jpg",FullImage = "/Content/bqc/newdhinh_8.jpg",Title = "8. ĐỒ HÌNH PHẢN CHIẾU NÃO BỘ TRÊN MẶT"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_9.jpg",FullImage = "/Content/bqc/newdhinh_9.jpg",Title = "9. ĐỒ HÌNH PHẢN CHIẾU CƠ QUAN SINH DỤC NAM TRÊN MẶT"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_10.jpg",FullImage = "/Content/bqc/newdhinh_10.jpg",Title = "10. ĐỒ HÌNH PHẢN CHIẾU CƠ QUAN SINH DỤC NỮ TRÊN MẶT"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_11.jpg",FullImage = "/Content/bqc/newdhinh_11.jpg",Title = "11. ĐỒ HÌNH PHẢN CHIẾU BÀN TAY MỞ TRÊN MẶT VÀ PHẢN CHIẾU NGOẠI VI TRÊN BÀN TAY"},
                new DoHinhPhanChieuModel(){
                    Icon = "/Content/bqc/inewdhinh_12.jpg",
                    FullImage = "/Content/bqc/newdhinh_12.jpg",
                    Title = "12. PHÁC ĐỒ PHẢN CHIẾU 12 ĐÔI DÂY THẦN KINH SỌ NÃO (chú thích)",
                    ChuThich = "html_do_hinh_0.html"
                },
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_13.jpg",FullImage = "/Content/bqc/newdhinh_13.jpg",Title = "13. ĐỒ HÌNH PHẢN CHIẾU BÀN CHÂN TRÊN MẶT VÀ NGOẠI VI CƠ THỂ TRÊN BÀN CHÂN"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_14.jpg",FullImage = "/Content/bqc/newdhinh_14.jpg",Title = "14. ĐỒ HÌNH PHẢN CHIẾU LOA TAI TRÊN MẶT & NGOẠI VI CƠ THỂ LÊN LOA TAI"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_15.jpg",FullImage = "/Content/bqc/newdhinh_15.jpg",Title = "15. ĐỒ HÌNH PHẢN CHIẾU BÀN TAY NẮM VÀ NGOẠI VI CƠ THỂ TRÊN LƯNG"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_16.jpg",FullImage = "/Content/bqc/newdhinh_16.jpg",Title = "16. ĐỒ HÌNH PHẢN CHIẾU BÀN CHÂN VÀ NGOẠI VI CƠ THỂ TRÊN LƯNG"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_17.jpg",FullImage = "/Content/bqc/newdhinh_17.jpg",Title = "17. ĐỒ HÌNH PHẢN CHIẾU NGOẠI VI CƠ THỂ TRÊN NGỰC & BỤNG"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_18.jpg",FullImage = "/Content/bqc/newdhinh_18.jpg",Title = "18. ĐỒ HÌNH PHẢN CHIẾU BÀN TAY NẮM TRÊN MẶT VÀ PHẢN CHIẾU NỘI TẠNG TRÊN BÀN TAY"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_19.jpg",FullImage = "/Content/bqc/newdhinh_19.jpg",Title = "19. ĐỒ HÌNH PHẢN CHIẾU BÀN CHÂN TRÊN MẶT VÀ PHẢN CHIẾU NỘI TẠNG TRÊN BÀN CHÂN"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_20.jpg",FullImage = "/Content/bqc/newdhinh_20.jpg",Title = "20. ĐỒ HÌNH PHẢN CHIẾU LOA TAI TRÊN MẶT VÀ PHẢN CHIẾU NỘI TẠNG TRÊN LOA TAI"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_21.jpg",FullImage = "/Content/bqc/newdhinh_21.jpg",Title = "21. ĐỒ HÌNH PHẢN CHIẾU BÀN TAY NẮM VÀ NỘI TẠNG CƠ THỂ TRÊN LƯNG"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_22.jpg",FullImage = "/Content/bqc/newdhinh_22.jpg",Title = "22. ĐỒ HÌNH PHẢN CHIẾU BÀN CHÂN VÀ NỘI TẠNG CƠ THỂ TRÊN LƯNG"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_23.jpg",FullImage = "/Content/bqc/newdhinh_23.jpg",Title = "23. ĐỒ HÌNH PHẢN CHIẾU NGOẠI VI CƠ THỂ TRÊN MẶT (NGƯỢC CHIỀU)"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_24.jpg",FullImage = "/Content/bqc/newdhinh_24.jpg",Title = "24. ĐỒ HÌNH PHẢN CHIẾU NGOẠI VI CƠ THỂ TRÊN MẶT (NGƯỢC CHIỀU)"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_25.jpg",FullImage = "/Content/bqc/newdhinh_25.jpg",Title = "25. ĐỒ HÌNH PHẢN CHIẾU NGOẠI VI CƠ THỂ TRÊN MẶT (NGƯỢC CHIỀU)"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_26.jpg",FullImage = "/Content/bqc/newdhinh_26.jpg",Title = "26. HỒ HÌNH PHẢN CHIẾU CẲNG CHÂN LÊN MẶT (NGƯỢC CHIỀU)"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_27.jpg",FullImage = "/Content/bqc/newdhinh_27.jpg",Title = "27. ĐỒ HÌNH PHẢN CHIẾU NGỌA TÀM TRÊN MẶT"},
                new DoHinhPhanChieuModel(){Icon = "/Content/bqc/inewdhinh_28.jpg",FullImage = "/Content/bqc/newdhinh_28.jpg",Title = "28. ĐỒ HÌNH PHẢN CHIẾU HỆ HÔ HẤP (NGƯỢC CHIỀU)"}
              

            };

            return View(model);
        }
    }
}