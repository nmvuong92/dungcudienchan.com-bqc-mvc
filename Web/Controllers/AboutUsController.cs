using System.Web.Mvc;

namespace Web.Controllers
{
    public class AboutUsController : BaseController
    {
        // GET: AboutUs
        public ActionResult Index()
        {
            return View();
        }
    }
}