using System.Collections.Generic;
using System.Web.Mvc;
using VD.Data.Entity;
using VD.Data.IRepository;
using Web.ViewModels.WebParts;
using System.Linq;
namespace Web.Controllers
{

    public class WebPartsController : BaseController
    {
        private ICounterRepository _counterRepo;
        private ILangRepository _langRepo;
        private IConfigRepository _confServ;
        public WebPartsController(
            ICounterRepository _counterRepo,
            ILangRepository _langRepo,
            IConfigRepository _confRepo
            )
        {
            this._counterRepo = _counterRepo;
            this._langRepo = _langRepo;
            this._confServ = _confRepo;
        }
        public PartialViewResult SelectLanguage()
        {
            ViewBag.__langid = __langid;
            IEnumerable<Lang> viewmodel = _langRepo.GetList(orderBy: q => q.OrderBy(o => o.Order));
            return PartialView(viewmodel);
        }
        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            FooterView model = new FooterView()
            {
                Statistic = this._counterRepo.GetCounter()
            };
            model.copyright = _confServ.GetContent("conf_copyright");
            model.design_by = _confServ.GetContent("conf_designby");
            model.tel = _confServ.GetContent("conf_tel");
            model.mail = _confServ.GetContent("conf_email");
            model.address = _confServ.GetContent("conf_address");
            model.link_facebook = _confServ.GetContent("link_facebook");
            model.link_linkedin = _confServ.GetContent("link_linkedin");
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult Header()
        {
            HeaderView model = new HeaderView();
            model._current_culture = base.__langid;
            model.Langs = _langRepo.GetList(orderBy: q => q.OrderBy(o => o.Order));
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult JobMenu()
        {
            return PartialView();
        }

        [OutputCache(Duration = 3600)]
        public PartialViewResult Favicon()
        {
            return PartialView();
        }
        public PartialViewResult AppMenu()
        {
            ViewBag.HotLine = _confServ.GetContent("html_hotline");
            return PartialView();
        }

    }
}