using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web.Mvc;
using VD.Data;
using VD.Data.Entity.BQC;
using VD.Lib;
using VD.Lib.DTO;
using Web.NganLuongMobileCard;
using Web.ViewModels.BaoKim;

namespace Web.Controllers
{
    public class NganLuongMobileCardController : BaseController
    {
        // GET: NganLuongMobileCardDefault
        [NonAction]
        public List<SelectListItem> GetDDLNhaMang()
        {
            return new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value = "VIETTEL",
                    Text = "VietTel"
                },
                new SelectListItem()
                {
                    Value = "VNP",
                    Text = "Vinaphone"
                },
                new SelectListItem()
                {
                    Value = "VMS",
                    Text = "Mobiphone"
                },
                new SelectListItem()
                {
                    Value = "VCOIN",
                    Text = "Vcoin"
                },
                new SelectListItem()
                {
                    Value = "GATE",
                    Text = "Gate"
                }
            };
        } 
        public ActionResult NapTheCao()
        {
            ViewBag.ddlNhaMang = GetDDLNhaMang();
            NapTheVM model = new NapTheVM();
            model.nhamang = "VIETTEL";
            return View();
        }

        [HttpPost]
        public ActionResult NapTheCao(NapTheVM vm)
        {
            rs r; //nganluongthecao
            var ss_captcha = mySessions.Get("Captcha" + "nganluongthecao");
            bool true_capt = false;
            if (ss_captcha != null)
            {
                if (ss_captcha.ToString().ToLower() == vm.Captcha.ToLower())
                {
                    true_capt = true;

                }
            }

            if (!true_capt)
            {
                ModelState.AddModelError(string.Empty, "Mã captcha không chính xác!");
            }

            if (ModelState.IsValid)
            {

                RequestInfo info = new RequestInfo();
                //Merchant site id
                info.Merchant_id = "52343";
                //Email tài khoản nhận tiền khi nạp
                info.Merchant_acount = "jayleefac@gmail.com";
                //Mật khẩu giao tiếp khi đăng ký merchant site
                info.Merchant_password = "6f0d030a8ef75f776eebb73c48522991";

                //Nhà mạng
                info.CardType = vm.nhamang;
                info.Pincard = vm.pin;

                //Mã đơn hàng
                info.Refcode = (new Random().Next(0, 10000)).ToString();
                info.SerialCard = vm.Seri;

                ResponseInfo resutl = NLCardLib.CardChage(info);
                String html = "";


                if (resutl.Errorcode.Equals("00"))
                {
                    html += "<div>" + resutl.Message + "</div>";
                    html += "<div>Số tiền nạp : <b>" + resutl.Card_amount + "</b> đ</div>";
                    html += "<div>Mã giao dịch : <b>" + resutl.TransactionID + "</b></div>";
                    html += "<div>Mã đơn hàng : <b>" + resutl.Refcode + "</b></div>";


                    var au = MySsAuthUsers.GetAuth();
                    var giavip1 = __setting.menh_gia_the_cao_mua_thanh_vien_vip1;

                    using (var __db = new vuong_cms_context())
                    {
                        //var giavip1 = __setting.menh_gia_the_cao_mua_thanh_vien_vip1;

                        using (var tx = new TransactionScope())
                        {
                            try
                            {
                                int __menhgia = int.Parse(resutl.Card_amount);
                                var user = __db.Users.Find(au.ID);
                                user.ViTien += __menhgia;
                                if (user.ViTien >= giavip1)
                                {
                                    user.ViTien -= giavip1;
                                    user.IsVip1 = true;
                                    user.NgayVip = DateTime.Now;
                                    user.ChuThichVip = "Ngân Lượng thẻ cào";
                                    __db.SaveChanges();

                                    //ls giao dich
                                    LichSuGiaoDich lsgiaodich = new LichSuGiaoDich();
                                    lsgiaodich.UserId = user.Id;
                                    lsgiaodich.ngaygiaodich = DateTime.Now;
                                    lsgiaodich.menhgia = giavip1;
                                    lsgiaodich.tengiaodich = "Nâng cấp VIP";
                                    __db.LichSuGiaoDich.Add(lsgiaodich);
                                    __db.SaveChanges();
                                }
                                else
                                {
                                    TempData["message"] = "Không đủ tiền thanh toán vui lòng nạp thêm!";
                                }

                                //ls napthe
                                LichSuNapThe lsnap = new LichSuNapThe();
                                lsnap.menhgia = __menhgia;
                                lsnap.ngay = DateTime.Now;
                                lsnap.UserId = user.Id;
                                lsnap.tenthe = "Ngân lượng thẻ cào";
                                __db.LichSuNapThe.Add(lsnap);
                                __db.SaveChanges();

                                //
                                tx.Complete();

                                r = rs.T(html);
                            }
                            catch (Exception ex)
                            {
                                r = rs.F(ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    html += "<div>Nạp thẻ không thành công</div>";
                    html += "<div>" + resutl.Message + "</div>";
                    r = rs.F(html);
                }
                if (r.r)
                {
                    return RedirectToAction("Profile", "User");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nạp thẻ không thành công: " + resutl.Message);
                }
            }
            ViewBag.ddlNhaMang = GetDDLNhaMang();
            return View(vm);
        }
    }
}