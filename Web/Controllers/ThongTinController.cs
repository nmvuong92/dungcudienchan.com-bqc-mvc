using System.Linq;
using System.Web.Mvc;
using VD.Data;
using Web.Security;

namespace Web.Controllers
{
 
    public class ThongTinController : BaseController
    {
        // GET: ThongTin
        public ActionResult Index()
        {
            vuong_cms_context  __db = new vuong_cms_context();
            var model =__db.ThongTin.FirstOrDefault();


            return View(model);
        }
    }
}