
using System.Linq;
using System.Web.Mvc;
using VD.Data;
using VD.Data.IRepository;
using Web.Controllers;
using Web.Security;

namespace Web.Areas.Admin.Controllers
{
    [myAuth(Roles = "1")]
    public class HomeController : BaseController
    {
        private IUserRepository _userServ;
        private ICounterRepository _counterRepo;
      
        private vuong_cms_context __db = new vuong_cms_context();

        public HomeController(IUserRepository userServ, ICounterRepository _counterRepo)
        {
            this._userServ = userServ;
            this._counterRepo = _counterRepo;
      
        }
       
        // GET: Admin/Home
        public ActionResult Index()
        {
         
            return View();
           
        }
        [HttpGet]
        public PartialViewResult ThongKeLuongTruyCap()
        {
            var viewmodel = _counterRepo.GetCounter();
            return PartialView(viewmodel);
        }
     

    }
}