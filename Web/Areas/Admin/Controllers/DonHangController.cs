using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using VD.Data;
using VD.Data.Entity.MF;
using VD.Data.IRepository;
using VD.Data.Paging;
using VD.Lib.DTO;
using VD.Lib.Encode;
using Web.Areas.Admin.ViewModels;
using Web.Areas.Admin.ViewModels.MF;
using Web.Controllers;
using Web.Security;

namespace Web.Areas.Admin.Controllers
{
      [myAuth(Roles = "1")]
    public class DonHangController : BaseController
    {
        private IDonHangRepository _DonHangServ;
        vuong_cms_context __db = new vuong_cms_context();
        private IImgTmpRepository _imgTmpServ;
        public DonHangController(
            IDonHangRepository _DonHangServ, IImgTmpRepository _imgTmpServ)
        {
            this._DonHangServ = _DonHangServ;
            this._imgTmpServ = _imgTmpServ;

        }

        public ActionResult Index()
        {

            DonHang vm = new DonHang();
            return View(vm);
        }

        [HttpPost]
        public PartialViewResult ajax_paging(donhang_sp paging)
        {
            PG<DonHang> vmpg;
            vmpg = _DonHangServ.GetQueryPaging(paging);
            return PartialView(vmpg);
        }


     

        // GET: Admin/DonHangs/Edit/5
        public ActionResult Edit(int id)
        {
            DonHang entity = _DonHangServ.GetEntry(id);

            DonHangCRUD vm = new DonHangCRUD();
            vm.entity = entity;
            vm.Id = entity.Id;
            vm.TrangThaiGiaoHangId = entity.TrangThaiGiaoHangId;
            vm.TrangThaiThanhToanId = entity.TrangThaiThanhToanId;
            vm.HTTTID = entity.HTTTID;
            vm.GhiChuDonHang = entity.GhiChuDonHang;
            vm.ddlTrangThaiGiaoHang = __db.TrangThaiGiaoHang.Select(s => new SelectListItem()
            {
                Text=s.Ten,
                Value = s.Id.ToString(),
            }).ToList();

            vm.ddlTrangThaiThanhToan = __db.TrangThaiThanhToan.Select(s => new SelectListItem()
            {
                Text = s.Ten,
                Value = s.Id.ToString(),
            }).ToList();
            vm.ddlHttt = __db.HTTTs.Select(s => new SelectListItem()
            {
                Text = s.Ten,
                Value = s.Id.ToString(),
            }).ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public JsonResult Edit(DonHangCRUD model)
        {
            rs r;
            using (var trans = new TransactionScope())
            {
                try
                {
                    var entity = _DonHangServ.SingleOrDefault(s => s.Id == model.Id);
                    entity.TrangThaiGiaoHangId = model.TrangThaiGiaoHangId;
                    entity.TrangThaiThanhToanId = model.TrangThaiThanhToanId;
                    entity.HTTTID = model.HTTTID;
                    entity.ModifiedDate = DateTime.Now;
                    entity.GhiChuDonHang = model.GhiChuDonHang;
                    _DonHangServ.Save();
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
                DonHang DonHang = _DonHangServ.GetEntry(id);

                _DonHangServ.Delete(DonHang);
                _DonHangServ.Save();

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
                            __db.Database.ExecuteSqlCommand("DELETE FROM DonHangs WHERE Id=" + idmess);
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
