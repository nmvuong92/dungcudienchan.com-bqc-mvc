using System.Web.Mvc;

using Web.Controllers;
using Web.Security;

namespace Web.Areas.Admin.Controllers
{
    [myAuth(Roles = "1")]
     
    public class aWebPartsController :BaseController
    {
        public PartialViewResult Head()
        {
            return PartialView();
        }
        public PartialViewResult Header()
        {
            return PartialView();
        }

        public PartialViewResult Footer()
        {
            return PartialView();
        }

        
        public PartialViewResult LeftNav()
        {
            
            return PartialView();
        }
    }
}