using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
   
    public class VideoController : BaseController
    {
        // GET: Video
        public ActionResult Index()
        {
            return View();
        }
    }
}