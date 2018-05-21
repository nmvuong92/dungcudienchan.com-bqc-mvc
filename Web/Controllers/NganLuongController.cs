using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;
using VD.Data;
using VD.Data.Entity.BQC;
using VD.Lib.Encode;
using Web.NganLuong;
using Web.Security;

namespace Web.Controllers
{

    [myAuth]
    public class NganLuongController : BaseController
    {

        private string merchant_site_code = "52343";
        private string merchant_pass = "6f0d030a8ef75f776eebb73c48522991";
        private string receiver = "jayleefac@gmail.com";


        // GET: NganLuong
        public ActionResult ThanhToan()
        {
            SimpleAES __aes = new SimpleAES();
            var uid = MySsAuthUsers.GetAuth().ID;
            NL_Checkout nlcheckout = new NL_Checkout();
            nlcheckout.merchant_site_code = this.merchant_site_code;
            nlcheckout.secure_pass = this.merchant_pass;

            var rnd = new Random(DateTime.Now.Millisecond);
            int oderID = rnd.Next(0, 3000);
            DateTime dtNow = DateTime.Now;

            string strOrderID = __aes.EncryptToString(uid.ToString());//MÃ USER ENCODED //dtNow.Year.ToString() + dtNow.Month.ToString() + dtNow.Day.ToString() + dtNow.Hour.ToString() + dtNow.Minute.ToString() + dtNow.Second.ToString();

            string order_description = ""; //"Bình nước & Lock Lock 1.2 lít";
            string buyer_info = ""; //"Họ tên người mua *|* Địa chỉ Email *|* Điện thoại *|* Địa chỉ nhận hàng";// "Họ tên người mua *|* Địa chỉ Email *|* Điện thoại *|* Địa chỉ nhận hàng";
            string affiliate_code = "";
            string rs = nlcheckout.buildCheckoutUrlNew("http://dienchanonline.com/nganluong/success", this.receiver, "", strOrderID, __setting.menh_gia_the_cao_mua_thanh_vien_vip1.ToString(), "vnd", 1, 0, 0, 0, 0, order_description, buyer_info, affiliate_code);
            return Redirect(rs);
        }
        public ActionResult Success()
        {
            var au = MySsAuthUsers.GetAuth();
            try
            {
                String return_url = Request["return_url"];
                String receiver = Request["receiver"];
                String transaction_info = Request["transaction_info"];
                String order_code = Request["order_code"];
                String price = Request["price"];
                String payment_id = Request["payment_id"];
                String payment_type = Request["payment_type"];
                String error_text = Request["error_text"];
                String secure_code = Request["secure_code"];

                NL_Checkout checkOut = new NL_Checkout();
                bool rs = checkOut.verifyPaymentUrl(transaction_info, order_code, price, payment_id, payment_type, error_text, secure_code);
                var giavip1 = __setting.menh_gia_the_cao_mua_thanh_vien_vip1;
                if (rs)
                {
                    using (var __db = new vuong_cms_context())
                    {
                        using (var tx = new TransactionScope())
                        {
                            int __menhgia = int.Parse(price.Remove(',').Remove('.'));
                            var user = __db.Users.Find(au.ID);
                            user.ViTien += __menhgia;
                            if (user.ViTien >= giavip1)
                            {
                                user.ViTien -= giavip1;
                                user.IsVip1 = true;
                                user.NgayVip = DateTime.Now;
                                user.ChuThichVip = "Nạp Ngân Lượng chuyển tiền";
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
                            lsnap.tenthe = "Ngân lượng chuyển tiền";
                            __db.LichSuNapThe.Add(lsnap);
                            __db.SaveChanges();
                            //tx complete
                            tx.Complete();
                        }
                    }
                    return RedirectToAction("Profile", "User");
                }
                else
                {
                    TempData["message"] = "Thanh toán không thành công";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            return RedirectToAction("Profile", "User");
        }
        public ActionResult Index()
        {
            return Content("OK");

        }
    }
}