using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
   
    public class BaiThuHoachController : BaseController
    {
        // GET: BaiThuHoach
        public ActionResult Index()
        {
            return View();
        }
    }
}