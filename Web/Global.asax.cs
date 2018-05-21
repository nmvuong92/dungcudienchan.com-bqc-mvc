using System;
using System.Data.Entity;
using System.Globalization;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VD.Lib;
using Web.Controllers;
using Web.Mappers;

namespace Web
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //wepb api route
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //UnityConfig.RegisterComponents();

            //chỉ sử dụng 1 view razor thôi
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());//add razor view engine


            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();

            //disabled

            //Enable or Disable Client Side Validation at Application Level
            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
            //
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new MyDateTimeModelBinder());
           ModelBinders.Binders.Add(typeof(int?), new IntegerModelBinder());
           ModelBinders.Binders.Add(typeof(int), new IntegerModelBinder());


           GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
        /* ALLOW HTTPS
        protected void Application_BeginRequest()
        {
            if (!Context.Request.IsSecureConnection)
                Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));
        }*/
      
        public class MyDateTimeModelBinder : DefaultModelBinder
        {
            public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

                if (!string.IsNullOrEmpty(displayFormat) && value != null)
                {
                    DateTime date;
                    displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                    // use the format specified in the DisplayFormat attribute to parse the date
                    if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        return date;
                    }
                    else
                    {
                        bindingContext.ModelState.AddModelError(
                            bindingContext.ModelName,
                            string.Format("{0} is an invalid date format", value.AttemptedValue)
                        );
                    }
                }

                return base.BindModel(controllerContext, bindingContext);
            }
        }

        public class DecimalModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                ValueProviderResult valueResult = bindingContext.ValueProvider
                    .GetValue(bindingContext.ModelName);
                var modelState = new ModelState { Value = valueResult };
                object actualValue = null;
                if (valueResult.AttemptedValue != string.Empty)
                {
                    try
                    {
                        actualValue = Convert.ToDecimal(valueResult.AttemptedValue, new CultureInfo(myCookies.GetLang()));
                    }
                    catch (FormatException e)
                    {
                        modelState.Errors.Add(e);
                    }
                }
                bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
                return actualValue;
            }
        }

        public class IntegerModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                ValueProviderResult valueResult = bindingContext.ValueProvider
                    .GetValue(bindingContext.ModelName);
                ModelState modelState = new ModelState { Value = valueResult };
                object actualValue = null;
                if (valueResult!=null && !string.IsNullOrWhiteSpace(valueResult.AttemptedValue))
                {
                    string c = valueResult.AttemptedValue.Replace(".", string.Empty).Replace(",",string.Empty);
                    try
                    {
                        actualValue = Convert.ToInt32(c, CultureInfo.CurrentCulture);
                    }
                    catch (FormatException e)
                    {
                        modelState.Errors.Add(e);
                    }
                }

                bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

                return actualValue;
            }
        }


        public bool err = false;
        protected void Application_EndRequest()
        {
            if (err == false)
            {
                return;
            }
            var context = new HttpContextWrapper(Context);
            bool __isAjax = context.Request.IsAjaxRequest();
            if (Context.Response.StatusCode == 302 && Context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Context.Response.Clear();
                Context.Response.StatusCode = 401;
            }

            if (Context.Response.StatusCode == 404)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.DataTokens["area"] = ""; // In case controller is in another area
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "Page404";

                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
            else if (Context.Response.StatusCode == 400)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.DataTokens["area"] = ""; // In case controller is in another area
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "Page400";

                new JsonResult
                {
                    Data = new
                    {
                        returnUrl = "foo"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                
                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
            else if (Context.Response.StatusCode == 401)
            {
                Response.Clear();

       
                var rd = new RouteData();
                rd.DataTokens["area"] = ""; // In case controller is in another area
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "Page401";
                rd.Values["isAjax"] = __isAjax;
                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
            else if (Context.Response.StatusCode == 403)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.DataTokens["area"] = ""; // In case controller is in another area
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "Page403";

                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
            else if (Context.Response.StatusCode == 500)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.DataTokens["area"] = ""; // In case controller is in another area
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "Page500";

                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
            else if (Context.Response.StatusCode == 503)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.DataTokens["area"] = ""; // In case controller is in another area
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "Page503";

                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
        }
    }
}
