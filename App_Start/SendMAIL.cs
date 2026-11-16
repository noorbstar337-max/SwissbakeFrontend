using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using prolib;
using SwissbakeIn.Entities;

namespace SwissbakeIn.App_Start
{
    public class SendMAIL
    {



        //public static void SendMail(string Name, string email, string Number, string Content, string Subj)
        //{
        //    try
        //    {
        //        email = email.Replace(",,", ",");
        //        string emailid = "noreply@swissbake.bh";
        //        string password = "SwissBake#123";
        //          MailAddress SendFrom = new MailAddress(emailid);
        //        SmtpClient smtp = new SmtpClient();
        //        MailMessage MyMessage = new MailMessage();
        //        MyMessage.From = SendFrom;

        //        string host = HttpContext.Current.Request.Url.Host;

        //        if (host == "localhost")
        //            MyMessage.To.Add("irfansyd@gmail.com");
        //        else
        //        {
        //            if (email == "")
        //            {
        //                MyMessage.To.Add("tahir@swissbake.bh");
        //            }
        //            else
        //            {
        //                MyMessage.To.Add(email);
        //                MyMessage.Bcc.Add("tahir@swissbake.bh");
        //            }
        //            MyMessage.Bcc.Add("accounts@swissbake.bh");
        //        }
        //        MyMessage.Subject = Subj;
        //        MyMessage.Body = Content;
        //        MyMessage.IsBodyHtml = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Port = 587;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new NetworkCredential(emailid, password);
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        smtp.Send(MyMessage);
        //    }
        //    // ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "call", "alert('Your Enquiry has been register Successfully....')", True)
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public static void CareerSendMail(string Name, string email, string Number, string Content, string Subj)
        //{
        //    try
        //    {
        //        email = email.Replace(",,", ",");
        //        string emailid = "support@swissbake.bh";
        //        string password = "SwissBake#123";
        //        MailAddress SendFrom = new MailAddress(emailid);
        //        SmtpClient smtp = new SmtpClient();
        //        MailMessage MyMessage = new MailMessage();
        //        MyMessage.From = SendFrom;

        //        string host = HttpContext.Current.Request.Url.Host;

        //        if (host == "localhost")
        //            MyMessage.To.Add("irfan.bstar@gmail.com");
        //        else
        //        {
        //            MyMessage.To.Add(email);
        //             MyMessage.Bcc.Add("sales.mum@swissbake.bh");
        //        }
        //        MyMessage.Subject = Subj;
        //        MyMessage.Body = Content;
        //        MyMessage.IsBodyHtml = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Port = 587;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new NetworkCredential(emailid, password);
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        smtp.Send(MyMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public static void SupportSendMail(string Name, string email, string Number, string Content, string Subj)
        //{
        //    try
        //    {
        //        email = email.Replace(",,", ",");
        //        string emailid = "support@swissbake.bh";
        //        string password = "SwissBake#123";
        //        MailAddress SendFrom = new MailAddress(emailid);
        //        SmtpClient smtp = new SmtpClient();
        //        MailMessage MyMessage = new MailMessage();
        //        MyMessage.From = SendFrom;

        //        string host = HttpContext.Current.Request.Url.Host;

        //        if (host == "localhost")
        //            MyMessage.To.Add("irfansyd@gmail.com");
        //        else
        //        {
        //            MyMessage.To.Add(email);
        //            MyMessage.Bcc.Add("tahir@swissbake.bh");
        //            MyMessage.Bcc.Add("accounts@swissbake.bh");
        //        }
        //        MyMessage.Subject = Subj;
        //        MyMessage.Body = Content;
        //        MyMessage.IsBodyHtml = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Port = 587;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new NetworkCredential(emailid, password);
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        smtp.Send(MyMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public static void BakerSendMail(string Name, string email, string Number, string Content, string Subj)
        //{
        //    try
        //    {
        //        email = email.Replace(",,", ",");
        //        string emailid = "support@swissbake.bh";
        //        string password = "SwissBake#123";
        //        MailAddress SendFrom = new MailAddress(emailid);
        //        SmtpClient smtp = new SmtpClient();
        //        MailMessage MyMessage = new MailMessage();
        //        MyMessage.From = SendFrom;

        //        string host = HttpContext.Current.Request.Url.Host;

        //        if (host == "localhost")
        //            MyMessage.To.Add("irfansyd@gmail.com");
        //        else
        //        {
        //            MyMessage.To.Add(email);
        //            MyMessage.Bcc.Add("tahir@swissbake.bh");
        //        }
        //        MyMessage.Subject = Subj;
        //        MyMessage.Body = Content;
        //        MyMessage.IsBodyHtml = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Port = 587;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new NetworkCredential(emailid, password);
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        smtp.Send(MyMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static void BakerDirectSendMail(string Name, string email, string Number, string Content, string Subj)
        //{
        //    try
        //    {
        //        email = email.Replace(",,", ",");
        //        string emailid = "support@swissbake.bh";
        //        string password = "SwissBake#123";
        //        MailAddress SendFrom = new MailAddress(emailid);
        //        SmtpClient smtp = new SmtpClient();
        //        MailMessage MyMessage = new MailMessage();
        //        MyMessage.From = SendFrom;

        //        string host = HttpContext.Current.Request.Url.Host;

        //        if (host == "localhost")
        //            MyMessage.To.Add("irfansyd@gmail.com");
        //        else
        //        {
        //            MyMessage.To.Add("tahir@swissbake.bh");
        //            MyMessage.Bcc.Add("sales.mum@swissbake.bh");
        //        }
        //        MyMessage.Subject = Subj;
        //        MyMessage.Body = Content;
        //        MyMessage.IsBodyHtml = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Port = 587;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new NetworkCredential(emailid, password);
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        smtp.Send(MyMessage);
        //    }
        //     catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static void DirectSendMail(string Name, string email, string Number, string Content, string Subj)
        //{
        //    try
        //    {
        //        email = email.Replace(",,", ",");
        //        string emailid = "noreply@swissbake.bh";
        //        string password = "SwissBake#123";
        //        MailAddress SendFrom = new MailAddress(emailid);
        //        SmtpClient smtp = new SmtpClient();
        //        MailMessage MyMessage = new MailMessage();
        //        MyMessage.From = SendFrom;

        //        string host = HttpContext.Current.Request.Url.Host;

        //        if (host == "localhost") { 
        //            MyMessage.To.Add("irfansyd@gmail.com");
        //        }
        //        else
        //        {
        //            MyMessage.To.Add(email);
        //        }
        //        MyMessage.Subject = Subj;
        //        MyMessage.Body = Content;
        //        MyMessage.IsBodyHtml = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Port = 587;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new NetworkCredential(emailid, password);
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        smtp.Send(MyMessage);
        //    }
        //     catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static void FeedbackMail(string email, string Content, string Subj)
        //{
        //    try
        //    {
        //        string emailid = "noreply@swissbake.bh";
        //        string password = "SwissBake#123";
        //        MailAddress SendFrom = new MailAddress(emailid);
        //        SmtpClient smtp = new SmtpClient();
        //        MailMessage MyMessage = new MailMessage();
        //        MyMessage.From = SendFrom;
        //        string host = HttpContext.Current.Request.Url.Host;

        //        if (host == "localhost")
        //        {
        //            MyMessage.To.Add("irfansyd@gmail.com");
        //        }
        //        else
        //        {
        //            MyMessage.To.Add(email);
        //        }
        //        MyMessage.Subject = Subj;
        //        MyMessage.Body = Content;
        //        MyMessage.IsBodyHtml = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Port = 587;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new NetworkCredential(emailid, password);
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        smtp.Send(MyMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static void SendMail(string toemail, string subject, string body)
        {
            try
            {

                string emailid = ConfigurationManager.AppSettings["EmailFrom"];
                string password = ConfigurationManager.AppSettings["EmailPassword"];
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OngoingSMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["OngoingPOSRTSSL"])); //465 - SSL | 587 - TLS
                smtp.EnableSsl = true; smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(emailid, password);
                MailMessage msg = new MailMessage(emailid, toemail);
                msg.Subject = subject;
                msg.IsBodyHtml = true;
                msg.Body = body;
                smtp.Send(msg);
            }
            // ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "call", "alert('Your Enquiry has been register Successfully....')", True)
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendMail(string Name, string email, string Number, string Content, string Subj)
        {
            try
            {
                email = email.Replace(",,", ",");
                string emailid = ConfigurationManager.AppSettings["EmailFrom"];
                string password = ConfigurationManager.AppSettings["EmailPassword"];
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OngoingSMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["OngoingPOSRTSSL"])); //465 - SSL | 587 - TLS
                smtp.EnableSsl = true; smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(emailid, password);

                string to = email;
                if (to == "")
                {
                    //to = "tahir@swissbake.bh";
                    to = "support@swissbake.ch";
                    //to = "irfan.bstar@gmail.com";
                }
                else
                {
                    to = (email);
                }

                //MailMessage msg = new MailMessage(emailid, email);
                MailMessage msg = new MailMessage(emailid, to);
               // msg.Bcc.Add("tahir@swissbake.bh");
                //msg.Bcc.Add("accounts@swissbake.bh");
               // msg.Bcc.Add("debug.ifaada@gmail.com");
                msg.Bcc.Add("irfan.bstar@gmail.com");

                msg.Subject = Subj;
                msg.IsBodyHtml = true;
                msg.Body = Content;
                smtp.Send(msg);

                //string emailid = "noreply@swissbake.bh";
                //string password = "SwissBake#123";
                //MailAddress SendFrom = new MailAddress(emailid);
                //SmtpClient smtp = new SmtpClient();
                //MailMessage MyMessage = new MailMessage();
                //MyMessage.From = SendFrom;

                //string host = HttpContext.Current.Request.Url.Host;

                //if (host == "localhost")
                //    MyMessage.To.Add("irfansyd@gmail.com");
                //else
                //{
                //    if (email == "")
                //    {
                //        MyMessage.To.Add("tahir@swissbake.bh");
                //    }
                //    else
                //    {
                //        MyMessage.To.Add(email);
                //        MyMessage.Bcc.Add("tahir@swissbake.bh");
                //    }
                //    MyMessage.Bcc.Add("accounts@swissbake.bh");
                //}
                //MyMessage.Subject = Subj;
                //MyMessage.Body = Content;
                //MyMessage.IsBodyHtml = true;
                //smtp.UseDefaultCredentials = true;
                //smtp.Port = 587;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential(emailid, password);
                //smtp.Host = "smtp.gmail.com";
                //smtp.EnableSsl = true;
                //smtp.Send(MyMessage);
            }
            // ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "call", "alert('Your Enquiry has been register Successfully....')", True)
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void CareerSendMail(string Name, string email, string Number, string Content, string Subj)
        {
            try
            {
                email = email.Replace(",,", ",");
                string emailid = ConfigurationManager.AppSettings["EmailFrom"];
                string password = ConfigurationManager.AppSettings["EmailPassword"];
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OngoingSMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["OngoingPOSRTSSL"])); //465 - SSL | 587 - TLS
                smtp.EnableSsl = true; smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(emailid, password);
                MailMessage msg = new MailMessage(emailid, email);
                msg.Subject = Subj;
                msg.IsBodyHtml = true;
                msg.Body = Content;
                smtp.Send(msg);

                //string emailid = "support@swissbake.bh";
                //string password = "SwissBake#123";
                //MailAddress SendFrom = new MailAddress(emailid);
                //SmtpClient smtp = new SmtpClient();
                //MailMessage MyMessage = new MailMessage();
                //MyMessage.From = SendFrom;

                //string host = HttpContext.Current.Request.Url.Host;

                //if (host == "localhost")
                //    MyMessage.To.Add("irfan.bstar@gmail.com");
                //else
                //{
                //    MyMessage.To.Add(email);
                //    MyMessage.Bcc.Add("sales.mum@swissbake.bh");
                //}
                //MyMessage.Subject = Subj;
                //MyMessage.Body = Content;
                //MyMessage.IsBodyHtml = true;
                //smtp.UseDefaultCredentials = true;
                //smtp.Port = 587;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential(emailid, password);
                //smtp.Host = "smtp.gmail.com";
                //smtp.EnableSsl = true;
                //smtp.Send(MyMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void SupportSendMail(string Name, string email, string Number, string Content, string Subj)
        {
            try
            {
                email = email.Replace(",,", ",");
                // string emailid = "support@swissbake.bh";
                //string password = "SwissBake#123";
                //string emailid = ConfigurationManager.AppSettings["EmailFrom"];
                string emailid = ConfigurationManager.AppSettings["EmailFrom"];
                string password = ConfigurationManager.AppSettings["EmailPassword"];
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OngoingSMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["OngoingPOSRTSSL"])); //465 - SSL | 587 - TLS
                smtp.EnableSsl = true; smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(emailid, password);
                MailMessage msg = new MailMessage(emailid, "accounts@swissbake.bh");
                msg.Subject = Subj;
                msg.IsBodyHtml = true;
                msg.Body = Content;
                smtp.Send(msg);

                //MailAddress SendFrom = new MailAddress(emailid);
                //SmtpClient smtp = new SmtpClient();
                //MailMessage MyMessage = new MailMessage();
                //MyMessage.From = SendFrom;

                //string host = HttpContext.Current.Request.Url.Host;

                //if (host == "localhost")
                //    MyMessage.To.Add("irfansyd@gmail.com");
                //else
                //{
                //    MyMessage.To.Add(email);
                //    MyMessage.Bcc.Add("tahir@swissbake.bh");
                //    MyMessage.Bcc.Add("accounts@swissbake.bh");
                //}
                //MyMessage.Subject = Subj;
                //MyMessage.Body = Content;
                //MyMessage.IsBodyHtml = true;
                //smtp.UseDefaultCredentials = true;
                //smtp.Port = 587;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential(emailid, password);
                //smtp.Host = "smtp.gmail.com";
                //smtp.EnableSsl = true;
                //smtp.Send(MyMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void BakerSendMail(string Name, string email, string Number, string Content, string Subj)
        {
            try
            {
                email = email.Replace(",,", ",");
                //string emailid = "support@swissbake.bh";
                //string password = "SwissBake#123";
                string emailid = ConfigurationManager.AppSettings["EmailFrom"];
                string password = ConfigurationManager.AppSettings["EmailPassword"];
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OngoingSMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["OngoingPOSRTSSL"])); //465 - SSL | 587 - TLS
                smtp.EnableSsl = true; smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(emailid, password);
                MailMessage msg = new MailMessage(emailid,email);
                msg.Subject = Subj;
                msg.IsBodyHtml = true;
                msg.Body = Content;
                smtp.Send(msg);

                //MailAddress SendFrom = new MailAddress(emailid);
                //SmtpClient smtp = new SmtpClient();
                //MailMessage MyMessage = new MailMessage();
                //MyMessage.From = SendFrom;

                //string host = HttpContext.Current.Request.Url.Host;

                //if (host == "localhost")
                //    MyMessage.To.Add("irfansyd@gmail.com");
                //else
                //{
                //    MyMessage.To.Add(email);
                //    MyMessage.Bcc.Add("tahir@swissbake.bh");
                //}
                //MyMessage.Subject = Subj;
                //MyMessage.Body = Content;
                //MyMessage.IsBodyHtml = true;
                //smtp.UseDefaultCredentials = true;
                //smtp.Port = 587;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential(emailid, password);
                //smtp.Host = "smtp.gmail.com";
                //smtp.EnableSsl = true;
                //smtp.Send(MyMessage);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public static void BakerDirectSendMail(string Name, string email, string Number, string Content, string Subj)
        {
            try
            {
                email = email.Replace(",,", ",");
                string emailid = ConfigurationManager.AppSettings["EmailFrom"];
                string password = ConfigurationManager.AppSettings["EmailPassword"];
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OngoingSMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["OngoingPOSRTSSL"])); //465 - SSL | 587 - TLS
                smtp.EnableSsl = true; smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(emailid, password);
                MailMessage msg = new MailMessage(emailid, "sales.mum@swissbake.bh");
                msg.Subject = Subj;
                msg.IsBodyHtml = true;
                msg.Body = Content;
                smtp.Send(msg);

                //string emailid = "support@swissbake.bh";
                //string password = "SwissBake#123";
                //MailAddress SendFrom = new MailAddress(emailid);
                //SmtpClient smtp = new SmtpClient();
                //MailMessage MyMessage = new MailMessage();
                //MyMessage.From = SendFrom;

                //string host = HttpContext.Current.Request.Url.Host;

                //if (host == "localhost")
                //    MyMessage.To.Add("irfansyd@gmail.com");
                //else
                //{
                //    MyMessage.To.Add("tahir@swissbake.bh");
                //    MyMessage.Bcc.Add("sales.mum@swissbake.bh");
                //}
                //MyMessage.Subject = Subj;
                //MyMessage.Body = Content;
                //MyMessage.IsBodyHtml = true;
                //smtp.UseDefaultCredentials = true;
                //smtp.Port = 587;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential(emailid, password);
                //smtp.Host = "smtp.gmail.com";
                //smtp.EnableSsl = true;
                //smtp.Send(MyMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DirectSendMail(string Name, string email, string Number, string Content, string Subj)
        {
            try
            {
                email = email.Replace(",,", ",");
                string emailid = ConfigurationManager.AppSettings["EmailFrom"];
                string password = ConfigurationManager.AppSettings["EmailPassword"];
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OngoingSMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["OngoingPOSRTSSL"])); //465 - SSL | 587 - TLS
                smtp.EnableSsl = true; smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(emailid, password);
                MailMessage msg = new MailMessage(emailid, email);
                msg.Subject = Subj;
                msg.IsBodyHtml = true;
                msg.Body = Content;
                smtp.Send(msg);

                //string emailid = "noreply@swissbake.bh";
                //string password = "SwissBake#123";
                //MailAddress SendFrom = new MailAddress(emailid);
                //SmtpClient smtp = new SmtpClient();
                //MailMessage MyMessage = new MailMessage();
                //MyMessage.From = SendFrom;

                //string host = HttpContext.Current.Request.Url.Host;

                //if (host == "localhost")
                //{
                //    MyMessage.To.Add("irfansyd@gmail.com");
                //}
                //else
                //{
                //    MyMessage.To.Add(email);
                //}
                //MyMessage.Subject = Subj;
                //MyMessage.Body = Content;
                //MyMessage.IsBodyHtml = true;
                //smtp.UseDefaultCredentials = true;
                //smtp.Port = 587;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential(emailid, password);
                //smtp.Host = "smtp.gmail.com";
                //smtp.EnableSsl = true;
                //smtp.Send(MyMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static JsonResult SendVerifyEmail()
        {
            System.Web.Mvc.JsonResult objsn = new JsonResult();

            try
            {
                string FrontDomain = ConfigurationManager.AppSettings["frontdomain"];

                string link = FrontDomain + "/Misc/VerifyEmail/?link=" + Helper.Encrypt(HttpContext.Current.Session["AppRegId"].ToString());
                string body = "";

                StreamReader read = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/Mail/Verifyemail.html"));
                body = read.ReadToEnd();
                body = body.Replace("#CName", HttpContext.Current.Session["AppRegCompany"].ToString());
                //body = body.Replace("#Accname", form["Desig"]);
                body = body.Replace("#Verifylink", link);
                SendMAIL.DirectSendMail("", HttpContext.Current.Session["AppRegMail"].ToString(), "", body, "Email Verification | SwissBake® B2B Portal");

                //HttpContext.Current.Session["AppRegId"] = null;
               // HttpContext.Current.Session["AppRegMail"] = null;
               // HttpContext.Current.Session["regId"] = null;

                objsn.Data = "true";
                objsn.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                return objsn; // Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // return System.Web.Mvc.Controller.Json(false, JsonRequestBehavior.AllowGet);
                objsn.Data = "false";
                objsn.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                return objsn;
            }
        }


        public static JsonResult SendRegistrationEmail()
        {
            System.Web.Mvc.JsonResult objsn = new JsonResult();

            try
            {
                //string link = HttpContext.Current.Request.Url.Host + "/Misc/VerifyEmail/?link=" + Helper.Encrypt(HttpContext.Current.Session["AppRegId"].ToString());
                string body = "";

                StreamReader read = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/Mail/Registration.html"));
                body = read.ReadToEnd();
                body = body.Replace("#CName", HttpContext.Current.Session["AppRegCompany"].ToString());
                body = body.Replace("#CPerson", HttpContext.Current.Session["AppRegName"].ToString());
                body = body.Replace("#Email", HttpContext.Current.Session["AppRegMail"].ToString());
                body = body.Replace("#TelNumber", HttpContext.Current.Session["AppRegMobile"].ToString());
                //body = body.Replace("#Accname", form["Desig"]);
                //body = body.Replace("#Verifylink", link);
                SendMAIL.DirectSendMail("", HttpContext.Current.Session["AppRegMail"].ToString(), "", body, "You have successfully registered | SwissBake® B2B Portal");

                HttpContext.Current.Session["AppRegId"] = null;
                HttpContext.Current.Session["AppRegMail"] = null;
                HttpContext.Current.Session["regId"] = null;
                HttpContext.Current.Session["AppRegCompany"] = null;
                HttpContext.Current.Session["AppRegMobile"] = null;
                HttpContext.Current.Session["AppRegName"] = null;

                objsn.Data = "true";
                objsn.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                return objsn; // Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // return System.Web.Mvc.Controller.Json(false, JsonRequestBehavior.AllowGet);
                objsn.Data = "false";
                objsn.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                return objsn;
            }
        }




        public static void FeedbackMail(string email, string Content, string Subj)
        {
            try
            {
                string emailid = "noreply@swissbake.bh";
                string password = "SwissBake#123";
                MailAddress SendFrom = new MailAddress(emailid);
                SmtpClient smtp = new SmtpClient();
                MailMessage MyMessage = new MailMessage();
                MyMessage.From = SendFrom;
                string host = HttpContext.Current.Request.Url.Host;

                if (host == "localhost")
                {
                    MyMessage.To.Add("irfansyd@gmail.com");
                }
                else
                {
                    MyMessage.To.Add(email);
                }
                MyMessage.Subject = Subj;
                MyMessage.Body = Content;
                MyMessage.IsBodyHtml = true;
                smtp.UseDefaultCredentials = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(emailid, password);
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(MyMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}