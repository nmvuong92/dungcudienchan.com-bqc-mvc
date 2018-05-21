using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Data;
using VD.Data.Entity.MF;
using VD.Lib.DTO;
using Web.Areas.Admin.ViewModels.MF;
using Web.Controllers;
using Web.Security;

namespace Web.Areas.Admin.Controllers
{
        [myAuth(Roles = "1")]
    public class ProductCatController :BaseController
    {
        vuong_cms_context __db = new vuong_cms_context();
        // GET: Admin/ProductCat
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult List()
        {
            var qqq = __db.ProductCat.Where(w => w.ParentId == null).AsQueryable();
            return PartialView(qqq);
        }

        public PartialViewResult Child(int parentId)
        {
            var qqq = __db.ProductCat.Where(w=>w.ParentId==parentId).AsQueryable();
            return PartialView(qqq);
        }

        public PartialViewResult ddlParent(string name, string SelectedValue = "-1", string Default = "Parent")
        {
            var qqq = __db.ProductCat.Where(w => w.ParentId == null).AsQueryable();
            ViewBag.Name = name;
            ViewBag.SelectedValue = SelectedValue;
            ViewBag.Default = Default;
            return PartialView(qqq);
        }

        public PartialViewResult ddlChild(int parentId, int sign = 1, string SelectedValue = "-1")
        {
  
            var qqq = __db.ProductCat.Where(w => w.ParentId == parentId).AsQueryable();
            ViewBag.sign = sign;
            ViewBag.SelectedValue = SelectedValue;
            return PartialView(qqq);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var entity = __db.ProductCat.Find(id);
            ProductCatCRUD vm = new ProductCatCRUD();
            vm.Id = entity.Id;
            vm.Name = entity.Name;
            vm.ParentId = entity.ParentId;
            vm.ShowInProduct = entity.ShowInProduct;
            vm.Description = entity.Description;
            
            return View(vm);
        }

        [HttpPost]
        public JsonResult EditProccess(ProductCatCRUD model)
        {
            rs r;
            try
            {
                 var entity = __db.ProductCat.Find(model.Id);
                entity.Name = model.Name;
                entity.ShowInProduct = model.ShowInProduct;
                entity.ParentId = (model.ParentId==-1)?null:model.ParentId;
                entity.Description = model.Description;
                __db.SaveChanges();
                r = rs.T("Ok");
            }
            catch (Exception ex)
            {
                r = rs.F(ex.Message);
            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ProductCatCRUD vm = new ProductCatCRUD();
            vm.ParentId = -1;
            return View(vm);
        }

        [HttpPost]
        public JsonResult CreateProccess(ProductCatCRUD model)
        {
            rs r;
            try
            {
                var entity = new ProductCat();
                entity.Name = model.Name;
                entity.ShowInProduct = model.ShowInProduct;
                entity.ParentId = (model.ParentId == -1) ? null : model.ParentId;
                entity.Description = model.Description;
                __db.ProductCat.Add(entity);
                __db.SaveChanges();
                r = rs.T("Ok");
            }
            catch (Exception ex)
            {
                r = rs.F(ex.Message);
            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }

        public JsonResult Delete(int id)
        {
            rs r;
            try
            {
                var entity = __db.ProductCat.Find(id);
                __db.ProductCat.Remove(entity);
                __db.SaveChanges();
                r = rs.T("Ok");
            }
            catch (Exception ex)
            {
                r = rs.F(ex.Message);
            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }
    }
}