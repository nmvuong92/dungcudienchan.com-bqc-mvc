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
using Web.Areas.Admin.ViewModels.MF;
using Web.Controllers;
using Web.Security;

namespace Web.Areas.Admin.Controllers
{
      [myAuth(Roles = "1")]
    public class ProductController : BaseController
    {
        private IProductRepository _ProductServ;
        vuong_cms_context __db = new vuong_cms_context();
        private IImgTmpRepository _imgTmpServ;
        public ProductController(
            IProductRepository _ProductServ, IImgTmpRepository _imgTmpServ)
        {
            this._ProductServ = _ProductServ;
            this._imgTmpServ = _imgTmpServ;

        }

        public ActionResult Index()
        {

            Product vm = new Product();
            return View(vm);
        }

        [HttpPost]
        public PartialViewResult ajax_paging(product_sp paging)
        {
            PG<Product> vmpg;
            vmpg = _ProductServ.GetQueryPaging(paging);
            return PartialView(vmpg);
        }


        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ProductCRUD vm = new ProductCRUD();
            vm.ImgTmpId = this._imgTmpServ.Create();
            vm.ConHang = true;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public JsonResult Create(ProductCRUD model)
        {
            rs r;
            try
            {
                var findThumbs = __db.ImgTmp.Find(model.ImgTmpId);
                Product entity = new Product();
                entity.MaSP = model.MaSP;
                entity.ProductName = model.ProductName;
                entity.ThumbnailImage = model.ThumbnailImage;
                entity.Price = model.Price;
               

                entity.Infomation = model.Infomation;
                entity.ProductCatId = model.ProductCatId;
              
                entity.MainProduct = model.MainProduct;
                entity.ImgTmpId = model.ImgTmpId;
                entity.ConHang = model.ConHang;
              
                entity.IsBanChay = model.IsBanChay;
                //first thumbs
                if (findThumbs.ImgTmpDetails.Any())
                {
                    var firstOrDefault = findThumbs.ImgTmpDetails.FirstOrDefault(f => f.IsMain);
                    if (firstOrDefault == null)
                    {
                        firstOrDefault = findThumbs.ImgTmpDetails.FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            entity.ThumbnailImage = firstOrDefault.Thumbnail;
                        }
                        else
                        {
                            entity.ThumbnailImage = "http://file.qdnd.vn/image/images/noimages.png";
                        }
                    }
                    else
                    {
                        entity.ThumbnailImage = firstOrDefault.Thumbnail;
                    }
                }

                __db.Product.Add(entity);
                __db.SaveChanges();
                r = rs.T("Okay");
            }
            catch (Exception ex)
            {
                r = rs.F("Lỗi: " + ex.Message);
            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int id)
        {
            Product entity = _ProductServ.GetEntry(id);

            ProductCRUD vm = new ProductCRUD();
            vm.Id = entity.Id;
            vm.MaSP = entity.MaSP;
            vm.ProductName = entity.ProductName;
            vm.ThumbnailImage = entity.ThumbnailImage;
            vm.Price = entity.Price??0;
            vm.Infomation = entity.Infomation;
            vm.ProductCatId = entity.ProductCatId;
           
            vm.MainProduct = entity.MainProduct;
            vm.ConHang = entity.ConHang;
           
            vm.IsBanChay = entity.IsBanChay;
            if (entity.ImgTmpId == null)
            {
                vm.ImgTmpId = _imgTmpServ.Create();
            }
            else
            {
                vm.ImgTmpId = entity.ImgTmpId;
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       

        public JsonResult Edit(ProductCRUD model)
        {
            rs r;
            using (var trans = new TransactionScope())
            {
                try
                {
                    var findThumbs = __db.ImgTmp.Find(model.ImgTmpId);
                    var entity = _ProductServ.SingleOrDefault(s => s.Id == model.Id);
                    entity.MaSP = model.MaSP;
                    entity.ProductCatId = model.ProductCatId;
               
                    entity.ProductName = model.ProductName;
                    entity.Price = model.Price;
                    entity.ImgTmpId = model.ImgTmpId;
                    entity.Infomation = model.Infomation;
                    entity.MainProduct = model.MainProduct;
                    entity.ConHang = model.ConHang;
                  
                    entity.IsBanChay = model.IsBanChay;
                    //first thumbs
                    if (findThumbs.ImgTmpDetails.Any())
                    {
                        var firstOrDefault = findThumbs.ImgTmpDetails.FirstOrDefault(f => f.IsMain);
                        if (firstOrDefault == null)
                        {
                            firstOrDefault = findThumbs.ImgTmpDetails.FirstOrDefault();
                            if (firstOrDefault != null)
                            {
                                entity.ThumbnailImage = firstOrDefault.Thumbnail;
                            }
                            else
                            {
                                entity.ThumbnailImage = "http://file.qdnd.vn/image/images/noimages.png";
                            }
                        }
                        else
                        {
                            entity.ThumbnailImage = firstOrDefault.Thumbnail;
                        }
                    }
                    _ProductServ.Save();
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
                Product Product = _ProductServ.GetEntry(id);

                _ProductServ.Delete(Product);
                _ProductServ.Save();

                r = rs.T("Ok");
            }
            catch (Exception ex)
            {
                r = rs.F("Lỗi: " + ex.Message);

            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }
        
        [HttpPost]
        public ActionResult SetBanChayProduct(int id)
        {
            rs r;
            try
            {

                Product search = _ProductServ.GetEntry(id);
                search.IsBanChay = !search.IsBanChay;
                _ProductServ.Save();
                r = rs.T("Ok");
            }
            catch (Exception ex)
            {
                r = rs.F("Lỗi: " + ex.Message);

            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult SetMainProduct(int id)
        {
            rs r;
            try
            {

                Product search = _ProductServ.GetEntry(id);
                search.MainProduct = !search.MainProduct;
                _ProductServ.Save();
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
                            __db.Database.ExecuteSqlCommand("DELETE FROM Products WHERE Id=" + idmess);
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


        #region IMAGES

        #endregion
    }
}
