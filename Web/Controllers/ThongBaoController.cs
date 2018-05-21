using System;
using System.Linq;
using System.Web.Mvc;
using VD.Data;
using VD.Lib;

namespace Web.Controllers
{
    public class ThongBaoController : Controller
    {
        vuong_cms_context __db =new vuong_cms_context();
        // GET: ThongBao
        [HttpGet]
        public JsonResult LayThongBao()
        {
            var tim = __db.ThongBao.FirstOrDefault(f => f.IsHienThi);
            var cook = myCookies.Get("alert");
            if (tim != null && cook == string.Empty)
            {
                myCookies.Set("alert", "1", DateTime.Now.AddHours(24));
            }
            else
            {
                tim = null;
            }
            return Json(tim, JsonRequestBehavior.AllowGet);
        }
    }
}