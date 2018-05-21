using System.Collections.Generic;
using System.Web.Mvc;
using VD.Lib.DTO;
using Web.Security;
using Web.ViewModels.BQC;

namespace Web.Controllers
{
  
    public class HuyetBQCController : BaseController
    {
        // GET: HuyetBQC
        public ActionResult Index(int huyet=-1, bool grid = true, string loai = "")
        {
            HuyetBQCSearchModel model = new HuyetBQCSearchModel();
            model.huyet = huyet;
            model.grid = grid;
            model.loai = loai;
            model.somat = 2;
            model.mota = "";
            model.loai = "mt";
            if (model.huyet != -1)
            {
                int somat = 0;
                string _loai = "mt";
                string search = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mt_" + huyet + ".png");
                string search2 = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mc_" + huyet + ".png");
                if (System.IO.File.Exists(search))
                {
                    somat++;
                    _loai = "mt";
                }
                if (System.IO.File.Exists(search2))
                {
                    somat++;
                    _loai = "mc";
                }
                if (somat == 2)
                {
                    _loai = "mt";
                }
                string mota = "";
                if (somat != 0)
                {
                    string search_html_file = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc-html/mt_" + huyet + ".html");
                    if (System.IO.File.Exists(search_html_file))
                    {
                        mota = System.IO.File.ReadAllText(search_html_file);
                    }

                }
                model.somat = somat;
                model.mota = mota;
                model.loai = _loai;
            }
            if (!string.IsNullOrWhiteSpace(loai))
            {
                model.loai = loai;
            }
            return View(model);
        }
        [HttpGet]
        public JsonResult CheckHuyet(int huyet)
        {
            int somat = 0;
            string loai = "mt";
            string search = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mt_" + huyet + ".png");
            string search2 = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mc_" + huyet + ".png");
            if (System.IO.File.Exists(search))
            {
                somat++;
                loai = "mt";
            }
            if (System.IO.File.Exists(search2))
            {
                somat++;
                loai = "mc";
            }
            if (somat == 2)
            {
                loai = "mt";
            }
            string mota = "";
            if (somat != 0)
            {
                string search_html_file = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc-html/mt_" + huyet + ".html");
                if (System.IO.File.Exists(search_html_file))
                {
                    mota = System.IO.File.ReadAllText(search_html_file);
                }
                
            }
            return Json(rs.T("Ok", new { somat, loai, mota }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Default(bool grid = true,string loai="mt")
        {
            XuLyAnh xla = new XuLyAnh();
            if (loai == "mt")
            {
                string back_grid = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mt_back_grid.png");
                string back = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mt_back.png");
                string hhh = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mt_full_huyet.png");
                string nen = grid ? back_grid : back;
                byte[] img = xla.BitmapToBytes(xla.Merge(new List<string>() { nen, hhh }));
                return File(img, "image/png");
            }
            else
            {
                string back_grid = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mc_back_grid.png");
                string back = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mc_back.png");
                string hhh = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mc_full_huyet.png");
                string nen = grid ? back_grid : back;
                byte[] img = xla.BitmapToBytes(xla.Merge(new List<string>() { nen, hhh }));
                return File(img, "image/png");
            }
          
        }
        public ActionResult Test(int huyet, bool grid = false,string loai="")
        {
            XuLyAnh xla = new XuLyAnh();


            string search = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mt_" + huyet + ".png");

            if (System.IO.File.Exists(search)&&loai=="mt")
            {
                string back_grid = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mt_back_grid.png");
                string back = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mt_back.png");
                string nen = grid ? back_grid : back;
                byte[] img = xla.BitmapToBytes(xla.Merge(new List<string>() { nen, search }));
                return File(img, "image/png");
            }

            string search2 = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mc_" + huyet + ".png");
            if (System.IO.File.Exists(search2) && loai == "mc")
            {
                string back_grid = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mc_back_grid.png");
                string back = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/mc_back.png");
                string nen = grid ? back_grid : back;
                byte[] img = xla.BitmapToBytes(xla.Merge(new List<string>() { nen, search2 }));
                return File(img, "image/png");
            }

            string no_result_path = System.Web.HttpContext.Current.Server.MapPath("~/Content/img/no-result.png");
            byte[] no_result = System.IO.File.ReadAllBytes(no_result_path);

            return File(no_result, "image/png");
        }
    }
}