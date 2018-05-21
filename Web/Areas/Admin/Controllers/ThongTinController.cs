using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Data;
using VD.Data.Entity.BQC;
using Web.Controllers;
using Web.Security;

namespace Web.Areas.Admin.Controllers
{
    [myAuth(Roles = "1")]
    public class ThongTinController : BaseController
    {
        // GET: Admin/ThongTin
        vuong_cms_context __db = new vuong_cms_context();
        public ActionResult Index()
        {
            var model = __db.ThongTin.FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ThongTin vm)
        {
            var model = __db.ThongTin.Find(vm.Id);
            if (model != null)
            {
                model.TieuDe = vm.TieuDe;
                model.NoiDung = vm.NoiDung;
                __db.SaveChanges();
                    
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Không tìm thấy dữ liệu!");
            }
            return View("Index",model);


        }
    }
}