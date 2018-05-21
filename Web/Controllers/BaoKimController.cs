using System;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using VD.Data;
using VD.Data.Entity.BQC;
using VD.Lib;
using VD.Lib.DTO;
using Web.SaleCardControllerService;
using Web.Security;
using Web.ViewModels.BaoKim;

namespace Web.Controllers
{
    [myAuth]
    public class BaoKimController : BaseController
    {
        // GET: BaoKim
        public ActionResult NapThe()
        {
            return View();
        }

        [HttpPost]
      
        public ActionResult NapThe(NapTheVM model)
        {
            rs r;
            var ss_captcha = mySessions.Get("Captcha" + "baokimthecao");
            bool true_capt = false;
            if (ss_captcha != null)
            {
                if (ss_captcha.ToString().ToLower() == model.Captcha.ToLower())
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
                string card_id1 = "";
                string ten = "";

                if (model.nhamang == "Viettel")
                {
                    card_id1 = "107";
                    ten = "VIETTEL";
                }
                else if (model.nhamang == "Mobile")
                {
                    card_id1 = "92";
                    ten = "MOBILE PHONE";
                }
                else if (model.nhamang == "Vina")
                {
                    card_id1 = "93";
                    ten = "VINA PHONE";
                }
                else if (model.nhamang == "Gate")
                {
                    card_id1 = "120";
                    ten = "GATE";
                }
                else if (model.nhamang == "VTC")
                {
                    card_id1 = "121";
                    ten = "VTC";
                }

                string secure_pass = myAppSetings.Setting<string>("secure_pass");//"e81652ffe12cd0f5";
               
                string soseri = model.Seri;
                string sopin = model.pin;
                string merchant_id = myAppSetings.Setting<string>("merchant_id"); //"30861";
                string api_username = myAppSetings.Setting<string>("api_username");// "dienchanonlinecom";
                string api_password = myAppSetings.Setting<string>("api_password"); //"THc56s6QC3hnJ3eaKo5D";
                string card_id = card_id1;
                string transaction_id = Convert.ToString(time(DateTime.UtcNow));

                string data_sign = api_password + api_username + card_id + merchant_id + sopin + soseri + transaction_id;
                data_sign = FormsAuthentication.HashPasswordForStoringInConfigFile(secure_pass + data_sign, "MD5").ToLower();


                //BKTransactionAPI bk = new BKTransactionAPI();

                SaleCardControllerPortTypeClient bk = new SaleCardControllerPortTypeClient();
                TopupToMerchantRequest sendtest = new TopupToMerchantRequest();
                sendtest.api_username = api_username;
                sendtest.api_password = api_password;
                sendtest.card_id = card_id;
                sendtest.seri_field = soseri;
                sendtest.pin_field = sopin;
                sendtest.merchant_id = merchant_id;
                sendtest.transaction_id = transaction_id; //Mã TransactionId xác định giao dịch bên khách hàng
                sendtest.data_sign = data_sign; //Trường mô tả tính toàn vẹn dữ liệu gửi lên (Mô tả bên dưới)


                TopupToMerchantResponse kqtest = bk.DoTopupToMerchant(sendtest);
                
                if (kqtest.error_code == "0")
                {
                    //bool km = false;
                    //float tien = Convert.ToInt32(kqtest.info_card);
                    //float khuyenmai = 0;
                    //string sqluser = "SELECT * FROM MEMB_INFO where memb___id='" + txtTendn.Text + "'";
                    //DataTable dt = ExeSQLToDataTable(sqluser);
                    //if (dt.Rows.Count == 0)
                    //{
                    //    label1.Text = "Tên đăng nhập này không tồn tại";
                    //}
                    //else
                    //{
                    //    if (km)
                    //    {
                    //        float tienkm = khuyenmai * tien / 100;
                    //        string query_gcoin_km = "UPDATE MEMB_INFO SET gcoin_km=gcoin_km+" + tien + " where memb___id='" + txtTendn.Text + "'";
                    //        ExeNonSQL(query_gcoin_km);
                    //    }
                    //    string query_update = "UPDATE MEMB_INFO SET gcoin=gcoin+" + tien + " where memb___id='" + txtTendn.Text + "'";
                    //    ExeNonSQL(query_update);
                    //}
                    //string log = DateTime.Now.ToLongDateString() + "      " + txtSoPin.Text + "        " + txtSoSeri.Text + "         " + slPhone.SelectedValue.ToString() + "        " + kqtest.info_card + "         " + txtTendn.Text;


                    var au = MySsAuthUsers.GetAuth();
                    using (var __db = new vuong_cms_context())
                    {
                        //var giavip1 = __setting.menh_gia_the_cao_mua_thanh_vien_vip1;
                       
                        using (var tx = new TransactionScope())
                        {
                            int __menhgia = int.Parse(kqtest.info_card);
                            var user = __db.Users.Find(au.ID);
                            user.ViTien += __menhgia;
                            __db.SaveChanges();


                            //var user = __db.Users.Find(au.ID);
                            LichSuNapThe lsnap = new LichSuNapThe();
                            lsnap.menhgia = int.Parse(kqtest.info_card);
                            lsnap.ngay = DateTime.Now;
                            lsnap.UserId = au.ID;
                            lsnap.tenthe = ten;
                            __db.LichSuNapThe.Add(lsnap);
                            __db.SaveChanges();
                            //
                            tx.Complete();
                           
                            r = rs.T("Bạn đã nạp thành công thẻ " + ten + " với mệnh giá : " + kqtest.info_card + " VNĐ");
                        }
                    }
                }
                else

                {
                  
                    r = rs.F("Thông tin thẻ cào của bạn không hợp lệ hoặc đã được sử dụng!: " + kqtest.error_message); //"Thong tin loi :  " + kqtest.error_code + "(" + kqtest.transaction_id + "):" + kqtest.error_message.ToString();
                }
            }
            else
            {
                r = rs.F("Lỗi!");//"Thong tin loi :  " + kqtest.error_code + "(" + kqtest.transaction_id + "):" + kqtest.error_message.ToString();
            }
            if (r.r)
            {
                TempData["message"] = r.m;
                return RedirectToAction("NangCapVip", "User");
            }
           
            ModelState.AddModelError(string.Empty, r.m);
            return View(model);
        }
        [NonAction]
        private long time(DateTime time)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan span = time.Subtract(unixEpoch);
            return (long)span.TotalSeconds;
        }
    }
}