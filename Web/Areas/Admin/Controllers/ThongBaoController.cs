using AutoMapper;
using System;

using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Mvc;
using Ninject;
using VD.Data;
using VD.Data.Entity.BQC;
using VD.Data.IRepository;
using VD.Data.Paging;


using VD.Lib.DTO;
using VD.Lib.Encode;

using Web.Controllers;
using Web.Security;


namespace Web.Areas.Admin.Controllers
{
    [myAuth(Roles = "1")]
    public class ThongBaoController : BaseController
    {
        private IRoleRepository _roleServ;
        private IPermissionRepository _permisionServ;
        private IThongBaoRepository _ThongBaoerv;
        vuong_cms_context __db = new vuong_cms_context();
        public ThongBaoController(
            IRoleRepository _roleServ,
            IPermissionRepository _quyenServ,
            IThongBaoRepository _ThongBaoerv)
        {
            this._roleServ = _roleServ;
            this._permisionServ = _quyenServ;
            this._ThongBaoerv = _ThongBaoerv;

        }

        public ActionResult Index()
        {
            ThongBao vm = new ThongBao();
            return View(vm);
        }

        [HttpPost]
        public PartialViewResult ajax_paging(smartpaging paging)
        {
            PG<ThongBao> vmpg;
            vmpg = _ThongBaoerv.GetQueryPaging(paging);
            return PartialView(vmpg);
        }


        // GET: Admin/ThongBao/Create
        public ActionResult Create()
        {

            ViewBag.ddlRole = _roleServ.GetList().ToList().Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
            ViewBag.dsQuyen = _permisionServ.GetList().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult Create(ThongBao model)
        {
            rs r;
            try
            {
                ThongBao entity = new ThongBao();
                //pass encode
                entity.TieuDe = model.TieuDe;
                entity.NoiDung = model.NoiDung;
                entity.IsHienThi = model.IsHienThi;
                __db.ThongBao.Add(entity);
                __db.SaveChanges();
                r = rs.T("Okay");
            }
            catch (Exception ex)
            {
                r = rs.F("Lỗi: " + ex.Message);
            }

            return Json(r, JsonRequestBehavior.DenyGet);
        }

        // GET: Admin/ThongBao/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongBao ThongBao = _ThongBaoerv.GetEntry(id.Value);
            if (ThongBao == null)
            {
                return HttpNotFound();
            }

            return View(ThongBao);
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]

        public JsonResult Edit(ThongBao model)
        {
            rs r;
            using (var trans = new TransactionScope())
            {
                try
                {
                    SimpleAES __aes = new SimpleAES();
                    var entity = _ThongBaoerv.SingleOrDefault(s => s.Id == model.Id);
                    entity.TieuDe = model.TieuDe;
                    entity.NoiDung = model.NoiDung;
                    entity.IsHienThi = model.IsHienThi;
                    _ThongBaoerv.Save();
                    trans.Complete();
                    r = rs.T("Okay");
                }
                catch (Exception ex)
                {
                    r = rs.F("Lỗi: " + ex.Message);
                }
            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            rs r;
            try
            {
                ThongBao ThongBao = _ThongBaoerv.GetEntry(id);
                _ThongBaoerv.Delete(ThongBao);
                _ThongBaoerv.Save();
                r = rs.T("Ok");
            }
            catch (Exception ex)
            {
                r = rs.F("Lỗi: " + ex.Message);

            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult xoachon(int[] arr)
        {
            rs r;
            if (arr.Length == 0)
            {
                r = rs.F("Không có mục nào chọn");
            }
            else
            {
                using (var trans = new TransactionScope())
                {
                    try
                    {

                        foreach (int idmess in arr)
                        {
                            var en = _ThongBaoerv.SingleOrDefault(s => s.Id == idmess);
                            _ThongBaoerv.Delete(en);
                            _ThongBaoerv.Save();
                        }
                        r = rs.T("OK");
                        trans.Complete();
                    }
                    catch (Exception ex)
                    {
                        r = rs.F(ex.Message);
                    }
                }

            }

            return Json(r, JsonRequestBehavior.DenyGet);
        }

    }
}
