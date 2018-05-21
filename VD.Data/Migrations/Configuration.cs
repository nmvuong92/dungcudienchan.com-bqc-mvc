
using System.Data.Entity;
using System.Linq;
using VD.Data.Entity;
using VD.Data.Entity.BQC;
using VD.Data.Entity.MF;
using VD.Lib;

namespace VD.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using VD.Data.Base;
    using VD.Data.Entity.Logging;

    internal sealed class Configuration : DbMigrationsConfiguration<VD.Data.vuong_cms_context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(vuong_cms_context context)
        {
            CacheServ.ClearAll();

            return;
            context.Confs.Add(new Conf()
            {
                Key = "html_taikhoannganhang",
                Content = "",
                IsHtmlEditor = true,
                Description = "Tài khoản ngân hàng"
            });
            context.SaveChanges();
           
            context.Category.AddOrUpdate(Category.seed());
            context.SaveChanges();
            context.Article.AddOrUpdate(Article.seed());
            context.SaveChanges();
            context.Product.AddOrUpdate(Product.seed());
            context.SaveChanges();
            //settings:
            context.Settings.AddOrUpdate(Setting.seed());
            context.SaveChanges();

            //role
            context.Roles.AddOrUpdate(Role.seed());
            context.SaveChanges();

            //flag
            context.Langs.AddOrUpdate(Lang.seed());
            context.SaveChanges();

            //Confs
            context.Confs.AddOrUpdate(Conf.seed());
            context.SaveChanges();


            //LogType
            context.LogType.AddOrUpdate(LogType.seed());
            context.SaveChanges();

            //UserStatus
            context.UserStatus.AddOrUpdate(UserStatus.seed());
            context.SaveChanges();

            //user
            context.Users.AddOrUpdate(User.seed());
            context.SaveChanges();


            context.ThongTin.AddOrUpdate(ThongTin.seed());
            context.SaveChanges();
         
            //
            context.ThongBao.AddOrUpdate(ThongBao.seed());
            context.SaveChanges();
        }
    }
}
