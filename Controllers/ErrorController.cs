using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwissbakeIn.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("NotFound");
        }

        // GET: Error/Details/5
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
        
    }
}
