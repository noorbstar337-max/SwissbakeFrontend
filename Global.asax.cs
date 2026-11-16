using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


    public class global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(SwissbakeIn.WebApiConfig.Register);
            SwissbakeIn.FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            SwissbakeIn.RouteConfig.RegisterRoutes(RouteTable.Routes);
            SwissbakeIn.BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cartCookie;
            cartCookie = Request.Cookies.Get("TheCart"); // HttpCookie("")
            if ((cartCookie == null))
            {
                cartCookie = new HttpCookie("TheCart");
                cartCookie.Value = "";
                cartCookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(cartCookie);
            }
        }

    }
