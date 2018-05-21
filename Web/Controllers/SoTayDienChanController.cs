using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VD.Data;
using VD.Lib.DTO;
using Web.Models.BQC;
using Web.Security;

namespace Web.Controllers
{
   
    public class SoTayDienChanController : BaseController
    {
        // GET: SoTayDienChan
        public ActionResult Index()
        {
            List<DanhMucSoTayDienChanModel> model = new List<DanhMucSoTayDienChanModel>();

            var au = MySsAuthUsers.GetAuth();
            bool islock = au == null;
            if (au !=null && au.IsVip1 == false)
            {
                islock = true;
            }

            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 1,
                TenDanhMuc = "Thường dùng",
                SoTayDienChanModels = SoTayDienChanModel.getThuongDung(1),
                Lock = false
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 2,
                TenDanhMuc = "Vùng cơ quan",
                SoTayDienChanModels = SoTayDienChanModel.getVungCoQuan(2),
                Lock = islock
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 3,
                TenDanhMuc = "Tác dụng",
                SoTayDienChanModels = SoTayDienChanModel.getTacDung(3),
                Lock = islock
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 4,
                TenDanhMuc = "Triệu trứng cảm giác",
                SoTayDienChanModels = SoTayDienChanModel.getTrieuChungCamGiac(4),
                Lock = islock
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 5,
                TenDanhMuc = "Công thức diện chẩn đặc hiệu",
                SoTayDienChanModels = SoTayDienChanModel.getCTDCDacHieu(5),
                Lock = islock
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 6,
                TenDanhMuc = "Phác đồ đặc hiệu",
                SoTayDienChanModels = SoTayDienChanModel.getPhacDoDacHieu(6),
                Lock = islock
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 7,
                TenDanhMuc = "Phác đồ của tôi (*)",
                SoTayDienChanModels = new List<SoTayDienChanModel>(),
                Lock = islock
            });
            return View(model);
        }

        [HttpGet]
        public JsonResult ajaxGetByDanhMuc(int danhmucid)
        {
            var au = MySsAuthUsers.GetAuth();
            if (au == null && danhmucid != 1)
            {
                return Json(rs.F("Vui lòng đăng nhập để sử dụng đầy đủ chức năng Sổ tay diện chẩn!"),
                    JsonRequestBehavior.AllowGet);
            }
            if (au != null && au.IsVip1 == false && danhmucid != 1)
            {
                return Json(rs.F("Vui lòng nâng cấp tài khoản VIP để sử dụng đầy đủ chức năng Sổ tay diện chẩn!"),
                    JsonRequestBehavior.AllowGet);
            }

            List<DanhMucSoTayDienChanModel> model = new List<DanhMucSoTayDienChanModel>();
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 1,
                TenDanhMuc = "Thường dùng",
                SoTayDienChanModels = SoTayDienChanModel.getThuongDung(1)
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 2,
                TenDanhMuc = "Vùng cơ quan",
                SoTayDienChanModels = SoTayDienChanModel.getVungCoQuan(2)
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 3,
                TenDanhMuc = "Tác dụng",
                SoTayDienChanModels = SoTayDienChanModel.getTacDung(3)
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 4,
                TenDanhMuc = "Triệu trứng cảm giác",
                SoTayDienChanModels = SoTayDienChanModel.getTrieuChungCamGiac(4)
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 5,
                TenDanhMuc = "Công thức diện chẩn đặc hiệu",
                SoTayDienChanModels = SoTayDienChanModel.getCTDCDacHieu(5)
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 6,
                TenDanhMuc = "Phác đồ đặc hiệu",
                SoTayDienChanModels = SoTayDienChanModel.getPhacDoDacHieu(6)
            });
            model.Add(new DanhMucSoTayDienChanModel()
            {
                Id = 7,
                TenDanhMuc = "Phác đồ của tôi (*)",
                SoTayDienChanModels = new List<SoTayDienChanModel>()
            });


            if (danhmucid >= 1 && danhmucid <= 7)
            {

                var rs = model.FirstOrDefault(f => f.Id == danhmucid);
                if (danhmucid == 7)
                {
                    
                    vuong_cms_context __db = new vuong_cms_context();
                    int __id = 0;
                    rs.SoTayDienChanModels =
                        __db.PhacDoCuaToi.Where(w => w.UserId == au.ID).ToList().Select(s => new SoTayDienChanModel
                        {
                            Id = s.Id,
                            Ten = s.TenPhacDo,
                            GroupId = -1,
                            SoTayGroupModel = new SoTayGroupModel()
                            {
                                GroupId = -1,
                                TenGroup = "---"
                            },
                            DanhMucId = 7,
                            SL = 1
                        }).ToList();
                }
                return Json(rs, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult HienThiPhacDo(int id, int sl = 1, int stt = 1)
        {
            // do the thing that takes a long time
            if (sl == 1)
            {
                string search = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/h2m_" + id + ".png");
                if (System.IO.File.Exists(search))
                {
                    string grid = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/hai_mat_grid.png");
                    string back = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/hai_mat_back.png");

                    XuLyAnh xla = new XuLyAnh();
                    byte[] img = xla.BitmapToBytes(xla.Merge(new List<string>() { back, grid, search }));
                    return File(img, "image/png");
                }

            }
            else //>1
            {
                string search = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/h2m_" + id + "_" + stt + ".png");
                if (System.IO.File.Exists(search))
                {
                    string grid = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/hai_mat_grid.png");
                    string back = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/hai_mat_back.png");

                    XuLyAnh xla = new XuLyAnh();
                    byte[] img = xla.BitmapToBytes(xla.Merge(new List<string>() { back, grid, search }));
                    return File(img, "image/png");
                }
            }

            string no_result_path = System.Web.HttpContext.Current.Server.MapPath("~/Content/img/no-result.png");
            byte[] no_result = System.IO.File.ReadAllBytes(no_result_path);
            return File(no_result, "image/png");
        }
        [HttpGet]
        public ActionResult HienThiPhacDo7(int id)
        {
            vuong_cms_context __db = new vuong_cms_context();
            var phacdo = __db.PhacDoCuaToi.Find(id);
            List<string> huyet_split = phacdo.Huyet.Split(',').ToList();

            List<string> map = new List<string>();
            string grid = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/hai_mat_grid.png");
            string back = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/hai_mat_back.png");
            map.Add(grid);
            map.Add(back);
            foreach (var huyet in huyet_split)
            {
                ValidationHuyet __val = new ValidationHuyet();
                var lay_ten = __val.fnLayTenAnhPTCT(huyet);
                string search = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/"+ lay_ten+ ".png");
                if (System.IO.File.Exists(search))
                {
                    map.Add(search);
                }
               
            }
            
            XuLyAnh xla = new XuLyAnh();
            byte[] img = xla.BitmapToBytes(xla.Merge(map));
            return File(img, "image/png");
        }

        [HttpGet]
        public ContentResult PhacDoHtml(int id, int stt = 0)
        {
            string _rs = "";
            if (stt == 0)
            {
                string search_html_file =
                    System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc-html/h2m_" + id + ".html");
                if (System.IO.File.Exists(search_html_file))
                {
                    _rs = System.IO.File.ReadAllText(search_html_file);
                }
            }
            else
            {
                string search_html_file =
                    System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc-html/h2m_" + id + "_" + stt + ".html");
                if (System.IO.File.Exists(search_html_file))
                {
                    _rs = System.IO.File.ReadAllText(search_html_file);
                }
            }
            return Content(_rs);
        }

        [HttpGet]
        public JsonResult PhacDoCuaToiHtml(int id)
        {
            vuong_cms_context __db = new vuong_cms_context();
            var phacdo = __db.PhacDoCuaToi.Find(id);
            return Json(new { mota = phacdo.MoTa,tieude=phacdo.TenPhacDo},JsonRequestBehavior.AllowGet);
        }
    }
}