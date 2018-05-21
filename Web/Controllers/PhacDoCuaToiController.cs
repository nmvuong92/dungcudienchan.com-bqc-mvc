using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using VD.Data;
using VD.Data.Entity.BQC;
using VD.Lib.DTO;
using Web.Security;
using Web.ViewModels.BQC;

namespace Web.Controllers
{
    [myAuth(Roles = "2",vip1 = true)]
    public class PhacDoCuaToiController : BaseController
    {

        vuong_cms_context __db = new vuong_cms_context();
        [myAuth(Roles = "2")]
        public ActionResult Index(int page=1,string q="")
        {
            var au = MySsAuthUsers.GetAuth();
            PDCTview model = new PDCTview();
            IQueryable<PhacDoCuaToi> qqq = __db.PhacDoCuaToi.Where(w => w.UserId == au.ID);
            if (!string.IsNullOrEmpty(q))
            {
                model.Q = q;
                qqq = qqq.Where(w => w.TenPhacDo.Contains(q));
            }
            else
            {
                model.Q = "";
            }
            model.pList = qqq.OrderByDescending(b => b.ModifiedDate).ToPagedList(page, 10);
            return View(model);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult TaoMoi(PhacDoCuaToi model)
        {
            ValidationHuyet __val = new ValidationHuyet();
            rs valHuyet = __val.totalValidationHuyet(model.Huyet);
            if (valHuyet.r)
            {
                try
                {
                    var au = MySsAuthUsers.GetAuth();
                    model.UserId = au.ID;
                    __db.PhacDoCuaToi.Add(model);
                    __db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, valHuyet.m);
                return View(model);
            }
        }

       [System.Web.Mvc.HttpPost]
        public JsonResult Xoa(int id)
        {
            var first = __db.PhacDoCuaToi.Find(id);
            if (first != null)
            {
                __db.PhacDoCuaToi.Remove(first);
                __db.SaveChanges();
            }
           return Json(rs.T("Ok"), JsonRequestBehavior.DenyGet);

        }

        public ActionResult CapNhat(int id)
        {
            var en = __db.PhacDoCuaToi.Find(id);
            return View(en);

        }
        [System.Web.Mvc.HttpPost]
        public ActionResult CapNhat(PhacDoCuaToi model)
        {
            ValidationHuyet __val = new ValidationHuyet();
            rs valHuyet = __val.totalValidationHuyet(model.Huyet);
            if (valHuyet.r)
            {
                try
                {
                    var en = __db.PhacDoCuaToi.Find(model.Id);
                    en.TenPhacDo = model.TenPhacDo;
                    en.Huyet = model.Huyet;
                    en.MoTa = model.MoTa;
                    en.ModifiedDate = DateTime.Now;
                    __db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, valHuyet.m);
                return View(model);
            }
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult Preview(string dshuyet)
        {
            List<string> huyet_split = dshuyet.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            List<string> map = new List<string>();
            string grid = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/hai_mat_grid.png");
            string back = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/hai_mat_back.png");
            map.Add(grid);
            map.Add(back);
            ValidationHuyet __val = new ValidationHuyet();
            var vall = __val.totalValidationHuyet(dshuyet);
            foreach(var huyet in huyet_split)
            {
                var lay_ten = __val.fnLayTenAnhPTCT(huyet);
                string search = System.Web.HttpContext.Current.Server.MapPath("~/Content/bqc/" + lay_ten + ".png");
                if (System.IO.File.Exists(search))
                {
                    map.Add(search);
                }
            }
            XuLyAnh xla = new XuLyAnh();
            byte[] img = xla.BitmapToBytes(xla.Merge(map));
            string base64img = Convert.ToBase64String(img);
            return Json(
             new {
                base64img,
                vall
            },JsonRequestBehavior.AllowGet);//File(img, "image/png");
        }

    }
}