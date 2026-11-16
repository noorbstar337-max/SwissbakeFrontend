using SwissbakeIn.App_Start;
using prolib;
using reCAPTCHA.MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;

namespace SwissbakeIn.Controllers
{
    [BuildCart]
    [ETag]
    [WhitespaceFilter]
    [GZipOrDeflate]
    public class MiscController : Controller
    {
        Home dbhome = new Home();
        Account dbaccount = new Account();


        public ActionResult Career()
        {
            ViewBag.error = Session["error"];
            Session["error"] = null;
            return View();
        }

        [HttpPost]
        [CaptchaValidator]
        public ActionResult Career(FormCollection form, bool captchaValid, HttpPostedFileBase file)
        {
            if (captchaValid == true)
            {
                var allowedExtensions = new[] {".jpg", ".xlsx", ".docx", ".pdf", ".doc"};
                var Image_url = file.ToString(); //getting complete url  
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "_" + form["Desig"].Replace(" ","-") + ext; //appending the name with id  
                                                               // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("~/Content/Career/"), myfile);
                    Image_url = "/Content/Career/" + myfile;
                    file.SaveAs(path);

                    string formno = dbhome.InsertCarr(form["Name"], form["Email"], "Bahrainn", form["Mobile"], form["CurrDesig"], "", form["CurrCtc"], "", Image_url, "", "", "", "", form["Desig"], "", "InsertCarrer");

                    string body = "";

                    StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/Career.html"));
                    body = read.ReadToEnd();
                    body = body.Replace("#NAME_APPLICANT", form["Name"]);
                    body = body.Replace("#POSITION", form["Desig"]);
                    body = body.Replace("#APPLICATIONNO", formno);

                    SendMAIL.CareerSendMail(form["Name"], form["Email"], form["Mobile"], body, "Your application for the position of " + form["Desig"] + " at Swiss Bake Ingredients Pvt. Ltd. | Application no. " + formno + "");

                    read.Dispose();

                    body = "<strong>APPLICATION DETAIL:</strong><br><br><table><tr><td>NAME </td><td>:&nbsp;" + form["Name"] + "</td></tr><tr><td>EMAIL ID </td><td>:&nbsp;" + form["Email"] + "</td></tr><tr><td>MOBILE NO. </td><td>:&nbsp;" + form["Mobile"] + "</td></tr><tr><td>APPLYINNG FOR </td><td>:&nbsp;" + form["Desig"] + "</td></tr><tr><td>CURRENT DESIGNATION </td><td>:&nbsp;" + form["CurrDesig"] + "</td></tr><tr><td>CURRENT CTC </td><td>:&nbsp;" + form["CurrCtc"] + "</td></tr></table><br>";

                    SendMAIL.CareerSendMail("", "tahir@swissbake.bh", "", body, "Career application from swissbake.bh | Application No." + formno);



                    Session["error"] = "sucess()";
                }
                else
                {
                    ViewBag.message = "Please choose only Image file";
                }
                
            }

            return Redirect("/misc/career");
        }


        // GET: Misc
        public ActionResult Privacy()
        {
            return View();
        }

        // GET: Misc/Details/5
        public ActionResult Tou()
        {
            return View();
        }

        // GET: Misc/Create
        public ActionResult Shipping()
        {
            return View();
        }

        public ActionResult VerifyEmail(string link)
        {
            try
            {

                DataSet ds = new DataSet();

                ds = dbaccount.CheckEmailVerify(Convert.ToInt32(Helper.Decrypt(link)));

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Acc_EmailVerified"]) == false)
                {
                    int res = dbaccount.VerifyEmail(Convert.ToInt32(Helper.Decrypt(link)));
                    // Session["EmailVerifyMessage"] = "Your Registration Request has been submitted Sucessfully.You will soon receive Confirmation mail";
                    if (ds.Tables[0].Rows[0]["Acc_Status"].ToString() == "Active")
                    {
                        Session["EmailVerifyMessage"] = "Your email id is sucessfully verified. You can login now.";
                    }
                    else
                    {
                        Session["EmailVerifyMessage"] = "Your Registration Request has been already submitted.You will soon receive Confirmation mail";
                    }
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["Acc_Status"].ToString() == "Active")
                    {
                        Session["EmailVerifyMessage"] = "Your email id is sucessfully verified. You can login now.";
                    }
                    else
                    {
                        Session["EmailVerifyMessage"] = "Your Registration Request has been already submitted.You will soon receive Confirmation mail";
                    }
                    
                }
                

                return Redirect("/login");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }


        public ActionResult Pagenotfound()
        {
            return View();
        }


        public ActionResult ComingSoon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string email)
        {
            try
            {
                string emailto = ConfigurationManager.AppSettings["EmailTo"];
                SendMAIL.SendMail(emailto, "New Subscription Request", $"New Subscription Request Received from Email: {email}");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error_message = "Some error occured while processig your request. Please try again after a while." });
            }
        }
    }
}
