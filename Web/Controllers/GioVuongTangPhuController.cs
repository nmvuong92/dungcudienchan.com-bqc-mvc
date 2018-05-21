using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
   
    public class GioVuongTangPhuController : BaseController
    {
        // GET: GioVuongTangPhu
        public ActionResult Index()
        {
            return View();
        }
    }
}