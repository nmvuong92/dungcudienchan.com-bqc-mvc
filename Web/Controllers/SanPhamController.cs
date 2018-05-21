using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using VD.Data;
using VD.Data.Entity.MF;
using VD.Data.IRepository;
using VD.Lib;
using VD.Lib.DTO;
using Web.ViewModels;
using Web.ViewModels.JSON;
using Web.ViewModels.MF;

namespace Web.Controllers
{
    public class SanPhamController : BaseController
    {
        private IContactRepository _contactRepo;
        private IConfigRepository _confServ;

        public SanPhamController(IContactRepository _contactRepo, IConfigRepository _confServ)
        {
            this._contactRepo = _contactRepo;
            this._confServ = _confServ;
        }

        public ActionResult Dssp(int page = 1)
        {
            vuong_cms_context __db = new vuong_cms_context();
            ProductIndexByCatView view = new ProductIndexByCatView();

            // view.ProductCat = __db.ProductCat.Find(catid);
            // view.ProductCatRelated =
            //__db.ProductCat.Where(w => w.ParentId == view.ProductCat.ParentId).OrderBy(o => o.Id).Take(8).ToList();

            view.Products = __db.Product.OrderBy(o => o.Id).ToPagedList(page, __setting.row_per_page);
            
            return View(view);
        }

        public JsonResult LayDSSP(int DMID)
        {
            vuong_cms_context __db = new vuong_cms_context();
            ProductIndexByCatView view = new ProductIndexByCatView();

            // view.ProductCat = __db.ProductCat.Find(catid);
            // view.ProductCatRelated =
            //__db.ProductCat.Where(w => w.ParentId == view.ProductCat.ParentId).OrderBy(o => o.Id).Take(8).ToList();
            IQueryable<Product> qqq;
            if (DMID == 0)
            {
                qqq = __db.Product.Where(w => w.IsBanChay).OrderBy(q => q.IsBanChay).ThenByDescending(q => q.Id).AsQueryable();
            }
            else
            {
                qqq = __db.Product.Where(w => w.ProductCatId == DMID).OrderBy(q=>q.IsBanChay).ThenByDescending(q=>q.Id).AsQueryable();
            }

            var lst = qqq.Select(s => new ProductJson
            {
                Id = s.Id,
                MaSP = s.MaSP,
                ProductName = s.ProductName,
                Infomation = s.Infomation,
                ThumbnailImage = s.ThumbnailImage,
                Price = s.Price ?? 0,
                ProductCatId = s.ProductCatId,
                DanhMuc = s.ProductCat.Name,
                HinhAnhs = s.ImgTmp.ImgTmpDetails.Select(s2 => new Img()
                {
                    Thumb = s2.Thumbnail,
                    Full = s2.FullImage
                }).ToList(),
                MainProduct = s.MainProduct,
                ConHang = s.ConHang,
             
                IsBanChay = s.IsBanChay,
                SLGioHang = 0
            });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LayDM()
        {
            vuong_cms_context __db = new vuong_cms_context();
            // view.ProductCat = __db.ProductCat.Find(catid);
            // view.ProductCatRelated =
            //__db.ProductCat.Where(w => w.ParentId == view.ProductCat.ParentId).OrderBy(o => o.Id).Take(8).ToList();
            List<DMJson> lst = __db.ProductCat.Where(w => w.ParentId == null).OrderBy(q => q.Order).Select(s => new DMJson
            {
                Id = s.Id,
                Name = s.Name,
                Active = true,
            }).ToList();
          
            lst.Insert(0,new DMJson()
            {
                Name = "Sản phẩm nổi bật",
                Id = 0,
                Active = true,
               
            });

            foreach (var dm in lst)
            {
                int DMID = dm.Id;
                IQueryable<Product> qqq;
                if (DMID == 0)
                {
                    qqq = __db.Product.Where(w => w.IsBanChay).OrderBy(q => q.IsBanChay).ThenByDescending(q => q.Id).AsQueryable();
                }
                else
                {
                    qqq = __db.Product.Where(w => w.ProductCatId == DMID).OrderBy(q => q.IsBanChay).ThenByDescending(q => q.Id).AsQueryable();
                }
                var lst_sps = qqq.Select(s => new ProductJson
                {
                    Id = s.Id,
                    MaSP = s.MaSP,
                    ProductName = s.ProductName,
                    Infomation = s.Infomation,
                    ThumbnailImage = s.ThumbnailImage,
                    Price = s.Price ?? 0,
                    ProductCatId = s.ProductCatId,
                    DanhMuc = s.ProductCat.Name,
                    HinhAnhs = s.ImgTmp.ImgTmpDetails.Select(s2 => new Img()
                    {
                        Thumb = s2.Thumbnail,
                        Full = s2.FullImage
                    }).ToList(),
                    MainProduct = s.MainProduct,
                    ConHang = s.ConHang,

                    IsBanChay = s.IsBanChay,
                    SLGioHang = 0
                }).ToList();

                dm.SPS = lst_sps;
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        // GET: DonHang
        public ActionResult Index(string back_url = "")
        {
            ViewBag.back_url = back_url;
            ViewBag.PaymentCK = _confServ.GetContent("html_taikhoannganhang");
            ThanhToanVM vm = new ThanhToanVM();
            using (var __db = new vuong_cms_context())
            {

                vm.ddlProvince = __db.Province.OrderBy(o=>o.SortOrder).Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();
                vm.ddlHttt = __db.HTTTs.Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Ten
                }).ToList();
                vm.ddlXungDanh = new List<SelectListItem>(){
                    new SelectListItem(){Text="Anh",Value="Anh"},
                    new SelectListItem(){Text= "Chị",Value="Chị"}
                }.ToList();

                //user
                var uuu = MySsAuthUsers.GetAuth();


                if (uuu != null)
                {
                    var user = __db.Users.Find(uuu.ID);
                    vm.DiaChi = user.Address;
                    vm.Ho = user.Ho;
                    vm.Ten = user.Ten;
                    vm.ProvinceId = user.ProvinceId ?? -1;
                    vm.DistrictId = user.DistrictId ?? -1;
                    vm.WardId = user.WardId ?? -1;

                    vm.XungDanh = user.XungDanh;
                    vm.ddlDistrict =
                        __db.District.Where(w => w.ProvinceId == user.ProvinceId).Select(s => new SelectListItem()
                        {
                            Text = s.Name,
                            Value = s.Id.ToString()
                        }).ToList();
                    vm.ddlWard = __db.Ward.Where(w => w.DistrictID == user.DistrictId).Select(s => new SelectListItem()
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    }).ToList();
                }
                else
                {
                    vm.ddlDistrict = new List<SelectListItem>();
                    vm.ddlWard = new List<SelectListItem>();
                }
            }




            return View(vm);
        }

        public ActionResult ThongTinDonHang(int id)
        {
            var log_user = MySsAuthUsers.GetAuth();
            if (log_user == null)
            {
                return Content("Page not found");
            }
           
            vuong_cms_context __db = new vuong_cms_context();
            var model = __db.DonHangs.Find(id);
            if (log_user.ID != model.UserId)
            {
                return Content("Not permission!");
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult DatHangProccess(ThanhToanVM model)
        {
            rs r;
            var giohang = GioHang.Lay();
            if (!giohang.CTGioHangs.Any())
            {
                ModelState.AddModelError(string.Empty, "Giỏ hàng rỗng");
            }
            var uuu = MySsAuthUsers.GetAuth();
            if (uuu == null)
            {
                ModelState.AddModelError(string.Empty, "Vui lòng đăng nhập");
            }
            bool status = false;
            using (var __db = new vuong_cms_context())
            {
                if (ModelState.IsValid)
                {

                    using (var tx = __db.Database.BeginTransaction())
                    {
                        try
                        {
                            var gia_ship = __db.District.Find(model.DistrictId).GiaShip;
                            //donhang
                            DonHang donhang = new DonHang();
                            donhang.Ho = model.Ho;
                            donhang.Ten = model.Ten;
                            donhang.SDT = model.SDT;
                            donhang.Email = model.Email;
                            donhang.ProvinceId = model.ProvinceId;
                            donhang.DistrictId = model.DistrictId;
                            donhang.WardId = model.WardId;
                            donhang.XungDanh = model.XungDanh;
                            donhang.DiaChi = model.DiaChi;
                            donhang.HTTTID = model.HTTTID;
                            donhang.GiaShip = gia_ship;
                            donhang.PTVat = giohang.PTVat;
                            donhang.TienVat = giohang.TienVat;
                            donhang.TongTienHang = giohang.TongTienHang;
                            donhang.ThanhTien = giohang.ThanhTien + gia_ship;
                            donhang.SoLuongSanPham = giohang.SoLuongSanPham;
                            donhang.TrangThaiGiaoHangId = 1;
                            donhang.TrangThaiThanhToanId = 1;

                            //ctdh
                            donhang.CTDonHangs = new Collection<CTDonHang>();
                            foreach (var gio in giohang.CTGioHangs)
                            {
                                donhang.CTDonHangs.Add(new CTDonHang()
                                {
                                    SanPhamId = gio.SanPhamId,
                                    SoLuong = gio.SoLuong,
                                    ThanhTien = gio.ThanhTien,
                                    TenSanPham = gio.TenSanPham,
                                    DonGia = gio.DonGia,
                                });
                            }
                            //user

                            if (uuu != null)
                            {
                                donhang.UserId = uuu.ID;
                            }
                            else
                            {
                                donhang.UserId = null;
                            }

                        



                            //gửi email

                            string style =
                                "<style type='text/css'>.table_ctsp{border-collapse:collapse;width:100%}.table_ctsp tr td,.table_ctsp tr th{border:1px solid gray;padding:5px}</style>";
                            string ctdh = "";
                            ctdh += "<table class='table_ctsp'>";
                            ctdh += "<thead>";
                            ctdh += "<tr>";
                            ctdh += "<th>Mã sản phẩm</th>";
                            ctdh += "<th>Tên sản phẩm</th>";
                            ctdh += "<th>Hình ảnh</th>";
                            ctdh += "<th>Đơn giá</th>";
                            ctdh += "<th>Số lượng</th>";
                            ctdh += "<th>Thành tiền</th>";
                            ctdh += "</tr>";
                            ctdh += "</thead>";
                            ctdh += "<tbody>";
                            foreach (var item in donhang.CTDonHangs)
                            {
                                var sp = __db.Product.Find(item.SanPhamId);
                                ctdh += "<tr>";
                                ctdh += "<td>" + item.Id + "</td>";
                                ctdh += "<td>" + sp.ProductName + "</td>";
                                ctdh += "<td><img src='" + myUrl.ResolveServerUrl(sp.ThumbnailImage, false) +
                                        "' style='width:100px;'/></td>";
                                ctdh += "<td>" + item.DonGia.ToMoneyStr() + "</td>";
                                ctdh += "<td>" + item.SoLuong + "</td>";
                                ctdh += "<td>" + item.ThanhTien.ToMoneyStr() + "</td>";
                                ctdh += "</tr>";
                            }
                            ctdh += "</tbody>";
                            ctdh += "</table>";
                            var province = __db.Province.Find(donhang.ProvinceId);
                            var district = __db.District.Find(donhang.DistrictId);
                            var ward = __db.Ward.Find(donhang.WardId);
                            var httt = __db.HTTTs.Find(donhang.HTTTID);
                            var tinhtranggiaohang = __db.TrangThaiGiaoHang.Find(donhang.TrangThaiGiaoHangId);
                            var tinhtrangtt = __db.TrangThaiThanhToan.Find(donhang.TrangThaiThanhToanId);
                            var dic = new Dictionary<string, string>()
                            {
                                {"danh_xung", model.XungDanh},
                                {"ho", model.Ho},
                                {"ten", model.Ten},
                                {"dia_chi", model.DiaChi},
                                {"sdt", model.SDT},
                                {"tinh_thanh", province.Name + "(" + province.CountryCode + ")"},

                                {"quan_huyen", district.Name},
                                {"phuong_xa", ward.Name},
                                {"tong_tien_hang", donhang.TongTienHang.ToMoneyStr()},
                                {"pt_vat", donhang.PTVat.ToString()},
                                {"tien_vat", donhang.TienVat.ToMoneyStr()},
                                {"+", donhang.GiaShip.ToMoneyStr()},
                                {"tong_tien_thanh_toan", donhang.ThanhTien.ToMoneyStr()},
                                {"ngay_mua_hang", donhang.CreatedDate.XuatDateTime()},
                                {"chi_tiet_don_hang", ctdh},
                                {"hinh_thuc_thanh_toan", httt.Ten},
                                {"tinh_trang_thanh_toan", tinhtrangtt.Ten},
                                {"tinh_trang_giao_hang", tinhtranggiaohang.Ten},

                            };
                            var __seting_current = __db.Settings.Find(1);

                            string rs_str = "Đặt hàng thành công <br/> error: mail servers have problems, please try again later!";
                            if (!string.IsNullOrWhiteSpace(__seting_current.etmp_thong_bao_dat_hang_title)&&!string.IsNullOrWhiteSpace(__seting_current.etmp_thong_bao_dat_hang))
                            {
                                string _tieude2 = sendMailObj.ReplaceContent(__seting_current.etmp_thong_bao_dat_hang_title, dic);
                                string _noidung2 = style + sendMailObj.ReplaceContent(__seting_current.etmp_thong_bao_dat_hang, dic);


                                //send to cus and adm
                                var rs1 = _contactRepo.SendEmail(new sendMailObj()
                                {
                                    emailNhan = donhang.Email,
                                    tieude = _tieude2,
                                    noidung = _noidung2
                                }, true, true);


                                if (rs1.r)
                                {
                                    rs_str = "<h1>Đặt hàng thành công <br/></h1> <h2>" + _tieude2 + "</h2>" +_noidung2;
                                    donhang.IsDaGuiEmail = true;
                                }
                                else
                                {
                                    donhang.IsDaGuiEmail = false;
                                }
                            }
                            donhang.NoiDungGuiEmail = rs_str;
                            __db.DonHangs.Add(donhang);
                            __db.SaveChanges();
                            //com


                            tx.Commit();
                            r = rs.T(rs_str, donhang.Id);

                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                            r = rs.F(ex.Message);
                        }

                    }
                }
                else
                {
                    r = rs.F("Lỗi giao dịch");
                }

            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }


        // [Route("detail/{seo_title}/{id}.html")]
        public ActionResult Detail(int id)
        {
            vuong_cms_context __db = new vuong_cms_context();
            var model = __db.Product.FirstOrDefault(w => w.Id == id);
            var vm = new ChiTietSanPhamView();
            vm.Id = id;
            vm.SanPham = model;
            vm.DanhMucLienQuan =
              __db.ProductCat.Where(w => w.ParentId == model.ProductCat.ParentId)
                  .OrderBy(o => o.Id)
                  .Take(8).ToList();
            vm.SanPhamLienQuan =
                __db.Product.Where(w => w.ProductCatId == model.ProductCatId)
                    .OrderBy(o => o.Id)
                    .Take(8).ToList();
            return View(vm);
        }

        [HttpGet]
        public JsonResult LayGioHang()
        {
            var model = GioHang.Lay();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LayGiaShipProvince(int district_id)
        {
            int gia_ship = 0;
            using (var __db = new vuong_cms_context())
            {
                var item = __db.Province.Find(district_id);
                gia_ship = item.GiaShip;
            }
            return Json(gia_ship, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LayGiaShip(int district_id)
        {
            int gia_ship = 0;
            using (var __db = new vuong_cms_context())
            {
                var item = __db.District.Find(district_id);
                gia_ship = item.GiaShip;
            }
            return Json(gia_ship, JsonRequestBehavior.AllowGet);
        }
        public static Random rd;
        [HttpPost]
        public JsonResult ThemSanPham(int SanPhamId, int SoLuong)
        {
            rs r;
            rd = new Random();

            try
            {
                vuong_cms_context __db = new vuong_cms_context();
                var sanpham = __db.Product.Find(SanPhamId);

                CTGioHang ct = new CTGioHang();
                ct.SanPhamId = SanPhamId;
                ct.TenSanPham = sanpham.ProductName;
                ct.DonGia = sanpham.Price ?? 0;
                ct.HinhAnh = sanpham.ThumbnailImage;
                ct.SoLuong = SoLuong;
                ct.Link = Url.Action("Detail", "SanPham", new { id = sanpham.Id, seo_title = sanpham.ProductName.URLFriendly() });
                GioHang.ThemSanPham(ct);
                r = rs.T("Ok");
            }
            catch (Exception ex)
            {
                r = rs.F("Lỗi: " + ex.Message);
            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult Xoa(int id)
        {
            rs r;
            try
            {

                GioHang.XoaSP(id);
                r = rs.T("Ok");
            }
            catch (Exception ex)
            {
                r = rs.F(ex.Message);
            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public JsonResult LaySanPhamGioHang(int id)
        {
            CTGioHang ct = GioHang.LaySanPham(id);
            return Json(ct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CapNhatSoLuong(int id, int sl)
        {
            rs r;
            try
            {

                GioHang.CapNhatSL(id, sl);
                r = rs.T("Ok");
            }
            catch (Exception ex)
            {
                r = rs.F(ex.Message);
            }
            return Json(r, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public JsonResult ddlDistrict(int province_id)
        {
            vuong_cms_context __db = new vuong_cms_context();
            var obj = __db.District.Where(w => w.ProvinceId == province_id).Select(s => new
            {
                s.ProvinceId,
                s.Id,
                s.Name
            });
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ddlWard(int district_id)
        {
            vuong_cms_context __db = new vuong_cms_context();
            var obj = __db.Ward.Where(w => w.DistrictID == district_id).Select(s => new
            {
                s.DistrictID,
                s.Id,
                s.Name
            });
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}