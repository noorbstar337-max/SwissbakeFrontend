using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SwissbakeIn.App_Start
{
    public class Authenticate : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try

            {
              //  string url = HttpContext.Current.Request.UrlReferrer;
                string url = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
                if (url != "/login" || url != "/Login")
                {
                    HttpContext.Current.Session["Redirect_Url"] = url;
                }
                if (HttpContext.Current.Session["U_Id"].ToString() != null || HttpContext.Current.Session["U_Id"].ToString() != "" && HttpContext.Current.Session["U_Verified"].ToString() == "True" || HttpContext.Current.Session["U_Verified"].ToString() == "1")
                {
                    return;
                }
            }
            catch
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                HttpContext.Current.Session["Redirect_Url"] = url;
                if(HttpContext.Current.Session["U_Id"] != null)
                {
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "controller", "Home" },
                    { "action", "Login" }
                   });
                }
                   
            }
        }


    }
}