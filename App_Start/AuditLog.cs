using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using prolib;

namespace SwissbakeIn.App_Start
{
    
    public class AuditLog : ActionFilterAttribute 
    {
        Account dbAt = new Account();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string logid;
            string type;
            string memid;

            
            if (HttpContext.Current.Session["logid"] == null)
            {
                logid = "";
            }
            else
            {
                
                logid = BstarGeneral.GetIntVal(HttpContext.Current.Session["logid"].ToString()).ToString();
            }
            

            if (HttpContext.Current.Session["membertype"] == null)
            {
                type = "";
            }
            else
            {
                type = BstarGeneral.GetStringVal(HttpContext.Current.Session["membertype"].ToString()).ToString();
            }


            if (HttpContext.Current.Session["memberid"] == null)
            {
                memid = "";
            }
            else
            {
                memid = BstarGeneral.GetIntVal(HttpContext.Current.Session["memberid"].ToString()).ToString();
            } 
            DataSet auditdt = dbAt.InsertTraillog(logid,type , memid , HttpContext.Current.Request.Url.AbsolutePath, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            //string logid = HttpContext.Current.Session["logid"].ToString();
            if (System.Web.HttpContext.Current.Session["logid"] == null | System.Web.HttpContext.Current.Session["memberid"]  == null)
            {
                HttpContext.Current.Session["logid"] = auditdt.Tables[0].Rows[0]["Audlog_Id"];
                HttpContext.Current.Session["memberid"] = auditdt.Tables[0].Rows[0]["Audtlog_MemberId"];
                HttpContext.Current.Session["membertype"] = auditdt.Tables[0].Rows[0]["Audtlog_MemberType"];
            }

        }
}
}