using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using prolib;
//using SwissbakeIn.Models;
using SwissbakeIn.Entities;
using SwissbakeIn.App_Start;
using SwissbakeIn.Controllers;
using System.Text.RegularExpressions;
//using System.Data.DataSetExtensions;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Web.Helpers;
using System.Web.Services.Description;
using System.Xml.Linq;



namespace SwissbakeIn.Controllers
{
    public class AccessController : Controller
    {
        Checkexist dbcheck = new Checkexist();
        Account dbaccount = new Account();
        Product dbPt = new Product();
        Home dbhome = new Home();
        Order dbOt = new Order();
        payment dbPayt = new payment();
        Cart dbCart = new Cart();
        Recipe dbrecipe = new Recipe();
        Category dbct = new Category();
        int count;
        double PID, Amount, vatinr, vatper, rate, Quantity, Packing_Weight, TotalWeight, Product_MOQ;
        string Code, ProductName, ProdImg, unit, href, cookiePID, Product_linkas, subject;
        private Random _random = new Random();
        public HttpCookie cartCookie;

        public JsonResult SendOtp(string Id)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            ds = dbaccount.GetAccDetById(Convert.ToInt32(Id), "getContactDetailById");
            dt = ds.Tables[0];

            string number = dt.Rows[0][0].ToString();
            //string number = 7977628492.ToString();
            string name = dt.Rows[0][3].ToString();
            string Otp = _random.Next(100000, 999999).ToString("D6");

            string mess = "Hi " + name.ToUpper() + ", Enter " + Otp + " as the verification code to verify your mobile number " + number + " for your account on www.swissbake.bh. Valid for 5 minutes. Do not share with anyone.";


            string resp = SendSMS.Send(number, mess);

            Session["Otp"] = Otp;
            Session["OtpTime"] = DateTime.Now.AddMinutes(5);

            var ennumber = number;

            var firstDigits = ennumber.Substring(0, 7);
            var lastDigits = ennumber.Substring(ennumber.Length - 3, 3);

            var requiredMask = new String('*', ennumber.Length - lastDigits.Length);

            var maskedString = string.Concat(requiredMask, lastDigits);
            //var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");



            //string en = new Array(number.Length - 3).join('x') + number.Substring(number.Length - 2, 2);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            row = new Dictionary<string, object>();
            row.Add("Resp", resp);
            row.Add("Number", maskedString);
            rows.Add(row);




            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerifyMob(string otp)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                if (Session["Otp"] != null && Session["OtpTime"] != null)
                {
                    string code = Session["Otp"].ToString();

                    if (Convert.ToDateTime(Session["OtpTime"].ToString()) >= Convert.ToDateTime(DateTime.Now))
                    {
                        if (otp == code)
                        {
                            if (Session["AppRegId"] != null)
                            {
                                dbaccount.VerifyMob(Convert.ToInt32(Session["AppRegId"]));
                                ds = dbaccount.GetAccDetById(Convert.ToInt32(Session["AppRegId"]), "GetInactUserDetById");
                                dt = ds.Tables[0];

                                dbaccount.UpdateAccDuedays(Convert.ToInt32(dt.Rows[0][0]));

                                string body = "";

                                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/Registrationtoclient.html"));
                                body = read.ReadToEnd();
                                //body = body.Replace("#NAME", dt.Rows[0]["Contact_ContactPerson"].ToString());
                                body = body.Replace("#CName", dt.Rows[0]["Acc_Name"].ToString());
                                //body = body.Replace("#SName", dt.Rows[0][0].ToString());
                                //body = body.Replace("#CPerson", dt.Rows[0]["Contact_ContactPerson"].ToString());
                                //body = body.Replace("#Email", dt.Rows[0]["Cust_EmailId"].ToString());
                                //body = body.Replace("#Email", "irfan.shaikh@acaira.in");
                                //body = body.Replace("#TelNumber", dt.Rows[0]["Contact_Mobile"].ToString());
                                SendMAIL.SendMail("", dt.Rows[0]["Cust_EmailId"].ToString(), "", body, "Thank you ! for your registration | SwissBake® B2B Portal");

                                StreamReader read1 = new StreamReader(Server.MapPath("~/Content/Mail/Registrationtoadmin.html"));
                                body = read1.ReadToEnd();
                                //body = body.Replace("#CName", dt.Rows[0]["Acc_Name"].ToString());
                                body = body.Replace("#CPerson", dt.Rows[0]["Contact_ContactPerson"].ToString());
                                body = body.Replace("#Email", dt.Rows[0]["Cust_EmailId"].ToString());
                                //body = body.Replace("#Email", "irfan.shaikh@acaira.in");
                                body = body.Replace("#TelNumber", dt.Rows[0]["Contact_Mobile"].ToString());
                                body = body.Replace("#datetime", dt.Rows[0]["Acc_CreatedDate"].ToString());
                                SendMAIL.SendMail("", "", "", body, "*NEW* Customer Registration on B2B");

                                SendSMS.Send(dt.Rows[0]["Contact_Mobile"].ToString(), "To " + dt.Rows[0]["Contact_ContactPerson"].ToString() + ", Thank You for Registering on www.swissbake.bh the one stop solution to your bakery ingredient requirements. We will inform you once your account is activated.");
                                SendSMS.Send("9702044786", "*NEW ACCOUNT* New Customer " + dt.Rows[0]["Acc_Name"].ToString() + " " + dt.Rows[0]["Acc_Code"].ToString() + " has been added to www.swissbake.bh");
                                //Session["U_Id"] = dt.Rows[0][0];
                                //Session["U_Name"] = dt.Rows[0][3];
                                //Session["U_Email"] = dt.Rows[0][1];
                                //Session["U_Verified"] = dt.Rows[0][5];




                                return Json("Verified", JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json("Unverified", JsonRequestBehavior.AllowGet);
                            }
                            //else
                            //{
                            //    dbaccount.VerifyMob(Convert.ToInt32(Session["U_Id"]));
                            //    ds = dbaccount.GetAccDetById(Convert.ToInt32(Session["U_Id"]), "getLoginDetailById");
                            //}
                        }
                        else
                        {
                            return Json("Unverified", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        SendOtp(Session["AppRegId"].ToString());
                        return Json("Timeout", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("Something went wrong", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }

        }





        [HttpPost]
        public JsonResult RegisterDetail(string Compname = null, string Fullname = null, string Email = null, string Mobile = null, string Password = null,
                                                  string mtype = null, string Bname = null, string Bemail = null, string Bmobile = null, string Bstreet = null,
                                                  string Bpin = null, string Bcity = null, string Bstate = null, string Sname = null, string Semail = null,
                                                  string Smobile = null, string Sstreet = null, string Spin = null, string Scity = null, string Sstate = null,
                                                  string step = null, string Aid = null, string Pan = null, string Gst = null, string Sgst = null)
        {
            try
            {
                if (step == "1" && Aid == null)
                {
                    if (Gst == null)
                    {
                        Gst = "";
                    }
                    if (Sgst == null)
                    {
                        Sgst = "";
                    }

                    int id = dbaccount.Insert(Compname, Fullname, Email, Password, "", "", "", "0", "");  // Compname was at fullname

                    int accadd = dbaccount.InsertAccAccount(Fullname, 0, Mobile, "", Email, id);

                    int Scontid = dbaccount.InsertAccAdd(id, "Shipping", Sname, Semail, Smobile, 17, 0, "", "", "", Sgst, "InsertAccAddress", 0);

                    int Bcontid = dbaccount.InsertAccAdd(id, "Billing", Bname, Bemail, Bmobile, 17, 0, "", "", "", Gst, "InsertAccAddress", 0);

                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();

                    ds = dbaccount.GetAccDetById(Convert.ToInt32(id), "getLoginDetailById");
                    dt = ds.Tables[0];

                    //if (Convert.ToBoolean(dt.Rows[0][5]) != false)
                    //{
                    //    Session["U_Id"] = dt.Rows[0][0];
                    //    Session["U_Name"] = dt.Rows[0][3];
                    //    Session["U_Email"] = dt.Rows[0][1];
                    //    Session["U_Verified"] = dt.Rows[0][5];
                    //}
                    Session["AppRegId"] = dt.Rows[0][0];
                    Session["AppRegMail"] = dt.Rows[0][1];
                    Session["AppRegName"] = Fullname; // dt.Rows[0][3];
                    Session["AppRegMobile"] = Mobile; // dt.Rows[0][3];
                    Session["AppRegCompany"] = Compname; // dt.Rows[0][3];

                   // SendMAIL.SendVerifyEmail();  //-- 2 times verify - irfan

                    //return Json(id);
                    return Json(new { data = id, success = true });
                }
                else if (step == "2")
                {
                    if (Session["AppRegId"] != null)
                    { //= dt.Rows[0][0];

                        int res = dbaccount.UpdateIndustryAcc(mtype, Convert.ToInt32(Aid));


                        string r = Checkregsteps(Convert.ToInt32(Aid));

                        if (r == "4")
                        {
                            //return Json(r);
                            return Json(new { data = 4, success = true });
                        }

                        //return Json(true);
                        return Json(new { success = true, isreturn = true });
                    }
                    else
                        return Json(new { success = false, message = "Session Expired!!"});

                }
                else if (step == "3")
                {

                    if (Session["AppRegId"] != null) { //= dt.Rows[0][0];

                    int Scontid = dbaccount.InsertAccAdd(Convert.ToInt32(Aid), "Shipping", Sname, Semail, Smobile, 17, !string.IsNullOrEmpty(Sstate) ? Convert.ToInt32(Sstate) : 0, Scity, Spin, Sstreet, Sgst, "UpdateAccAddress", 0);

                        int Bcontid = dbaccount.InsertAccAdd(Convert.ToInt32(Aid), "Billing", Bname, Bemail, Bmobile, 17, !string.IsNullOrEmpty(Bstate) ? Convert.ToInt32(Bstate) : 0, Bcity, Bpin, Bstreet, Gst, "UpdateAccAddress", 0);

                        if (Pan != "" || Gst != "")
                        {
                            int tax = dbaccount.UpdateTaxAcc(Pan, Gst, Convert.ToInt32(Aid));
                        }

                        SendMAIL.SendRegistrationEmail();

                        //Session["LogDet"] = dt;
                        return Json(new { data = -99, success = true });
                    }
                    else
                        return Json(new { success = false, message = "Session Expired !!" });
 
                    //return Json("Done");
                }
                else
                {
                    // return Json(false);
                    return Json(new { success = false });
                }

            }
            catch (Exception ex)
            {
                //return Json(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return Json(new { success = false, message = ex.InnerException != null ? ex.InnerException.Message : ex.Message });
            }

        }



        public JsonResult Checklogin(string email, string pass, string me, string id = null)
        {
            try
            {
                
                try
                {
                    Request.Cookies["uslg"].Expires = DateTime.Now.AddDays(-1);
                }
                catch (Exception ex)
                { }



                if (email != "" && pass != "")
                {
                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();

                    ds = dbaccount.GetLoginDet(email, pass);
                    dt = ds.Tables[0];
                    if (ds.Tables[0].Rows.Count == 1)
                    {

                        string r = Checkregsteps(Convert.ToInt32(dt.Rows[0][0]));

                        if (r != "Complete" && r != "")
                        {
                            List<Dictionary<string, object>> ro = new List<Dictionary<string, object>>();
                            Dictionary<string, object> rw;
                            rw = new Dictionary<string, object>();
                            if (r == "Verify")
                            {
                                rw.Add("Verify", "1");
                                rw.Add("Id", dt.Rows[0][0]);
                                ro.Add(rw);
                                System.Web.HttpContext.Current.Session["AppRegId"] = System.Web.HttpContext.Current.Session["RegId"];
                                System.Web.HttpContext.Current.Session.Remove("RegId");
                                Session["AppRegMail"] = dt.Rows[0][1];
                                Session["AppRegName"] = dt.Rows[0][3];
                                return Json(ro);

                            }
                            else if (r == "UnverifiedEmail")
                            {
                                rw.Add("UnverifiedEmail", "2");  // 2 means with popup , 1 means without popup
                                rw.Add("Id", dt.Rows[0][0]);
                                ro.Add(rw);
                                System.Web.HttpContext.Current.Session["AppRegId"] = System.Web.HttpContext.Current.Session["RegId"];
                                System.Web.HttpContext.Current.Session.Remove("RegId");
                                Session["AppRegMail"] = dt.Rows[0][1];
                                Session["AppRegName"] = dt.Rows[0][3];
                                return Json(ro);

                            }


                            rw.Add("Reg", "1");
                            rw.Add("Redirect", "/Register");
                            ro.Add(rw);


                            return Json(ro);
                        }

                        if (dt.Rows[0][5].ToString() == "True" && dt.Rows[0][6].ToString() == "True")
                        {
                            return Json("Block", JsonRequestBehavior.AllowGet);
                        }
                        if (dt.Rows[0][5].ToString() == "True" && dt.Rows[0][4].ToString() == "Inactive")
                        {
                            return Json("Inactive", JsonRequestBehavior.AllowGet);
                        }


                        System.Web.HttpContext.Current.Session["U_Id"] = dt.Rows[0][0];
                        System.Web.HttpContext.Current.Session["U_Name"] = dt.Rows[0][3];
                        System.Web.HttpContext.Current.Session["U_Email"] = dt.Rows[0][1];
                        System.Web.HttpContext.Current.Session["U_Verified"] = dt.Rows[0][5];

                        try
                        {
                            if (System.Web.HttpContext.Current.Session["membertype"].ToString() == "Guest")
                            {
                                dbaccount.updateTrail(System.Web.HttpContext.Current.Session["logid"].ToString(), System.Web.HttpContext.Current.Session["U_Id"].ToString());
                                Session["memberid"] = Session["U_Id"];
                                Session["membertype"] = "Customer";
                            }

                        }
                        catch (Exception ex)
                        { }



                        if (1==1) //me != "")
                        {
                            HttpCookie uslg = new HttpCookie("uslg");
                            uslg.Values.Add("usid", dt.Rows[0][0].ToString());
                            uslg.Values.Add("uspwd", Encryption.Encrypt(dt.Rows[0][2].ToString()));
                            //Response.Buffer = true;
                            uslg.Expires = DateTime.Now.AddDays(1);
                            //Response.Cookies.Add(uslg);
                            Response.Cookies.Add(uslg);
                            //Response.SetCookie(uslg);
                            //Response.Flush();
                            //Response.ClearHeaders();
                            //Response.Flush();
                        }


                        //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                        Dictionary<string, object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);

                            }
                            try
                            {
                                var rurl = System.Web.HttpContext.Current.Request.UrlReferrer;
                                //string ru = Session["Redirect_Url"].ToString();
                                bool blnRedirect = false;

                                try
                                {
                                    if (Session["Redirect_Url"].ToString() != "")
                                    {
                                        row.Add("Redirect", Session["Redirect_Url"].ToString());
                                        //Redirect(Session["Redirect_Url"].ToString());
                                        blnRedirect = true;
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                                
                                if ( !blnRedirect && rurl != null)
                                {
                                    row.Add("Redirect", rurl.ToString());
                                }
                            }
                            catch
                            {
                                //row.Add("Redirect", "");
                            }
                            rows.Add(row);
                        }

                        //var r = serializer.Serialize(rows);
                        //if (Session["Redirect_Url"].ToString() != "")
                        //{
                        //    Redirect(Session["Redirect_Url"].ToString());
                        //}
                        return Json(rows);
                    }
                    else
                    {
                        return Json(false);
                    }

                }
                else if (id != "" && pass != "")
                {
                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();

                    ds = dbaccount.GetLoginDetbyIdpass(Convert.ToInt32(id), Encryption.Decrypt(pass));
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows[0][5].ToString() == "True" && dt.Rows[0][4].ToString() == "Inactive")
                        {
                            return Json("Inactive", JsonRequestBehavior.AllowGet);
                        }
                        System.Web.HttpContext.Current.Session["U_Id"] = dt.Rows[0][0];
                        System.Web.HttpContext.Current.Session["U_Name"] = dt.Rows[0][3];
                        System.Web.HttpContext.Current.Session["U_Email"] = dt.Rows[0][1];
                        System.Web.HttpContext.Current.Session["U_Verified"] = dt.Rows[0][5];

                        try
                        {


                            if (System.Web.HttpContext.Current.Session["membertype"].ToString() == "Guest")
                            {
                                dbaccount.updateTrail(System.Web.HttpContext.Current.Session["logid"].ToString(), System.Web.HttpContext.Current.Session["U_Id"].ToString());
                                Session["memberid"] = Session["U_Id"];
                                Session["membertype"] = "Customer";
                            }
                        }
                        catch (Exception ex)
                        { }

                        //if (me != "")
                        //{
                        //    HttpCookie uslg = new HttpCookie("uslg");
                        //    uslg.Values.Add("usid", dt.Rows[0][0].ToString());
                        //    uslg.Values.Add("uspwd", Encryption.Encrypt(dt.Rows[0][2].ToString()));
                        //    uslg.Expires = DateTime.Now.AddDays(1);
                        //    Response.SetCookie(uslg);
                        //    Response.Flush();
                        //}


                        //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                        Dictionary<string, object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);

                            }
                            try
                            {
                                if (Session["Redirect_Url"].ToString() != "")
                                {
                                    row.Add("Redirect", Session["Redirect_Url"].ToString());
                                    //Redirect(Session["Redirect_Url"].ToString());
                                }
                            }
                            catch
                            {
                                //row.Add("Redirect", "");
                            }
                            rows.Add(row);
                        }

                        //var r = serializer.Serialize(rows);
                        //if (Session["Redirect_Url"].ToString() != "")
                        //{
                        //    Redirect(Session["Redirect_Url"].ToString());
                        //}
                        return Json(rows);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }


        public JsonResult SendVerifyEmail()
        {
        //    System.Web.Mvc.JsonResult objsn = new JsonResult();

            try
            {
                string FrontDomain = ConfigurationManager.AppSettings["frontdomain"];

                string link = FrontDomain + "/Misc/VerifyEmail/?link=" + Helper.Encrypt(Session["AppRegId"].ToString());
                string body = "";

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/Verifyemail.html"));
                body = read.ReadToEnd();

                if(Session["AppRegCompany"] != null)
                    body = body.Replace("#CName", Session["AppRegCompany"].ToString());
                else
                    body = body.Replace("#CName", Session["AppRegName"].ToString());
                
                //body = body.Replace("#Accname", form["Desig"]);
                body = body.Replace("#Verifylink", link);
                SendMAIL.DirectSendMail("",Session["AppRegMail"].ToString(), "", body, "Email Verification | SwissBake® B2B Portal");

                //HttpContext.Current.Session["AppRegId"] = null;
                // HttpContext.Current.Session["AppRegMail"] = null;
                // HttpContext.Current.Session["regId"] = null;

                return  Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
                //objsn.Data = "false";
                //objsn.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                //return objsn;
            }
        }


        public string Checkregsteps(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    string res = dbcheck.CheckRegStep(Convert.ToString(Id));
                    if (res != "Complete")
                    {
                        Session["regStep"] = res;
                        Session["regId"] = Id;
                    }
                    return res;

                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }



        public JsonResult Checkmail(string emailid)
        {
            try
            {
                int result = dbcheck.CheckExist(emailid, "EmailIdCheck");
                if (result == 1)
                {
                    return Json(false);
                }
                else
                {
                    return Json(true);
                }

            }
            catch
            {
                return Json(false);
            }

        }

        public JsonResult Checkmobile(string mobile)
        {
            try
            {
                int result = dbcheck.CheckExist(mobile, "MobileCheck");
                if (result == 1)
                {
                    return Json(false);
                }
                else
                {
                    return Json(true);
                }

            }
            catch
            {
                return Json(false);
            }

        }

        public JsonResult CheckPass(string oldpassword)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = dbaccount.GetAccDetById(Convert.ToInt32(Session["U_Id"]), "getLoginDetailById");
                dt = ds.Tables[0];

                string password = dt.Rows[0][2].ToString();

                if (oldpassword == password)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
            catch
            {
                return Json(false);
            }
        }



        public JsonResult GetOrderDash()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            //ds = dbaccount.FillGridById(0, 10, Convert.ToInt32(Session["U_Id"]), "OrderBYACCountID");
            ds = dbaccount.FillGridById(0, 10, Convert.ToInt32(Session["U_Id"]), "OrderBYACCountID");

            dt = ds.Tables[0];

            IList<CommonStruct> Order = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                Order.Add(new CommonStruct()
                {
                    Code = Convert.ToString(i["Order_Code"]),
                    Datetime = Convert.ToString(i["Order_CreatedDate"]),
                    Etcstring1 = Convert.ToString(i["Order_Amount"]),
                    Etcstring2 = Convert.ToString(i["paynow"]),
                    Etcstring3 = Convert.ToString(i["href"]) + HttpUtility.UrlEncode(Helper.Encrypt((i["Order_ID"]).ToString())),
                    Status = Convert.ToString(i["Order_OrderStatus"]),
                    Id = Convert.ToInt32(i["Order_ID"])
                });
            }

            ViewData["Order"] = Order;

            //dt.Clear();

            //return Newtonsoft.Json.JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(Order, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInvoiceDash()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            //ds = dbaccount.FillGridById(0, 10, Convert.ToInt32(Session["U_Id"]), "OrderBYACCountID");
            ds = dbaccount.FillGridById(0, 10, Convert.ToInt32(Session["U_Id"]), "InvoiceGridview");

            dt = ds.Tables[0];

            IList<CommonStruct> Invoice = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                Invoice.Add(new CommonStruct()
                {
                    Code = Convert.ToString(i["Order_Code"]),
                    Datetime = Convert.ToString(i["Invoice_Date"]),
                    Etcstring1 = Convert.ToString(i["Invoice_Amount"]),
                    Etcstring2 = Convert.ToString(i["Invoice_Number"]),
                    Etcstring3 = Convert.ToString(i["hrefordre"]) + HttpUtility.UrlEncode(Helper.Encrypt((i["Order_ID"]).ToString())),
                    Etcstring4 = Convert.ToString(i["hrefinvoice"]) + HttpUtility.UrlEncode(Helper.Encrypt((i["Invoice_ID"]).ToString())),
                    Status = Convert.ToString(i["Invoice_Status"]),
                    Id = Convert.ToInt32(i["Invoice_ID"]),
                    Id2 = Convert.ToInt32(i["Order_ID"]),
                    Freight = Convert.ToString(i["Order_Freight"]),
                    AWB = Convert.ToString(i["Invoice_AWB"]),
                    Tracking = Convert.ToString(i["hreftracking"])
                });
            }

            ViewData["Invoice"] = Invoice;

            //dt.Clear();

            //return Newtonsoft.Json.JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(Invoice, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPaymentDash()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            //ds = dbaccount.FillGridById(0, 10, Convert.ToInt32(Session["U_Id"]), "OrderBYACCountID");
            ds = dbaccount.FillGridById(0, 0, Convert.ToInt32(Session["U_Id"]), "FillPayment");

            dt = ds.Tables[0];

            IList<CommonStruct> Payment = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                Payment.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["Payment_Date"]),
                    Etcstring1 = Convert.ToString(i["Payment_Amount"]),
                    Etcstring2 = Convert.ToString(i["otherinfo"]),
                });
            }

            ViewData["Payment"] = Payment;

            //dt.Clear();

            //return Newtonsoft.Json.JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(Payment, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetComplaintHistDash()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            //ds = dbaccount.FillGridById(0, 10, Convert.ToInt32(Session["U_Id"]), "OrderBYACCountID");
            ds = dbaccount.FillGridById(0, 0, Convert.ToInt32(Session["U_Id"]), "MainTicketFill");

            dt = ds.Tables[0];

            IList<CommonStruct> Complaint = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                Complaint.Add(new CommonStruct()
                {
                    Id = Convert.ToInt32(i["Complaint_ID"]),
                    Datetime = Convert.ToString(i["modifeddate"]),
                    Status = Convert.ToString(i["Complaint_CompStatus"]),
                    Etcstring1 = Convert.ToString(i["Complaint_Subject"])
                });
            }

            ViewData["Payment"] = Complaint;

            //dt.Clear();

            //return Newtonsoft.Json.JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(Complaint, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Myorder(string date, string M)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = dbaccount.FillOrderById(Convert.ToInt32(Session["U_Id"]), date, M);

            dt = ds.Tables[0];

            IList<CommonStruct> Order = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                Order.Add(new CommonStruct()
                {
                    Id = Convert.ToInt32(i["Order_ID"]),
                    Url = Convert.ToString(i["href"]),
                    Freight = Convert.ToString(i["Order_Freight"]),
                    Status = Convert.ToString(i["Order_OrderStatus"]),
                    Code = Convert.ToString(i["Order_Code"]),
                    Datetime = Convert.ToString(i["Order_CreatedDate"]),
                    Etcstring1 = Convert.ToString(i["Order_Amount"]),
                    Url2 = Convert.ToString(i["href"] + HttpUtility.UrlEncode(Helper.Encrypt(i["Order_ID"].ToString())) + "&fr=" + Helper.Encrypt(i["Order_Freight"].ToString()) + "&Status=" + i["Order_OrderStatus"])
                });
            }
            ViewData["Order"] = Order;
            ViewBag.reorder = M;
            if (Request.UrlReferrer.Segments[2] == "myorder")
            {
                ViewBag.Act1 = "selected";
            }
            else if (Request.UrlReferrer.Segments[2] == "cancelorder")
            {
                ViewBag.Act2 = "selected";
            }
            else if (Request.UrlReferrer.Segments[2] == "pendingorder")
            {
                ViewBag.Act3 = "selected";
            }
            else if (Request.UrlReferrer.Segments[2] == "completedorder")
            {
                ViewBag.Act4 = "selected";
            }
            else if (Request.UrlReferrer.Segments[2] == "reorder")
            {
                ViewBag.Act5 = "selected";
            }

            return PartialView("_OrderGrid", ds);
        }

        public ActionResult Invoice(string date, string M)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = dbaccount.FillInvoiceById(Convert.ToInt32(Session["U_Id"]), date, M);

            //dt = ds.Tables[0];

            //IList<CommonStruct> Invoice = new List<CommonStruct>();

            //foreach (DataRow i in ds.Tables[0].Rows)
            //{
            //    Invoice.Add(new CommonStruct()
            //    {
            //        Id = Convert.ToInt32(i["Invoice_ID"]),
            //        Url = Convert.ToString(i["hrefinvoice"]),
            //        Etcstring2 = Convert.ToString(i["Invoice_Number"]),
            //        Etcstring3 = Convert.ToString(i["hreforder"]),
            //        Etcstring4 = Convert.ToString(i["Order_ID"]),
            //        Freight = Convert.ToString(i["Order_Freight"]),
            //        Status = Convert.ToString(i["Order_OrderStatus"]),
            //        Code = Convert.ToString(i["Order_Code"]),
            //        Datetime = Convert.ToString(i["Invoice_Date"]),
            //        Etcstring1 = Convert.ToString(i["Invoice_Amount"]),
            //        Status2 = Convert.ToString(i["Invoice_Status"]),
            //        AWB = Convert.ToString(i["Invoice_AWB"]),
            //        Tracking = Convert.ToString(i["hreftracking"]),

            //    });
            //}
            //ViewData["Invoice"] = Invoice;
            if (Request.UrlReferrer.Segments[2] == "invoice")
            {
                ViewBag.Act1 = "selected";
            }
            else if (Request.UrlReferrer.Segments[2] == "cancelinvoice")
            {
                ViewBag.Act2 = "selected";
            }
            else if (Request.UrlReferrer.Segments[2] == "pendinginvoice")
            {
                ViewBag.Act3 = "selected";
            }
            else if (Request.UrlReferrer.Segments[2] == "dispatchinvoice")
            {
                ViewBag.Act4 = "selected";
            }
            else if (Request.UrlReferrer.Segments[2] == "deliveredinvoice")
            {
                ViewBag.Act5 = "selected";
            }
            return PartialView("_InvoiceGrid", ds);
        }

        public ActionResult Track(string date, string M)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = dbaccount.FillGridById(0, 10, Convert.ToInt32(Session["U_Id"]), M);

            //dt = ds.Tables[0];

            //IList<CommonStruct> Invoice = new List<CommonStruct>();

            //foreach (DataRow i in ds.Tables[0].Rows)
            //{
            //    Invoice.Add(new CommonStruct()
            //    {
            //        Id = Convert.ToInt32(i["Invoice_ID"]),
            //        Url = Convert.ToString(i["hrefinvoice"]),
            //        Etcstring2 = Convert.ToString(i["Invoice_Number"]),
            //        Etcstring3 = Convert.ToString(i["hreforder"]),
            //        Etcstring4 = Convert.ToString(i["Order_ID"]),
            //        Freight = Convert.ToString(i["Order_Freight"]),
            //        Status = Convert.ToString(i["Order_OrderStatus"]),
            //        Code = Convert.ToString(i["Order_Code"]),
            //        Datetime = Convert.ToString(i["Invoice_Date"]),
            //        Etcstring1 = Convert.ToString(i["Invoice_Amount"]),
            //        Status2 = Convert.ToString(i["Invoice_Status"]),
            //        AWB = Convert.ToString(i["Invoice_AWB"]),
            //        Tracking = Convert.ToString(i["hreftracking"]),

            //    });
            //}
            //ViewData["Invoice"] = Invoice;
            return PartialView("_InvoiceGrid", ds);
        }

        public ActionResult AwbTrack(string inno, string awb, string email, string M)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = dbaccount.FillTrackByAWB(inno, awb, email, M);

            return PartialView("_InvoiceGrid", ds);
        }



        public JsonResult GetShipAddress(string adid)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (adid != "" && adid != null)
            {
                ds = dbaccount.AddressById(Convert.ToInt32(Session["U_Id"]), Convert.ToInt32(adid), "Shipping", "FillBillAddressById");
            }
            else
            {
                ds = dbaccount.AddressById(Convert.ToInt32(Session["U_Id"]), 0, "Shipping", "FillBillAddress");
            }

            dt = ds.Tables[0];

            IList<CommonStruct> Address = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                Address.Add(new CommonStruct()
                {
                    Id = Convert.ToInt32(i["AccAddr_ID"]),
                    CompName = i["AccAddr_Name"].ToString(),
                    Name = i["Contact_ContactPerson"].ToString(),
                    Address = i["AccAddr_Address"].ToString(),
                    Email = i["AccAddr_Email"].ToString(),
                    Telephone = i["AccAddr_Telephone"].ToString(),
                    Pin = i["AccAddr_Pincode"].ToString(),
                    City = i["AccAddr_City"].ToString(),
                    State = i["AccAddr_State"].ToString(),
                    Country = i["CountryName"].ToString(),
                    Etcstring1 = i["StateName"].ToString(),
                    Gst = i["AccAddr_GST"].ToString()
                });
            }

            //string js = JsonConvert.SerializeObject(Address);

            return Json(Address, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteShipAddr(string adid)
        {
            int Scontid = dbaccount.InsertAccAdd(Convert.ToInt32(Session["U_Id"]), "", "", "", "", 0, 0, "", "", "", "", "DeleteAccAddress", Convert.ToInt32(adid));

            return Json(true);

        }



        public JsonResult UpdateShipAdd(string Address, string City, string Mail, string Name, string Pin, string State, string Telephone, string id, string Aid, string mode, string Gst = "")
        {

            if (mode == "1")
            {
                int Scontid = dbaccount.InsertAccAdd(Convert.ToInt32(id), "Shipping", Name, Mail, Telephone, 17, Convert.ToInt32(State), City, Pin, Address, Gst, "InsertAccAddress", 0);
            }
            else if (mode == "2")
            {
                int Scontid = dbaccount.InsertAccAdd(Convert.ToInt32(id), "Shipping", Name, Mail, Telephone, 17, Convert.ToInt32(State), City, Pin, Address, Gst, "UpdateAccAddress", Convert.ToInt32(Aid));
            }

            return Json(true);
        }

        public JsonResult ForgotPass(string mail)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = dbaccount.ForgotPassword(mail);
                string body = "";

                if (ds.Tables[0].Rows[0]["Acc_LoginBlock"].ToString() == "True")
                {
                    return Json("Your login is blocked, kindly contact admin at<br> +973 77990501");
                }

                if (ds.Tables[0].Rows[0]["Acc_Status"].ToString() != "Active")
                {
                    return Json("Your account is not active, Please contact customer care at +973 77990501");
                }



                //if (ds.Tables[0].Rows[0]["Acc_IsVerified"].ToString() != "True")
                //{
                //    return Json("Your account is not verified. Kindly contact customer care.");
                //}

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/Forgot_password.html"));
                body = read.ReadToEnd();
                body = body.Replace("#NAME", ds.Tables[0].Rows[0]["Acc_Name"].ToString());
                body = body.Replace("#LOGIN", ds.Tables[0].Rows[0]["Cust_EmailId"].ToString());
                body = body.Replace("#PASSWORD", ds.Tables[0].Rows[0]["Cust_Password"].ToString());
                SendMAIL.SendMail("", mail, "", body, "Password for your account at www.swissbake.bh");
                return Json("true");
            }
            catch
            {
                return Json("false");
            }
        }


        public ActionResult PaynowOrder(string ID)
        {
            try
            {


                string status = dbPayt.checkorderPaid(Convert.ToInt32(ID));
                if (status == "Already Exist")
                {
                    return Json("Already Paid", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session["paymenttype"] = "order";
                    DataSet dt1 = dbOt.getOrderDetails("Getorderdetails", Convert.ToInt32(ID));
                    Session["OrderID&Amount"] = dt1.Tables[0].Rows[0]["Order_Code"] + "," + Convert.ToDouble((dt1.Tables[0].Rows[0]["payamount"])).ToString() + "," + Session["U_Id"].ToString() + "," + ID; // invoice id is order id added here by mandar for payment getway
                    return Json("/Paynow", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ReorderNow(FormCollection form)
        {
            //Dim dt1 As DataSet = MainBLL.ViewByID(OrderId, "ReOrderViewById")
            //   ID = form["reorder"];
            if (Session["U_Id"] == null)
            {
                return Redirect("/login");
                //  return Json("/login", JsonRequestBehavior.AllowGet);
            }

            else
            {
                if (form["reorder"] != "")
                {
                    DataTable dt = dbOt.ViewByID(Convert.ToInt32(form["reorder"]), "ReOrderViewById").Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        performorder(Convert.ToInt32(row["OrDetail_ProductId"]), Convert.ToInt32(row["OrDetail_Qty"]), row["Product_linkas"].ToString());
                    }
                }
                return Redirect("/Cart");
            }
        }

        public void performorder(int D, int Q, string Linkas)
        {

            try
            {
                // D is ProductID , Q is Qty and A is button name
                //  TempData.Keep("MOQ");
                //  TempData.Keep("linkas");

                DataSet ds = new DataSet();
                string flag = "false";
                if (Q <= 0) { Q = 1; }
                if (Q >= Convert.ToInt32(1)) { flag = "true"; }
                if (flag == "true")
                {
                    int orQty = 0;
                    int smQty = 0;
                    if (Linkas == "Sample")
                    {
                        Q = 1;
                        ds = dbPt.GetProductDetailbyid(D.ToString(), Session["U_Id"].ToString(), "checkSampleorder");
                        orQty = Convert.ToInt32(ds.Tables[0].Rows[0]["totalorder"]);
                        if (Session["OrderProduct"] == null)
                        {
                        }
                        else
                        {
                            DataTable dt1 = (DataTable)Session["OrderProduct"];
                            smQty = dt1.AsEnumerable().Where(x => x["Product_linkas"].ToString() == "Sample").ToList().Count;
                        }
                    }
                    else
                    {
                        smQty = 0;
                    }
                    if (orQty == 0 || orQty <= 2)
                    {

                        if (smQty == 0 || smQty <= 4)
                        {


                            placeorder(D, Q);
                            //var list = JsonConvert.SerializeObject((DataTable)Session["OrderProduct"], Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
                            //IList<CommonStruct> cart = new List<CommonStruct>();
                            //DataTable dt = (DataTable)Session["OrderProduct"];
                            //foreach (DataRow i in dt.Rows)
                            //{
                            //    cart.Add(new CommonStruct()
                            //    {

                            //        Id = Convert.ToInt32(i["Srno"]),
                            //        Product_Id = Convert.ToInt32(i["Product_Id"]),
                            //        Product_Name = Convert.ToString(i["Product_Name"]),
                            //        Product_Code = Convert.ToString(i["Product_Code"]),
                            //        Product_Rate = Convert.ToInt32(i["Product_Rate"]),
                            //        Qty = Convert.ToInt32(i["Quantity"]),
                            //        Packing_Weight = Convert.ToDouble(i["Packing_Weight"]),
                            //        TotalWeight = Convert.ToDouble(i["TotalWeight"]),
                            //        Amount = Convert.ToDouble(i["Amount"]),
                            //        vatinr = Convert.ToDouble(i["vatinr"]),
                            //        vatper = Convert.ToDouble(i["vatper"]),
                            //        unit = Convert.ToString(i["unit"]),
                            //        Product_MOQ = Convert.ToDouble(i["Product_MOQ"]),
                            //        MainImg_Path = Convert.ToString(i["MainImg_Path"]),
                            //        href = Convert.ToString(i["href"]),

                            //    });
                            //}

                            //string[] result = new string[dt.Columns.Count];
                            //DataRow dr = dt.Rows[0];
                            //for (int i = 0; i < dr.ItemArray.Length; i++)
                            //{
                            //    result[i] = dr[i].ToString();
                            //}
                            //return Json(cart, JsonRequestBehavior.AllowGet);
                        }
                        //else
                        //{
                        //    return Json("You have reached maximum sample limit for this order", JsonRequestBehavior.AllowGet);
                        //}
                    }
                    //else
                    //{
                    //    return Json("You have already bought this sample", JsonRequestBehavior.AllowGet);
                    //}

                }
                //else
                //{
                //    return Json("Minimum Order Quantity for this product is " + TempData["MOQ"] + " No. of pack", JsonRequestBehavior.AllowGet);
                //}


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult pendingpay(string M)
        {
            try
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
                if (Convert.ToInt32(ds.Tables[1].Rows[0]["Paynow"]) >= Convert.ToInt32(M))
                {
                    Session["paymenttype"] = "paynow";
                    Random rnd = new Random();
                    string dice = "OD" + rnd.Next(10000, 999999).ToString() + " ";
                    Session["OrderID&Amount"] = dice + "," + Convert.ToDouble(M.Replace(",", "")).ToString() + "," + Session["U_Id"].ToString() + "," + dice; // invoice id is order id added here by mandar for payment getwa
                    return Json("/Paynow", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Already Paid", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult Subscribe(string mail, string name)
        {
            try
            {
                DataSet ds = new DataSet();
                dbaccount.Subscribe(mail, name);

                string body = "";

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/NewsLetter.html"));
                body = read.ReadToEnd();
                body = body.Replace("#NAME_APPLICANT", name);
                body = body.Replace("#EMAIL_ID", mail);
                SendMAIL.SendMail("", mail, "", body, "Thank you ! for subscribing to our newsletter | SwissBake®");
                return Json("true");
            }
            catch
            {
                return Json("false");
            }
        }

        [HttpPost]
        public JsonResult ApplyCredit(string t, string m)
        {
            try
            {
                if (Session["U_Id"] != null)
                {
                    int res = dbCart.ApplyCredit("InsertAppCredit", Convert.ToInt32(Session["U_Id"]), m);


                    return Json("true");
                }
                else
                {
                    return Json("false");
                }
            }
            catch
            {
                return Json("false");
            }
        }

        public JsonResult SelectShipAddress(string adid)
        {
            Session["DeliveryaddID"] = Convert.ToInt32(adid);
            return FillShipAddress();
        }
        public JsonResult updatetaxation(string P, string G)
        {
            dbOt.UpdateTaxAcc(P, G, Convert.ToInt32(Session["U_Id"]), Convert.ToInt32(Session["DeliveryaddID"]));
            return Json(true);
        }

        public JsonResult checkTaxation(string adid)
        {
            if (Session["U_Id"] == null)
            {
                return Json("/login", JsonRequestBehavior.AllowGet);
            }

            else
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = dbaccount.AddressById(0, Convert.ToInt32(Session["DeliveryaddID"]), "Shipping", "Checkaddress");
                if (ds.Tables[0].Rows[0]["AccAddr_GST"].ToString() == "" || ds.Tables[0].Rows[0]["Cust_Pan"].ToString() == "")
                {
                    IList<CommonStruct> Address = new List<CommonStruct>();

                    foreach (DataRow i in ds.Tables[0].Rows)
                    {
                        Address.Add(new CommonStruct()
                        {
                            Gst = Convert.ToString(i["AccAddr_GST"]),
                            Etcstring1 = Convert.ToString(i["Cust_Pan"]),
                            Etcstring2 = Convert.ToString(i["Acc_dueDays"])
                        });
                    }
                    return Json(Address, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["Acc_dueDays"]) > 0)
                    {
                        IList<CommonStruct> Address = new List<CommonStruct>();

                        foreach (DataRow i in ds.Tables[0].Rows)
                        {
                            Address.Add(new CommonStruct()
                            {
                                Gst = Convert.ToString(i["AccAddr_GST"]),
                                Etcstring1 = Convert.ToString(i["Cust_Pan"]),
                                Etcstring2 = Convert.ToString(i["Acc_dueDays"])
                            });
                        }
                        return Json(Address, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }
        //public JsonResult FillShipAddress()
        //{
        //    try
        //    {
        //        if (Session["U_Id"] == null)
        //        {
        //            return Json("/login", JsonRequestBehavior.AllowGet);
        //        }

        //        else
        //        {
        //            DataSet ds = new DataSet();
        //            DataTable dt = new DataTable();

        //            if (Session["DeliveryaddID"] != null && Session["DeliveryaddID"].ToString() != "")
        //            {
        //                ds = dbaccount.AddressById(0, Convert.ToInt32(Session["DeliveryaddID"]), "Shipping", "GetcheckoutAddressID");
        //            }
        //            else
        //            {
        //                ds = dbaccount.AddressById(Convert.ToInt32(Session["U_Id"]), 0, "Shipping", "GetcheckoutAddress");
        //            }

        //            dt = ds.Tables[0];

        //            IList<CommonStruct> Address = new List<CommonStruct>();

        //            foreach (DataRow i in ds.Tables[0].Rows)
        //            {
        //                Address.Add(new CommonStruct()
        //                {
        //                    Id = Convert.ToInt32(i["AccAddr_ID"]),
        //                    CompName = i["AccAddr_Name"].ToString(),

        //                    Address = i["AccAddr_Address"].ToString(),
        //                    Email = i["AccAddr_Email"].ToString(),
        //                    Telephone = i["AccAddr_Telephone"].ToString(),
        //                    Pin = i["AccAddr_Pincode"].ToString(),
        //                    City = i["AccAddr_City"].ToString(),
        //                    State = i["StateName"].ToString(),
        //                    Country = i["CountryName"].ToString(),
        //                    Etcstring1 = i["StateName"].ToString(),
        //                    Gst = i["AccAddr_GST"].ToString()
        //                });
        //            }
        //            Session["DeliveryaddID"] = ds.Tables[0].Rows[0]["AccAddr_ID"];
        //            //string js = JsonConvert.SerializeObject(Address);

        //            return Json(Address, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public JsonResult FillShipAddress()
        {
            try
            {
                if (Session["U_Id"] == null)
                {
                    return Json("/login", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    if (Session["DeliveryaddID"] != null && Session["DeliveryaddID"].ToString() != "")
                    {
                        ds = dbaccount.AddressById(0, Convert.ToInt32(Session["DeliveryaddID"]), "Shipping", "GetcheckoutAddressID");
                    }
                    else
                    {
                        ds = dbaccount.AddressById(Convert.ToInt32(Session["U_Id"]), 0, "Shipping", "GetcheckoutAddress");

                        //  Set delivery address only once (default)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["DeliveryaddID"] = ds.Tables[0].Rows[0]["AccAddr_ID"];
                        }
                    }

                    IList<CommonStruct> Address = new List<CommonStruct>();
                    foreach (DataRow i in ds.Tables[0].Rows)
                    {
                        Address.Add(new CommonStruct()
                        {
                            Id = Convert.ToInt32(i["AccAddr_ID"]),
                            CompName = i["AccAddr_Name"].ToString(),
                            Address = i["AccAddr_Address"].ToString(),
                            Email = i["AccAddr_Email"].ToString(),
                            Telephone = i["AccAddr_Telephone"].ToString(),
                            Pin = i["AccAddr_Pincode"].ToString(),
                            City = i["AccAddr_City"].ToString(),
                            State = i["StateName"].ToString(),
                            Country = i["CountryName"].ToString(),
                            Etcstring1 = i["StateName"].ToString(),
                            Gst = i["AccAddr_GST"].ToString()
                        });
                    }

                    return Json(Address, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public JsonResult FillBillAddress()
        {
            try
            {
                if (Session["U_Id"] == null)
                {
                    return Json("/login", JsonRequestBehavior.AllowGet);
                }

                else
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    //if (Session["DeliveryaddID"] != null && Session["DeliveryaddID"].ToString() != "")
                    //{
                    //    ds = dbaccount.AddressById(0, Convert.ToInt32(Session["DeliveryaddID"]), "Shipping", "GetcheckoutAddressID");
                    //}
                    //else
                    //{
                    //    ds = dbaccount.AddressById(Convert.ToInt32(Session["U_Id"]), 0, "Shipping", "GetcheckoutAddress");
                    //}
                    ds = dbaccount.AddressById(Convert.ToInt32(Session["U_Id"]), 0, "Billing", "FillBillAddress");
                    dt = ds.Tables[0];

                    IList<CommonStruct> Address = new List<CommonStruct>();

                    foreach (DataRow i in ds.Tables[0].Rows)
                    {
                        Address.Add(new CommonStruct()
                        {
                            Id = Convert.ToInt32(i["AccAddr_ID"]),
                            CompName = i["AccAddr_Name"].ToString(),

                            Address = i["AccAddr_Address"].ToString(),
                            Email = i["AccAddr_Email"].ToString(),
                            Telephone = i["AccAddr_Telephone"].ToString(),
                            Pin = i["AccAddr_Pincode"].ToString(),
                            City = i["AccAddr_City"].ToString(),
                            State = i["StateName"].ToString(),
                            Country = i["CountryName"].ToString(),
                            Etcstring1 = i["StateName"].ToString(),
                            Gst = i["AccAddr_GST"].ToString()
                        });
                    }
                    //Session["DeliveryaddID"] = ds.Tables[0].Rows[0]["AccAddr_ID"];
                    //string js = JsonConvert.SerializeObject(Address);

                    return Json(Address, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public JsonResult Subs()
        {
            try
            {
                //int i = 0;
                //string s = "";
                //s = Request["Edit"].ToString();
                //i = Convert.ToInt32(Request["PId"]);
                //i = Convert.ToInt32(Session["U_Id"]);
                //s = Request["Qty"].ToString() + Request["Type"].ToString() + Request["Date"].ToString();
                //string name = "", email = "", contact = "", message = "", organisation = "", address = "", city = "", pin = "";

                string body = "";
                string strUName = "";
                string strEmail = "";

                if (Session["U_Id"] != null)
                {
                    if (Request["Edit"] != "")
                    {
                        int res = dbaccount.ModSubs(Convert.ToInt32(Request["Edit"]), Convert.ToInt32(Request["PId"]), Convert.ToInt32(Session["U_Id"]), Request["Qty"].ToString(), Request["Type"].ToString(), Request["Date"].ToString(), Convert.ToInt32(Session["U_Id"]), "UpdatePrdSubs");
                    }
                    else
                    {
                        int res = dbaccount.ModSubs(0, Convert.ToInt32(Request["PId"]), Convert.ToInt32(Session["U_Id"]), Request["Qty"].ToString(), Request["Type"].ToString(), Request["Date"].ToString(), Convert.ToInt32(Session["U_Id"]), "InsertPrdSubs");
                    }
                    strUName = Session["U_Name"].ToString();
                    strEmail = Session["U_Email"].ToString();
                }
                else
                {
                    long pid = Convert.ToInt64(Request["pid"]);
                    string qty = Request["Qty"];
                    string type = Request["Type"];
                    string date = Request["Date"];

                    string name = Request["Name-sch"];
                    string email = Request["Email-sch"];
                    string contact = Request["Mobile-sch"];
                    string message = Request["Comment-sch"];
                    string organisation = Request["Organisation-sch"];
                    string address = Request["Address-sch"];
                    string city = Request["City-sch"];
                    string pin = Request["Pin-sch"];

                    //int res = dbaccount.ModSubs(
                    //    0, pid, 0, qty, type, date, 0, "InsertPrdSubs",
                    //    name, email, contact, message, organisation, pin, address, city
                    //);
                }

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/RemindMe.html"));
                body = read.ReadToEnd();
                body = body.Replace("#Name", strUName);
                body = body.Replace("#Website_Link", "https://swissbake.bh");
                body = body.Replace("#Product_Name", Request["ProName"].ToString());
                body = body.Replace("#Qty", Request["Qty"].ToString());
                body = body.Replace("#Reminder_Frequency", Request["Type"].ToString());
                if (Request["Type"].ToString() == "Custom")
                {
                    //if (Convert.ToInt32(Request["Date"]) > 1)
                    //{
                    //    body = body.Replace("#Reminder_date", "After every " + Request["Date"].ToString() + " Days");
                    //}
                    //else
                    //{
                    //    body = body.Replace("#Reminder_date", "After every " + Request["Date"].ToString() + " Day");
                    //}
                    body = body.Replace("#Reminder_date", "Yearly ");

                }
                else
                {
                    body = body.Replace("#Reminder_date", "Day " + Request["Date"].ToString());
                }

                try
                {
                    SendMAIL.DirectSendMail(strUName, strEmail, "", body, "You have set an order Quote / Notification reminder for " + Request["ProName"].ToString() + " on SwissBake® B2B Portal");
                }
                catch
                {

                }

                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //public JsonResult Subs()
        //{
        //    try
        //    {
        //        string body = "";
        //        string strUName = "", strEmail = "";

        //        if (Session["U_Id"] != null)
        //        {
        //            int uid = Convert.ToInt32(Session["U_Id"]);
        //            if (Request["Edit"] != "")
        //            {

        // dbaccount.ModSubs(
        //                    Convert.ToInt32(Request["Edit"]),
        //                    Convert.ToInt32(Request["PId"]),
        //                    uid,
        //                    Request["Qty"],
        //                    Request["Type"],
        //                    Request["Date"],
        //                    uid,
        //                    "UpdatePrdSubs",
        //                    Request["Name-sch"],
        //                    Request["Email-sch"],
        //                    Request["Mobile-sch"],
        //                    Request["Comment-sch"],
        //                    Request["Organisation-sch"],
        //                    Request["Pin-sch"],
        //                    Request["Address-sch"],
        //                    Request["City-sch"]
        //                );
        //            }
        //            else
        //            {
        //                dbaccount.ModSubs(
        //                    0,
        //                    Convert.ToInt32(Request["PId"]),
        //                    uid,
        //                    Request["Qty"],
        //                    Request["Type"],
        //                    Request["Date"],
        //                    uid,
        //                    "InsertPrdSubs",
        //                    Request["Name-sch"],
        //                    Request["Email-sch"],
        //                    Request["Mobile-sch"],
        //                    Request["Comment-sch"],
        //                    Request["Organisation-sch"],
        //                    Request["Pin-sch"],
        //                    Request["Address-sch"],
        //                    Request["City-sch"]
        //                );
        //            }

        //            strUName = Session["U_Name"].ToString();
        //            strEmail = Session["U_Email"].ToString();
        //        }
        //        else
        //        {
        //            dbaccount.ModSubs(
        //                0,
        //                Convert.ToInt32(Request["PId"]),
        //                0,
        //                Request["Qty"],
        //                Request["Type"],
        //                Request["Date"],
        //                0,
        //                "InsertPrdSubs",
        //                Request["Name-sch"],
        //                Request["Email-sch"],
        //                Request["Mobile-sch"],
        //                Request["Comment-sch"],
        //                Request["Organisation-sch"],
        //                Request["Pin-sch"],
        //                Request["Address-sch"],
        //                Request["City-sch"]
        //            );

        //            strUName = Request["Name-sch"];
        //            strEmail = Request["Email-sch"];
        //        }

        //        // Read and prepare email template
        //        StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/RemindMe.html"));
        //        body = read.ReadToEnd();
        //        body = body.Replace("#Name", strUName);
        //        body = body.Replace("#Website_Link", "https://swissbake.bh");
        //        body = body.Replace("#Product_Name", Request["ProName"]);
        //        body = body.Replace("#Qty", Request["Qty"]);
        //        body = body.Replace("#Reminder_Frequency", Request["Type"]);
        //        body = body.Replace("#Reminder_date", Request["Type"] == "Custom" ? "Yearly" : "Day " + Request["Date"]);

        //        try
        //        {
        //            SendMAIL.DirectSendMail(strUName, strEmail, "", body,
        //                "You have set an order Quote / Notification reminder for " + Request["ProName"] + " on SwissBake® B2B Portal");
        //        }
        //        catch { }

        //        return Json(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex);
        //    }
        //}


        public JsonResult GetAllNotifi(string T)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            if (T != "" && T != null)
            {
                ds = dbaccount.GetNotifi(Convert.ToInt32(Session["U_Id"]), T, "NotificationByAccId");
            }
            else
            {
                ds = dbaccount.GetNotifi(Convert.ToInt32(Session["U_Id"]), "", "AllNotificationByAccId");
            }




            dt = ds.Tables[0];

            IList<CommonStruct> Notifi = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                string ur = "";
                if (i["Notification_Link"].ToString() != "")
                {
                    ur = i["Notification_Link"].ToString();
                }
                Notifi.Add(new CommonStruct()
                {
                    Id = Convert.ToInt32(i["Notification_Id"]),
                    Name = i["Notification_Title"].ToString(),
                    Url = ur,
                    Status = i["Notification_Read"].ToString(),
                    Datetime = i["Notification_CreatedDate"].ToString(),
                    Product_Desc = i["Notification_Desc"].ToString()
                });
            }

            //string js = JsonConvert.SerializeObject(Address);
            return Json(Notifi, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateNotifi(string id)
        {

            int res = dbaccount.UpdateNotifi(Convert.ToInt32(id), Convert.ToInt32(Session["U_Id"]), "UpdateReadNotification");


            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DelSubs()
        {
            try
            {
                int res = dbaccount.ModSubs(Convert.ToInt32(Request["ID"]), 0, 0, "", "", "", Convert.ToInt32(Session["U_Id"]), "DeletePrdSubs");


                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult SaveRecipe()
        {
            try
            {
                int res = dbrecipe.SaveRecipe(Convert.ToInt32(Session["U_Id"]), Convert.ToInt32(Request["Id"]), "InsertAccRecipe");


                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult DelRecipe()
        {
            try
            {
                int res = dbrecipe.SaveRecipe(Convert.ToInt32(Session["U_Id"]), Convert.ToInt32(Request["Id"]), "DeleteAccRecipeById");


                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult GetRecipe()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds = dbrecipe.getRecipe(Convert.ToInt32(Session["U_Id"]));




            dt = ds.Tables[0];

            IList<CommonStruct> Recipe = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                string ur = "";
                if (i["Recipe_Seourl"].ToString() != "")
                {
                    ur = i["Recipe_Seourl"].ToString();
                }
                Recipe.Add(new CommonStruct()
                {
                    Id = Convert.ToInt32(i["AccRec_Id"]),
                    Name = i["Recipe_Name"].ToString(),
                    Url = ur
                });
            }

            //string js = JsonConvert.SerializeObject(Address);

            return Json(Recipe, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetUserDetById()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds = dbaccount.GetAccDetById(Convert.ToInt32(Session["U_Id"]), "getContactDetailById");

            dt = ds.Tables[0];

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);

                }


                rows.Add(row);
            }

            //string js = JsonConvert.SerializeObject(Address);

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductHeader(string u)
        {
            DataSet ds = new DataSet();
            ds = dbct.GetList();
            DataRow[] results = ds.Tables[0].Select("PrdCategory_Seourl ='" + u + "'");
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            row = new Dictionary<string, object>();
            ViewBag.mode = "getproduct";
            if (results.Length > 0)
            {
                //ViewBag.Title = results[0][3];
                //ViewBag.Catname = results[0][0];
                //ViewBag.Oneline = results[0][7];
                //ViewBag.Catdesc = results[0][8];
                row.Add("Title", results[0][3]);
                row.Add("Catname", results[0][0]);
                row.Add("Oneline", results[0][7]);
                row.Add("Catdesc", results[0][8]);
                rows.Add(row);
            }



            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Product(string u, string u_id, string mode = "")
        {
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            if (mode == "search")
            {
                u = u.TrimEnd('-').TrimStart('-').Replace("%20", " ").Replace("-", " ");
                //string q = "search like '%" + u.Replace(" ", "") + "%'";
                string[] str = u.Split(' ');

                //string val = "";

                //if (str.Count() > 1)
                //{
                //    int i = 0;
                //    val = " where ";
                //    foreach (object obj in str)
                //    {
                //        i++;

                //        //int _min = 2;
                //        //int _max = 5;
                //        //Random _rdm = new Random();
                //        //int num = _rdm.Next(_min, _max);

                //        //string s1 = obj.ToString();

                //        //var strin = obj.ToString().ToCharArray();
                //        //string chr = strin[num].ToString();
                //        //var position = s1.LastIndexOf(chr);

                //        //string reg = ".{" + position + "}";
                //        //string reg2 = "$" + chr + "%";

                //        //Regex.Replace(s1, reg, reg2);




                //        if (i == str.Count())
                //        {
                //            val = val + "search like '%" + obj + "%'";
                //        }
                //        else
                //        {
                //            val = val + "search like '%" + obj + "%' or ";
                //        }
                //    }
                //}
                ViewBag.seg1 = "search";
                ViewBag.seg2 = u;
                ViewBag.seg3 = u.Replace(" ", ", ");
                ds = dbPt.SearchList(u, str.Count().ToString(), u_id, "searchproduct", u);//dbaccount.FillOrderById(4, date, M);

            }
            else if (mode == "homepro")
            {

                ds = dbPt.SearchList(u, "", u_id, "getHomeproduct", "");


                if (ds.Tables[0].Rows.Count == 0)
                {
                    return Content("false");
                }



            }
            else if (mode == "exclusive")
            {
                ds = dbPt.SearchList(u, "", u_id, "getexclusiveproduct", "");
            }
            else if (mode == "getSolution")
            {
                ds = dbPt.SearchList(u, "", u_id, "getSolution", "");
            }
            else
            {
                ds = dbPt.GetList(u.Replace("/", ""), u_id);//dbaccount.FillOrderById(4, date, M);
            }



            //  dt = ds.Tables[0];
            DataRow[] results = ds.Tables[0].Select("PrdCategory_Seourl ='" + u.Replace("/", "") + "'");
            if (results.Length > 0)
            {
                ViewBag.Title = results[0][0];
                ViewBag.Catname = results[0][1];
            }
            IList<CommonStruct> product = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                product.Add(new CommonStruct()
                {


                    Product_MainImage = Convert.ToString(i["Product_MainImage"]),
                    //   Product_Id = Convert.ToInt32(i["Product_Id"]),
                    Product_Rate = Convert.ToDouble(i["Product_Rate"]),
                    Product_Vat = Convert.ToDouble(i["Product_Vat"]),
                    //  Product_Dosage = Convert.ToString(i["Product_Dosage"]),
                    Product_Name = Convert.ToString(i["Product_Name"]),
                    Product_Code = Convert.ToString(i["Product_Code"]),
                    Product_Usage = Convert.ToString(i["Product_Usage"]),
                    Product_Stock = Convert.ToString(i["Product_Stock"]),
                    //  Product_Recipes = Convert.ToString(i["Product_Recipes"]),
                    PrdCategory_Seourl = Convert.ToString(i["PrdCategory_Seourl"]),
                    Product_Seotitle = Convert.ToString(i["Product_Seotitle"]),
                    Product_Seourl = Convert.ToString(i["Product_Seourl"]),
                    Product_New = Convert.ToString(i["Product_New"]),
                    Product_Instock = Convert.ToString(i["Product_Instock"]),
                    Product_Onorder = Convert.ToString(i["Product_Onorder"]),
                    minprice = Convert.ToString(i["Product_Rate"]),
                    //Convert.ToString(i["minprice"]),
                    maxprice = Convert.ToString(i["maxprice"]),
                    Discount_Discount = Convert.ToString(i["Discount_Discount"]),
                    discounted_Rate = Convert.ToString(i["discounted_Rate"]),
                    Distdiscounted_Rate = Convert.ToString(i["Distdiscounted_Rate"]),
                    DistDiscount_Discount = Convert.ToString(i["DistDiscount_Discount"]),
                    Discount_MinQty = Convert.ToString(i["Discount_MinQty"]),
                    Product_OneLineDesc = Convert.ToString(i["Product_OneLineDesc"]),
                    Packing_Packing = Convert.ToString(i["Packing_Packing"]),
                    Etcint1 = Convert.ToInt32(i["Product_AccId"])

                });
            }
            ViewData["productgrid"] = product;

            ViewBag.procount = product.Count;




            return PartialView("_ProductGrid", ds);
        }
        [HttpPost]
        public ActionResult RequestB(string t, string n, string m, string e, string o, string a, string c, string p, string cmm, string rd, int pi, string co, string pn)
        {
            string message;
            DataSet ds = new DataSet();

            ds = dbaccount.AddressById(Convert.ToInt32(Session["U_Id"]), 0, "Shipping", "getaccdetails");

            if (t == "Whatsapp")
            {
                o = ds.Tables[0].Rows[0]["Acc_Name"].ToString();
                a = ds.Tables[0].Rows[0]["AccAddr_Address"].ToString();
                c = ds.Tables[0].Rows[0]["AccAddr_City"].ToString();
                p = ds.Tables[0].Rows[0]["AccAddr_Pincode"].ToString();
            }

            if (t == "Whatsapp" && pn == "Cart")
            {
                if (Session["U_Id"] != null)
                {
                    IList<CommonStruct> cart = new List<CommonStruct>();
                    DataTable dt = (DataTable)Session["OrderProduct"];
                    foreach (DataRow i in dt.Rows)
                    {
                        cmm = cmm + " " + Convert.ToString(i["Product_Name"]) + " | " + Convert.ToString(i["unit"]) + " | No. of Packs:" + Convert.ToString(i["Quantity"]) + ", ";

                    }
                    cmm = cmm.TrimEnd(' ').TrimEnd(',');
                    cmm = "Hi! I am interested in this item: " + cmm + ". Please get back to me.";


                    Session["OrderProduct"] = null;
                    Session.Remove("OrderProduct");

                    subject = "Whatsapp Order from Swissbake Bahrain";
                }
                else
                {
                    return Json("Redirect");
                }


            }
            else if (t == "Quote" && pn == "Cart")
            {
                if (Session["U_Id"] != null)
                {
                    cmm = cmm + " | Quote for following products: ";
                    IList<CommonStruct> cart = new List<CommonStruct>();
                    DataTable dt = (DataTable)Session["OrderProduct"];
                    foreach (DataRow i in dt.Rows)
                    {
                        cmm = cmm + " " + Convert.ToString(i["Product_Name"]) + " | " + Convert.ToString(i["unit"]) + " | No. of Packs:" + Convert.ToString(i["Quantity"]) + ", ";

                    }
                    cmm = cmm.TrimEnd(' ').TrimEnd(',');

                    string pbody = "<h3>PRODUCT NAME : " + pn + "</h3><strong>CUSTOMER DETAIL:</strong><br><br><table><tr><td>NAME </td><td>:&nbsp;" + n + "</td></tr><tr><td>EMAIL  </td><td>:&nbsp;" + e + "</td></tr><tr><td>ORGANISATION </td><td>:&nbsp;" + o + "</td></tr><tr><td>TELEPHONE NUMBER </td><td>:&nbsp;" + m + "</td></tr><tr><td>ADDRESS </td><td>:&nbsp;" + a + "</td></tr><tr><td>CITY </td><td>:&nbsp;" + c + "</td></tr><tr><td>PIN CODE </td><td>:&nbsp;" + p + "</td></tr></table><br>";
                    //< tr >< td > MESSAGE </ td >< td >:&nbsp; " + cmm + " </ td ></ tr >

                    cmm = cmm + "<br><br>" + pbody;

                              subject = "Enquiry for Product from Swissbake Bahrain";
                }
                else
                {
                    return Json("Redirect");
                }
            }
            else
            {
                cmm = cmm + " | " + pn;
                cmm = cmm.TrimEnd(' ').TrimEnd(',');
                if (t == "Whatsapp")
                {
                    if (Session["U_Id"] != null)
                    {
                        cmm = "Hi! I am interested in this item: " + cmm + ". Please get back to me.";
                        subject = "Whatsapp Order from Swissbake Bahrain";
                    }
                    else
                    {
                        return Json("Redirect");
                    }
                }
                else if (t == "Sample")
                {
                    //if (Session["U_Id"] != null)
                    //{

                    //}
                    //else
                    //{
                    //    return Json("Redirect");
                    //}
                }
                else
                {
                    subject = "Enquiry for Product from Swissbake Bahrain";
                }
            }

            cmm = cmm.TrimEnd(' ').TrimEnd(',');


            int res = dbhome.InsertRequest(t, n, m, e, o, a, c, p, cmm.TrimEnd(','), pi, "InsertEnquiryWidLead");

            Session["error"] = "sucess()";
            string body = "";

            StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/Contact.html"));
            body = read.ReadToEnd();
            body = body.Replace("#NAME_CONTACT", n);

            SendMAIL.SendMail(n, e, m, body, "Thank You for contacting us | Swiss Bake Ingredients Pvt. Ltd.");
            string email = "";
            if (Session["U_Id"] != null)
            {
                email = dbaccount.Email(Convert.ToInt32(Session["U_Id"]));
                email = Session["U_Email"] + "," + email;
            }
            if (t == "Sample")
            {
                //string pbody = "<h3>PRODUCT NAME : " + pn + "</h3><strong>CUSTOMER DETAIL:</strong><br><br><table><tr><td>NAME </td><td>:&nbsp;" + n + "</td></tr><tr><td>EMAIL  </td><td>:&nbsp;" + e + "</td></tr><tr><td>ORGANISATION </td><td>:&nbsp;" + o + "</td></tr><tr><td>TELEPHONE NUMBER </td><td>:&nbsp;" + m + "</td></tr><tr><td>ADDRESS </td><td>:&nbsp;" + a + "</td></tr><tr><td>CITY </td><td>:&nbsp;" + c + "</td></tr><tr><td>PIN CODE </td><td>:&nbsp;" + p + "</td></tr><tr><td>MESSAGE </td><td>:&nbsp;" + cmm + "</td></tr></table><br>";
                string pbody = "<h3>PRODUCT NAME : " + pn + "</h3><strong>CUSTOMER DETAIL:</strong><br><br><table><tr><td>NAME </td><td>:&nbsp;" + n + "</td></tr><tr><td>EMAIL  </td><td>:&nbsp;" + e + "</td></tr><tr><td>TELEPHONE NUMBER </td><td>:&nbsp;" + m + "</td></tr><tr><td>ADDRESS </td><td>:&nbsp;" + a + "</td></tr><tr><td>CITY </td><td>:&nbsp;" + c + "</td></tr><tr><td>PIN CODE </td><td>:&nbsp;" + p + "</td></tr><tr><td>MESSAGE </td><td>:&nbsp;" + cmm + "</td></tr></table><br>";

                read = new StreamReader(Server.MapPath("~/Content/Mail/RequestSample.html"));
                body = read.ReadToEnd();
                body = body.Replace("#REQUEST_SAMPLE", pbody);


                SendMAIL.SendMail(n, email, m, body, "Sample Request for Product " + co.Replace("CODE - ", "") + " from Swissbake Bahrain");
            }
            else if (t == "Whatsapp")
            {
                body = "<strong>CUSTOMER DETAIL:</strong><br><br><table><tr><td>NAME </td><td>:&nbsp;" + n + "</td></tr><tr><td>EMAIL  </td><td>:&nbsp;" + e + "</td></tr><tr><td>ORGANISATION </td><td>:&nbsp;" + ds.Tables[0].Rows[0]["Acc_Name"] + "</td></tr><tr><td>TELEPHONE NUMBER </td><td>:&nbsp;" + m + "</td></tr><tr><td>ADDRESS </td><td>:&nbsp;" + ds.Tables[0].Rows[0]["AccAddr_Address"] + "</td></tr><tr><td>CITY </td><td>:&nbsp;" + ds.Tables[0].Rows[0]["AccAddr_City"] + "</td></tr><tr><td>PIN CODE </td><td>:&nbsp;" + ds.Tables[0].Rows[0]["AccAddr_Pincode"] + "</td></tr><tr><td>MESSAGE </td><td>:&nbsp;" + cmm + "</td></tr></table><br>";

                SendMAIL.SendMail(n, email, m, body, subject);
            }
            else
            {
                body = "<strong>CUSTOMER DETAIL:</strong><br><br><table><tr><td>NAME </td><td>:&nbsp;" + n + "</td></tr><tr><td>EMAIL  </td><td>:&nbsp;" + e + "</td></tr><tr><td>ORGANISATION </td><td>:&nbsp;" + o + "</td></tr><tr><td>TELEPHONE NUMBER </td><td>:&nbsp;" + m + "</td></tr><tr><td>ADDRESS </td><td>:&nbsp;" + a + "</td></tr><tr><td>CITY </td><td>:&nbsp;" + c + "</td></tr><tr><td>PIN CODE </td><td>:&nbsp;" + p + "</td></tr><tr><td>MESSAGE </td><td>:&nbsp;" + cmm + "</td></tr></table><br>";

                SendMAIL.SendMail(n, email, m, body, subject);
            }

            if (t == "Whatsapp")
            {
                //message = cmm;
                return Json(cmm);
            }


            return Json("true");
        }
        public ActionResult Productdetail(string u, string u_id, string mode)
        {
            try
            {
                if (Session["U_Id"] == null)
                {
                    // return Redirect("/login");
                    return Json("/login", JsonRequestBehavior.AllowGet);
                }

                else
                {
                    TempData["InCart"] = "false";
                    DataSet ds = new DataSet();
                    if (Session["U_Id"] != null)
                    {
                        ds = dbPt.GetProductDetailbyid(u, Session["U_Id"].ToString(), mode);
                    }
                    else
                    {
                        ds = dbPt.GetProductDetailbyid(u, "0", mode);
                    }
                    TempData["MOQ"] = ds.Tables[0].Rows[0]["Product_MOQ"];
                    TempData["linkas"] = ds.Tables[0].Rows[0]["Product_linkas"];
                    if (Session["OrderProduct"] != null)
                    {
                        DataTable dt = (DataTable)Session["OrderProduct"];
                        // Dim id As inte
                        if (dt.Rows.Count > 0)
                        {
                            bool contains = dt.AsEnumerable().Any(row => Convert.ToInt32(u) == row.Field<int>("Product_Id"));
                            // Dim contains As Boolean = dt.AsEnumerable().Any(Function(row) lblcode.Text = row.Field(Of [String])("Code"))
                            if (contains)
                            {
                                TempData["InCart"] = "true";
                            }
                            else { TempData["InCart"] = "false"; }
                        }
                    }
                    //else
                    //{
                    //    // btnBuy.Visible = True
                    //    btnCart.Visible = true;
                    //    divqty.Visible = true;
                    //    divtotalpr.Visible = true;
                    //    btngotocart.Visible = false;
                    //}

                    return PartialView("_ProductDetails", ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult addcart(Int32 D, Int32 Q, string A)
        {
            try
            {
                if (Session["U_Id"] == null)
                {
                    //return Redirect("/login");
                    return Json("/login", JsonRequestBehavior.AllowGet);
                }

                else
                {
                    // D is ProductID , Q is Qty and A is button name
                    TempData.Keep("MOQ");
                    TempData.Keep("linkas");

                    DataSet ds = new DataSet();
                    string flag = "false";
                    if (Q <= 0) { Q = 1; }
                    if (Q >= Convert.ToInt32(TempData["MOQ"])) { flag = "true"; }
                    if (flag == "true")
                    {
                        int orQty = 0;
                        int smQty = 0;
                        if (TempData["linkas"].ToString() == "Sample")
                        {
                            Q = 1;
                            ds = dbPt.GetProductDetailbyid(D.ToString(), Session["U_Id"].ToString(), "checkSampleorder");
                            orQty = Convert.ToInt32(ds.Tables[0].Rows[0]["totalorder"]);
                            if (Session["OrderProduct"] == null)
                            {
                            }
                            else
                            {
                                DataTable dt1 = (DataTable)Session["OrderProduct"];

                                smQty = dt1.AsEnumerable().Where(x => x["Product_linkas"].ToString() == "Sample").ToList().Count;

                                var result = dt1.AsEnumerable()
                   .Where(r => r.Field<int>("Product_Id") == D)
                   .Sum(x => Convert.ToInt32(x["Quantity"]));
                                if (result >= 1)
                                {
                                    return Json("You have reached maximum sample limit for this product", JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            smQty = 0;
                        }
                        if (orQty == 0 || orQty <= 2)
                        {

                            if (smQty == 0 || smQty <= 4)
                            {


                                placeorder(D, Q);
                                //return PartialView();
                                //  return PartialView("_PartialCart", Session["OrderProduct"]);
                                // return Json((DataTable)Session["OrderProduct"]);
                                var list = JsonConvert.SerializeObject((DataTable)Session["OrderProduct"], Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });


                                IList<CommonStruct> cart = new List<CommonStruct>();
                                DataTable dt = (DataTable)Session["OrderProduct"];
                                foreach (DataRow i in dt.Rows)
                                {
                                    cart.Add(new CommonStruct()
                                    {

                                        Id = Convert.ToInt32(i["Srno"]),
                                        Product_Id = Convert.ToInt32(i["Product_Id"]),
                                        Product_Name = Convert.ToString(i["Product_Name"]),
                                        Product_Code = Convert.ToString(i["Product_Code"]),
                                        Product_Rate = Convert.ToDouble(i["Product_Rate"]),
                                        Qty = Convert.ToInt32(i["Quantity"]),
                                        Packing_Weight = Convert.ToDouble(i["Packing_Weight"]),
                                        TotalWeight = Convert.ToDouble(i["TotalWeight"]),
                                        Amount = Convert.ToDouble(i["Amount"]),
                                        vatinr = Convert.ToDouble(i["vatinr"]),
                                        vatper = Convert.ToDouble(i["vatper"]),
                                        unit = Convert.ToString(i["unit"]),
                                        Product_MOQ = Convert.ToDouble(i["Product_MOQ"]),
                                        MainImg_Path = Convert.ToString(i["MainImg_Path"]),
                                        href = Convert.ToString(i["href"]),

                                    });
                                }

                                string[] result = new string[dt.Columns.Count];
                                DataRow dr = dt.Rows[0];
                                for (int i = 0; i < dr.ItemArray.Length; i++)
                                {
                                    result[i] = dr[i].ToString();
                                }
                                return Json(cart, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json("You have reached maximum sample limit for this order", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("You have already bought this sample", JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        //return Content("<script language='javascript' type='text/javascript'>alert('Minimum Order Quantity for this product is '" + ViewBag.MOQ + "');</script>");
                        // return Json("Minimum Order Quantity for this product is " + TempData["MOQ"] + " KG", JsonRequestBehavior.AllowGet);
                        return Json("Minimum Order Quantity for this product is " + TempData["MOQ"] + " No. of pack", JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult RemoveItem(Int32 PID)
        {
            try
            {
                if (Session["U_Id"] == null)
                {
                    // return Redirect("/login");
                    return Json("/login", JsonRequestBehavior.AllowGet);
                }

                else
                {

                    DataTable dt = (DataTable)Session["OrderProduct"];
                    if (Session["OrderProduct"] == null)
                    {

                    }
                    else
                    {
                        count = dt.Rows.Count;
                        bool contains = dt.AsEnumerable().Any(row => PID == row.Field<int>("Product_Id"));
                        if (contains)
                        {
                            DataRow[] oldrow = dt.Select("Product_Id='" + PID + "'");
                            dt.Rows.Remove(oldrow[0]);
                            ///
                            string cookieID;
                            if ((((Request.Cookies["TheCart"]) != null)))
                            {
                                cartCookie = Request.Cookies["TheCart"];
                                cookieID = Request.Cookies["TheCart"].Value;
                                if (cookieID == "" | cookieID == "NULL")
                                    cookieID = "NULL";
                                else
                                {
                                    string pid = (PID * 7.3).ToString();
                                    cookieID = cookieID.Replace(pid + ",", "").Replace(pid, "");
                                    if (cookieID.Length > 0)
                                    {
                                        string str = cookieID.Substring(cookieID.Length - 1);
                                        if (str == ",")
                                            cookieID = cookieID.Remove(cookieID.Length - 1);
                                    }
                                }
                            }
                            else
                                cookieID = "NULL";
                            cartCookie.Value = cookieID;
                            Response.Cookies.Add(cartCookie);
                            if (Session["U_Id"] != null)
                                dbCart.Deletefrontcart("Delete", Session["U_Id"].ToString(), PID);
                            // System.Web.UI.Page.Response.Redirect("/cart");
                            // }
                            ////
                        }
                        else
                        {
                        }
                        if (dt.Rows.Count == 0)
                        {
                            Session.Remove("OrderProduct");
                        }
                        else
                        {
                            Session["OrderProduct"] = dt;
                        }
                    }
                    var list = JsonConvert.SerializeObject((DataTable)Session["OrderProduct"], Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });

                    double prodrate = 0;
                    IList<CommonStruct> cart = new List<CommonStruct>();
                    //  DataTable dt = (DataTable)Session["OrderProduct"];
                    foreach (DataRow i in dt.Rows)
                    {

                        try {
                            prodrate = Convert.ToInt32(i["Product_Rate"]);
                        }
                        catch (Exception ex)
                        {
                            prodrate = 0;
                        }

                        cart.Add(new CommonStruct()
                        {

                            Id = Convert.ToInt32(i["Srno"]),
                            Product_Id = Convert.ToInt32(i["Product_Id"]),
                            Product_Name = Convert.ToString(i["Product_Name"]),
                            Product_Code = Convert.ToString(i["Product_Code"]),
                            Product_Rate = prodrate, // Convert.ToInt32(i["Product_Rate"]),
                            Qty = Convert.ToInt32(i["Quantity"]),
                            Packing_Weight = Convert.ToDouble(i["Packing_Weight"]),
                            TotalWeight = Convert.ToDouble(i["TotalWeight"]),
                            Amount = Convert.ToDouble(i["Amount"]),
                            vatinr = Convert.ToDouble(i["vatinr"]),
                            vatper = Convert.ToDouble(i["vatper"]),
                            unit = Convert.ToString(i["unit"]),
                            Product_MOQ = Convert.ToDouble(i["Product_MOQ"]),
                            MainImg_Path = Convert.ToString(i["MainImg_Path"]),
                            href = Convert.ToString(i["href"]),

                        });
                    }


                    return Json(cart, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult changeqty(Int32 D, Int32 Q, Int32 M, string L)
        {
            try
            {
                if (Session["U_Id"] == null)
                {
                    //return Redirect("/login");
                    return Json("/login", JsonRequestBehavior.AllowGet);
                }

                else
                {

                    // Int32 D, Int32 Q,  Int32 M, string L
                    // D is ProductID , Q is Qty  ,A is button name, M is MOQ and L is linkas.

                    //Int32 D   =13;
                    //Int32 Q   =1;
                    //Int32 M  =0;
                    //string L ="Product";
                    TempData["MOQ"] = M;
                    TempData["linkas"] = L;
                    TempData.Keep("MOQ");
                    TempData.Keep("linkas");
                    if (L == "Sample")
                    {
                        Q = 1;
                    }

                    DataSet ds = new DataSet();
                    string flag = "false";
                    if (Q <= 0) { Q = 1; }
                    if (Q >= Convert.ToInt32(TempData["MOQ"])) { flag = "true"; }
                    if (flag == "true")
                    {
                        int orQty = 0;
                        int smQty = 0;
                        if (TempData["linkas"].ToString() == "Sample")
                        {
                            ds = dbPt.GetProductDetailbyid(D.ToString(), Session["U_Id"].ToString(), "checkSampleorder");
                            orQty = Convert.ToInt32(ds.Tables[0].Rows[0]["totalorder"]);
                            if (Session["OrderProduct"] == null)
                            {
                            }
                            else
                            {
                                DataTable dt1 = (DataTable)Session["OrderProduct"];
                                smQty = dt1.AsEnumerable().Where(x => x["Product_linkas"].ToString() == "Sample").ToList().Count;
                            }
                        }
                        else
                        {
                            smQty = 0;
                        }
                        if (orQty == 0 || orQty <= 2)
                        {

                            if (smQty == 0 || smQty <= 4)
                            {


                                placeorderQ(D, Q);
                                var list = JsonConvert.SerializeObject((DataTable)Session["OrderProduct"], Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });


                                IList<CommonStruct> cart = new List<CommonStruct>();
                                DataTable dt = (DataTable)Session["OrderProduct"];
                                foreach (DataRow i in dt.Rows)
                                {
                                    cart.Add(new CommonStruct()
                                    {

                                        Id = Convert.ToInt32(i["Srno"]),
                                        Product_Id = Convert.ToInt32(i["Product_Id"]),
                                        Product_Name = Convert.ToString(i["Product_Name"]),
                                        Product_Code = Convert.ToString(i["Product_Code"]),
                                        Product_Rate = Convert.ToDouble(i["Product_Rate"]),
                                        Qty = Convert.ToInt32(i["Quantity"]),
                                        Packing_Weight = Convert.ToDouble(i["Packing_Weight"]),
                                        TotalWeight = Convert.ToDouble(i["TotalWeight"]),
                                        Amount = Convert.ToDouble(i["Amount"]),
                                        vatinr = Convert.ToDouble(i["vatinr"]),
                                        vatper = Convert.ToDouble(i["vatper"]),
                                        unit = Convert.ToString(i["unit"]),
                                        Product_MOQ = Convert.ToDouble(i["Product_MOQ"]),
                                        MainImg_Path = Convert.ToString(i["MainImg_Path"]),
                                        href = Convert.ToString(i["href"]),

                                    });
                                }


                                return Json(cart, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json("You have reached maximum sample limit for this order", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("You have already bought this sample", JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        //return Content("<script language='javascript' type='text/javascript'>alert('Minimum Order Quantity for this product is '" + ViewBag.MOQ + "');</script>");
                        // return Json("Minimum Order Quantity for this product is " + TempData["MOQ"] + " KG", JsonRequestBehavior.AllowGet);
                        return Json("Minimum Order Quantity for this product is " + TempData["MOQ"] + " No. of pack", JsonRequestBehavior.AllowGet);
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public ActionResult getstockDisplay(string u, string u_id, string mode)
        //{
        //    try
        //    {
        //        int warehouse;
        //        string ordered = txtquantity.Text
        //        return Json(freight, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public ActionResult getfreight()
        {
            try
            {
                DataSet dt = dbaccount.GetAccDetById(Convert.ToInt32(Session["U_Id"]), "GetFreight");
                IList<CommonStruct> freight = new List<CommonStruct>();
                foreach (DataRow i in dt.Tables[0].Rows)
                {
                    freight.Add(new CommonStruct()
                    {
                        Type = Convert.ToString(i["Acc_ShippingType"]),
                        Amount = Convert.ToDouble(i["Acc_ShippingAmt"])
                    });
                }
                return Json(freight, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult fillsidecart()
        {
            try
            {

                DataTable dt = (DataTable)Session["OrderProduct"];
                if (Session["OrderProduct"] != null)
                {
                    IList<CommonStruct> cart = new List<CommonStruct>();

                    foreach (DataRow i in dt.Rows)
                    {
                        DataSet dtstock = dbCart.b2bGetbothstock(Convert.ToInt32(i["Product_Id"]));

                        cart.Add(new CommonStruct()
                        {

                            Id = Convert.ToInt32(i["Srno"]),
                            Product_Id = Convert.ToInt32(i["Product_Id"]),
                            Product_Name = Convert.ToString(i["Product_Name"]),
                            Product_Code = Convert.ToString(i["Product_Code"]),
                            Product_Rate = Convert.ToDouble(i["Product_Rate"]),
                            Qty = Convert.ToInt32(i["Quantity"]),
                            Packing_Weight = Convert.ToDouble(i["Packing_Weight"]),
                            TotalWeight = Convert.ToDouble(i["TotalWeight"]),
                            Amount = Convert.ToDouble(i["Amount"]),
                            vatinr = Convert.ToDouble(i["vatinr"]),
                            vatper = Convert.ToDouble(i["vatper"]),
                            unit = Convert.ToString(i["unit"]),
                            Product_MOQ = Convert.ToDouble(i["Product_MOQ"]),
                            MainImg_Path = Convert.ToString(i["MainImg_Path"]),
                            href = Convert.ToString(i["href"]),
                            Product_linkas = Convert.ToString(i["Product_linkas"]),
                            Etcstring2 = Convert.ToString(dtstock.Tables[0].Rows[0]["warehouse2"]),
                            Etcstring3 = Convert.ToString(dtstock.Tables[0].Rows[0]["warehouse3"]),

                        });
                    }
                    return Json(cart, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void placeorder(Int32 PrdId, Int32 Qty)
        {

            try
            {


                //  HttpCookie cartCookie;

                if (PrdId > 0)
                {
                    DataSet dsPrd = new DataSet();
                    dsPrd = dbPt.Getcartproduct("getaddtocart", Convert.ToInt32(Session["U_Id"].ToString()), PrdId, Qty);


                    if (dsPrd.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Srno", typeof(int));
                        dt.Columns.Add("Product_Id", typeof(int));
                        dt.Columns.Add("Product_Name", typeof(string));
                        dt.Columns.Add("Product_Code", typeof(string));
                        dt.Columns.Add("Product_Rate", typeof(string));
                        dt.Columns.Add("Quantity", typeof(string));
                        dt.Columns.Add("Packing_Weight", typeof(string));
                        dt.Columns.Add("TotalWeight", typeof(string));
                        dt.Columns.Add("Amount", typeof(string));
                        dt.Columns.Add("vatinr", typeof(string));
                        dt.Columns.Add("vatper", typeof(string));
                        dt.Columns.Add("unit", typeof(string));
                        dt.Columns.Add("Product_MOQ", typeof(string));
                        dt.Columns.Add("MainImg_Path", typeof(string));
                        dt.Columns.Add("href", typeof(string));
                        dt.Columns.Add("Product_linkas", typeof(string));

                        PID = BstarGeneral.GetIntVal(dsPrd.Tables[0].Rows[0]["Product_Id"].ToString());
                        ProductName = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["Product_Name"].ToString()).ToString();
                        Code = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["Product_Code"].ToString()).ToString();
                        rate = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Product_Rate"].ToString());
                        Quantity = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Quantity"].ToString());
                        Packing_Weight = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Packing_Weight"].ToString());
                        TotalWeight = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["TotalWeight"].ToString());
                        Amount = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Amount"].ToString());
                        vatinr = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["vatinr"].ToString());
                        vatper = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["vatper"].ToString());
                        unit = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["unit"].ToString()).ToString();
                        Product_MOQ = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Product_MOQ"].ToString());
                        ProdImg = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["MainImg_Path"].ToString()).ToString();
                        href = "/product/" + BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["href"].ToString()).ToString();
                        Product_linkas = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["Product_linkas"].ToString()).ToString();
                        cookiePID = (PID * 7.3).ToString();
                        cookiePID = cookiePID.Replace(",", "");
                        cartCookie = Request.Cookies["TheCart"];
                        if (cartCookie == null)
                            Response.Cookies.Add(cartCookie);
                        string MainString = cartCookie.Value;
                        string SearchString;
                        int FirstChr;

                        if (MainString == "")
                            FirstChr = -1;
                        else
                        {
                            SearchString = cookiePID;
                            FirstChr = MainString.IndexOf(SearchString);
                        }

                        if (FirstChr < 0)
                        {
                            if (cartCookie.Value == "")
                                cartCookie.Value = cookiePID + ",";
                            else
                                cartCookie.Value = cartCookie.Value + cookiePID + ",";
                        }
                        Response.Cookies.Add(cartCookie);

                    }
                }
                if (Session["OrderProduct"] == null)
                {
                    count = 0;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Srno", typeof(int));
                    dt.Columns.Add("Product_Id", typeof(int));
                    dt.Columns.Add("Product_Name", typeof(string));
                    dt.Columns.Add("Product_Code", typeof(string));
                    dt.Columns.Add("Product_Rate", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Packing_Weight", typeof(string));
                    dt.Columns.Add("TotalWeight", typeof(string));
                    dt.Columns.Add("Amount", typeof(string));
                    dt.Columns.Add("vatinr", typeof(string));
                    dt.Columns.Add("vatper", typeof(string));
                    dt.Columns.Add("unit", typeof(string));
                    dt.Columns.Add("Product_MOQ", typeof(string));
                    dt.Columns.Add("MainImg_Path", typeof(string));
                    dt.Columns.Add("href", typeof(string));
                    dt.Columns.Add("Product_linkas", typeof(string));
                    // double PID, Amount, vatinr, vatper, rate, WithoutdicRate, discount, Quantity, Packing_Weight, TotalWeight, Product_MOQ;
                    //  string Code, ProductName, ProdImg, unit, href, cookiePID;

                    //  amt = Math.Round(Convert.ToDouble(txtQty.Text) * rate);
                    // kgs = (Convert.ToDouble(txtQty.Text) * Convert.ToDouble(lblpackweight.Value))
                    //   totalvat = Math.Round(Convert.ToDouble((vat * amt) / (double)100));
                    dt.Rows.Add(count, PID, ProductName, Code, rate, Quantity, Packing_Weight, TotalWeight, Amount, vatinr, vatper, unit, Product_MOQ, ProdImg, href, Product_linkas);

                    count = count + 1;
                    string Checkgroupname = "";
                    if (Session["U_Id"].ToString() != null)
                    {
                        DataSet datacart = dbCart.CheckCartName(PrdId.ToString(), Session["U_Id"].ToString(), "CartCheck");
                        Checkgroupname = datacart.Tables[0].Rows[0]["result"].ToString();
                        if ((Checkgroupname == "Already Exist"))
                        {
                            dbCart.Update(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString(), Convert.ToInt32(datacart.Tables[1].Rows[0]["Cart_Id"]));
                        }
                        else
                            dbCart.Insert(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString());
                    }




                    Session["OrderProduct"] = dt;
                    if (dt.Rows.Count == 0)
                        throw new Exception(": Please Select Atleast One Product");
                }
                else
                {
                    DataTable dt = (DataTable)Session["OrderProduct"];
                    count = dt.Rows.Count;

                    bool productCount = false;

                    if (Qty == 0)
                        Qty = 1;
                    productCount = true;

                    bool contains = dt.AsEnumerable().Any(row => PID == row.Field<int>("Product_Id"));
                    if (contains)
                    {
                        DataRow[] oldrow = dt.Select("Product_Id='" + PID + "'");
                        Qty = Convert.ToInt32(Qty) + Convert.ToInt32(oldrow[0]["Quantity"]);
                        placeorderQ(PrdId, Qty);

                        //dt.Rows.Remove(oldrow[0]);
                        //Amount = ((Convert.ToInt32(Qty) * Convert.ToDouble(Packing_Weight)) * Convert.ToDouble(rate));

                        //vatinr = Convert.ToDouble((vatper * Amount) / (double)100);

                        //dt.Rows.Add(count, PID, ProductName, Code, rate, Quantity, Packing_Weight, TotalWeight, Amount, vatinr, vatper, unit, Product_MOQ, ProdImg, href, Product_linkas);
                        //count = count + 1;
                        //if (Session("Cust_Id") != null)
                        //{
                        //    Checkgroupname = CartBLL.CheckCartName(PrdId, Session("Cust_Id"), "CartCheck");
                        //    if ((Checkgroupname == "Already Exist"))
                        //    {
                        //    }
                        //    else
                        //        CartBLL.Insert(Session("Cust_Id"), PrdId, txtQty.Text, Session("Cust_Id"));
                        //}
                    }
                    else
                    {
                        //  Amount = ((Convert.ToInt32(Qty) * Convert.ToDouble(Packing_Weight)) * Convert.ToDouble(rate));
                        //  vatinr = Convert.ToDouble((vatper * Amount) / (double)100);


                        dt.Rows.Add(count, PID, ProductName, Code, rate, Quantity, Packing_Weight, TotalWeight, Amount, vatinr, vatper, unit, Product_MOQ, ProdImg, href, Product_linkas);

                        count = count + 1;
                        string Checkgroupname = "";
                        if (Session["U_Id"].ToString() != null)
                        {
                            DataSet datacart = dbCart.CheckCartName(PrdId.ToString(), Session["U_Id"].ToString(), "CartCheck");
                            Checkgroupname = datacart.Tables[0].Rows[0]["result"].ToString();
                            if ((Checkgroupname == "Already Exist"))
                            {
                                dbCart.Update(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString(), Convert.ToInt32(datacart.Tables[1].Rows[0]["Cart_Id"]));
                            }
                            else
                                dbCart.Insert(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString());
                        }
                    }


                    Session["OrderProduct"] = dt;
                    //// btnBuy.Visible = False
                    //btnCart.Visible = false;
                    //divqty.Visible = false;
                    //divtotalpr.Visible = false;
                    //btngotocart.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void placeorderQ(Int32 PrdId, Int32 Qty)
        {

            try
            {

                //HttpCookie cartCookie;

                if (PrdId > 0)
                {
                    DataSet dsPrd = new DataSet();
                    dsPrd = dbPt.Getcartproduct("getaddtocart", Convert.ToInt32(Session["U_Id"].ToString()), PrdId, Qty);


                    if (dsPrd.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Srno", typeof(int));
                        dt.Columns.Add("Product_Id", typeof(int));
                        dt.Columns.Add("Product_Name", typeof(string));
                        dt.Columns.Add("Product_Code", typeof(string));
                        dt.Columns.Add("Product_Rate", typeof(string));
                        dt.Columns.Add("Quantity", typeof(string));
                        dt.Columns.Add("Packing_Weight", typeof(string));
                        dt.Columns.Add("TotalWeight", typeof(string));
                        dt.Columns.Add("Amount", typeof(string));
                        dt.Columns.Add("vatinr", typeof(string));
                        dt.Columns.Add("vatper", typeof(string));
                        dt.Columns.Add("unit", typeof(string));
                        dt.Columns.Add("Product_MOQ", typeof(string));
                        dt.Columns.Add("MainImg_Path", typeof(string));
                        dt.Columns.Add("href", typeof(string));
                        dt.Columns.Add("Product_linkas", typeof(string));

                        PID = BstarGeneral.GetIntVal(dsPrd.Tables[0].Rows[0]["Product_Id"].ToString());
                        ProductName = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["Product_Name"].ToString()).ToString();
                        Code = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["Product_Code"].ToString()).ToString();
                        rate = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Product_Rate"].ToString());
                        Quantity = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Quantity"].ToString());
                        Packing_Weight = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Packing_Weight"].ToString());
                        TotalWeight = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["TotalWeight"].ToString());
                        Amount = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Amount"].ToString());
                        vatinr = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["vatinr"].ToString());
                        vatper = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["vatper"].ToString());
                        unit = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["unit"].ToString()).ToString();
                        Product_MOQ = BstarGeneral.GetDoubleVal(dsPrd.Tables[0].Rows[0]["Product_MOQ"].ToString());
                        ProdImg = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["MainImg_Path"].ToString()).ToString();
                        href = "/product/" + BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["href"].ToString()).ToString();
                        Product_linkas = BstarGeneral.GetStringVal(dsPrd.Tables[0].Rows[0]["Product_linkas"].ToString()).ToString();
                        cookiePID = (PID * 7.3).ToString();
                        cookiePID = cookiePID.Replace(",", "");
                        cartCookie = Request.Cookies["TheCart"];
                        if (cartCookie == null)
                            Response.Cookies.Add(cartCookie);
                        string MainString = cartCookie.Value;
                        string SearchString;
                        int FirstChr;

                        if (MainString == "")
                            FirstChr = -1;
                        else
                        {
                            SearchString = cookiePID;
                            FirstChr = MainString.IndexOf(SearchString);
                        }

                        if (FirstChr < 0)
                        {
                            if (cartCookie.Value == "")
                                cartCookie.Value = cookiePID + ",";
                            else
                                cartCookie.Value = cartCookie.Value + cookiePID + ",";
                        }
                        Response.Cookies.Add(cartCookie);

                    }
                }
                if (Session["OrderProduct"] == null)
                {
                    count = 0;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Srno", typeof(int));
                    dt.Columns.Add("Product_Id", typeof(int));
                    dt.Columns.Add("Product_Name", typeof(string));
                    dt.Columns.Add("Product_Code", typeof(string));
                    dt.Columns.Add("Product_Rate", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Packing_Weight", typeof(string));
                    dt.Columns.Add("TotalWeight", typeof(string));
                    dt.Columns.Add("Amount", typeof(string));
                    dt.Columns.Add("vatinr", typeof(string));
                    dt.Columns.Add("vatper", typeof(string));
                    dt.Columns.Add("unit", typeof(string));
                    dt.Columns.Add("Product_MOQ", typeof(string));
                    dt.Columns.Add("MainImg_Path", typeof(string));
                    dt.Columns.Add("href", typeof(string));
                    dt.Columns.Add("Product_linkas", typeof(string));
                    // double PID, Amount, vatinr, vatper, rate, WithoutdicRate, discount, Quantity, Packing_Weight, TotalWeight, Product_MOQ;
                    //  string Code, ProductName, ProdImg, unit, href, cookiePID;

                    //  amt = Math.Round(Convert.ToDouble(txtQty.Text) * rate);
                    // kgs = (Convert.ToDouble(txtQty.Text) * Convert.ToDouble(lblpackweight.Value))
                    //   totalvat = Math.Round(Convert.ToDouble((vat * amt) / (double)100));
                    dt.Rows.Add(count, PID, ProductName, Code, rate, Quantity, Packing_Weight, TotalWeight, Amount, vatinr, vatper, unit, Product_MOQ, ProdImg, href, Product_linkas);

                    count = count + 1;
                    string Checkgroupname = "";
                    if (Session["U_Id"].ToString() != null)
                    {
                        DataSet datacart = dbCart.CheckCartName(PrdId.ToString(), Session["U_Id"].ToString(), "CartCheck");
                        Checkgroupname = datacart.Tables[0].Rows[0]["result"].ToString();
                        if ((Checkgroupname == "Already Exist"))
                        {
                            dbCart.Update(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString(), Convert.ToInt32(datacart.Tables[1].Rows[0]["Cart_Id"]));
                        }
                        else
                            dbCart.Insert(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString());
                    }




                    Session["OrderProduct"] = dt;
                    if (dt.Rows.Count == 0)
                        throw new Exception(": Please Select Atleast One Product");
                }
                else
                {
                    DataTable dt = (DataTable)Session["OrderProduct"];
                    count = dt.Rows.Count;

                    bool productCount = false;

                    if (Qty == 0)
                        Qty = 1;
                    productCount = true;

                    bool contains = dt.AsEnumerable().Any(row => PID == row.Field<int>("Product_Id"));
                    if (contains)
                    {
                        // DataRow[] oldrow = dt.Select("Product_Id='" + PID + "'");
                        //Qty = Convert.ToInt32(Qty) + Convert.ToInt32(oldrow[0]["Quantity"]);

                        // dt.Rows.Remove(oldrow[0]);
                        //Amount = ((Convert.ToInt32(Qty) * Convert.ToDouble(Packing_Weight)) * Convert.ToDouble(rate));

                        //vatinr = Convert.ToDouble((vatper * Amount) / (double)100);

                        // dt.Rows.Add(count, PID, ProductName, Code, rate, Quantity, Packing_Weight, TotalWeight, Amount, vatinr, vatper, unit, Product_MOQ, ProdImg, href, Product_linkas);
                        //  count = count + 1;
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row1 in dt.Rows)
                            {
                                if (Convert.ToInt32(row1["Product_Id"]) == PrdId)
                                {
                                    row1["Product_Rate"] = rate;
                                    row1["TotalWeight"] = TotalWeight;
                                    row1["Quantity"] = Quantity;
                                    row1["vatinr"] = vatinr;
                                    row1["Amount"] = Amount;
                                    Session["OrderProduct"] = dt;
                                    break;
                                }
                            }
                        }



                        string Checkgroupname = "";
                        if (Session["U_Id"].ToString() != null)
                        {
                            DataSet datacart = dbCart.CheckCartName(PrdId.ToString(), Session["U_Id"].ToString(), "CartCheck");
                            Checkgroupname = datacart.Tables[0].Rows[0]["result"].ToString();
                            if ((Checkgroupname == "Already Exist"))
                            {
                                dbCart.Update(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString(), Convert.ToInt32(datacart.Tables[1].Rows[0]["Cart_Id"]));
                            }
                            else
                                dbCart.Insert(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString());
                        }

                    }
                    else
                    {
                        //  Amount = ((Convert.ToInt32(Qty) * Convert.ToDouble(Packing_Weight)) * Convert.ToDouble(rate));
                        //  vatinr = Convert.ToDouble((vatper * Amount) / (double)100);


                        dt.Rows.Add(count, PID, ProductName, Code, rate, Quantity, Packing_Weight, TotalWeight, Amount, vatinr, vatper, unit, Product_MOQ, ProdImg, href, Product_linkas);

                        count = count + 1;
                        string Checkgroupname = "";
                        if (Session["U_Id"].ToString() != null)
                        {
                            DataSet datacart = dbCart.CheckCartName(PrdId.ToString(), Session["U_Id"].ToString(), "CartCheck");
                            Checkgroupname = datacart.Tables[0].Rows[0]["result"].ToString();
                            if ((Checkgroupname == "Already Exist"))
                            {
                                dbCart.Update(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString(), Convert.ToInt32(datacart.Tables[1].Rows[0]["Cart_Id"]));
                            }
                            else
                                dbCart.Insert(Session["U_Id"].ToString(), PrdId, Qty, Session["U_Id"].ToString());
                        }

                    }


                    Session["OrderProduct"] = dt;
                    //// btnBuy.Visible = False
                    //btnCart.Visible = false;
                    //divqty.Visible = false;
                    //divtotalpr.Visible = false;
                    //btngotocart.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult Getyield(string pid)
        {
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();
            if (Session["U_Id"] == null)
            {
                return Json("/login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                ds = dbPt.GetProductDetailbyid(pid, Session["U_Id"].ToString(), "getyield");




                IList<CommonStruct> yield = new List<CommonStruct>();

                foreach (DataRow i in ds.Tables[0].Rows)
                {
                    yield.Add(new CommonStruct()
                    {
                        //   ,   Product_Id, Product_Name,   Yield_BakerCalc,    Yield_Qty,  priceperQTY ,Yield_Percent, Yield_UnitWt,   price   Product_Vat Product_Seourl
                        Type = i["type"].ToString(),
                        Id = Convert.ToInt32(i["Product_Id"]),
                        Product_Name = i["Product_Name"].ToString(),
                        Etcstring2 = i["Yield_BakerCalc"].ToString(),
                        Qty = Convert.ToDouble(i["Yield_Qty"]),
                        EtcDouble1 = Convert.ToDouble(i["Yield_Qty"]),
                        Amount = Convert.ToDouble(i["priceperQTY"]),
                        unit = i["Yield_UnitWt"].ToString(),
                        Product_Rate = Convert.ToDouble(i["Product_Rate"]),
                        href = i["Product_Seourl"].ToString()


                    });
                }

                //string js = JsonConvert.SerializeObject(Address);

                return Json(yield, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetRecipegrid()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds = dbrecipe.GetList();
            dt = ds.Tables[0];

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);

                }


                rows.Add(row);
            }


            return Json(rows, JsonRequestBehavior.AllowGet);

        }


        public ActionResult fillQuickCat(string type)
        {
            try
            {
                DataTable dt;
                if (type == "all")
                {

                    dt = dbct.GetList().Tables[0];
                }
                else
                {
                    dt = dbct.GetRecentCat("RecentProductCat", Convert.ToInt32(Session["U_Id"])).Tables[0];
                }

                //  if (Session["OrderProduct"] != null)
                //  {
                IList<CommonStruct> category = new List<CommonStruct>();

                foreach (DataRow i in dt.Rows)
                {
                    //DataSet dtstock = dbCart.b2bGetbothstock(Convert.ToInt32(i["Product_Id"]));

                    category.Add(new CommonStruct()
                    {
                        CatId = Convert.ToInt32(i["PrdCategory_ID"]),
                        PrdCategory_Name = Convert.ToString(i["PrdCategory_Name"]),

                    });
                }
                return Json(category, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json("", JsonRequestBehavior.AllowGet);
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public ActionResult fillQuickProd(int id, string type)
        {
            try
            {

                // DataTable dt;//(DataTable)Session["OrderProduct"];

                if (Session["U_Id"] != null)
                {
                    DataTable dt;
                    if (type == "all")
                    {
                        dt = dbPt.Quickprod(id, Convert.ToInt32(Session["U_Id"]), "QuickProduct").Tables[0];
                    }
                    else
                    {
                        dt = dbPt.Quickprod(id, Convert.ToInt32(Session["U_Id"]), "RecentQuickProduct").Tables[0];

                    }
                    IList<CommonStruct> cart = new List<CommonStruct>();
                    //  string DomainName  = System.Configuration.ConfigurationManager.AppSettings["domain"];
                    foreach (DataRow i in dt.Rows)
                    {
                        //  DataSet  dtstock = dbCart.b2bGetbothstock(Convert.ToInt32(i["Product_Id"]));

                        cart.Add(new CommonStruct()
                        {


                            Product_Id = Convert.ToInt32(i["Product_Id"]),
                            Product_Name = Convert.ToString(i["Product_Name"]),
                            Product_Code = Convert.ToString(i["Product_Code"]),
                            Product_Rate = Convert.ToDouble(i["Product_Rate"]),
                            //  Qty = Convert.ToInt32(i["Quantity"]),
                            Packing_Weight = Convert.ToDouble(i["Packing_Weight"]),
                            // TotalWeight = Convert.ToDouble(i["TotalWeight"]),
                            Amount = Convert.ToDouble("0.00"),
                            // vatinr = Convert.ToDouble(i["vatinr"]),
                            // vatper = Convert.ToDouble(i["vatper"]),
                            // unit = Convert.ToString(i["unit"]),
                            Product_MOQ = Convert.ToDouble(i["Product_MOQ"]),
                            // MainImg_Path = Convert.ToString(i["MainImg_Path"]),
                            href = Convert.ToString(i["Product_Seourl"]),
                            Product_Recipes = Convert.ToString(i["Product_Recipes"]),
                            Product_Dosage = Convert.ToString(i["Product_Dosage"]),
                            Product_Usage = Convert.ToString(i["Product_Usage"]),
                            Product_Desc = Convert.ToString(i["Product_Desc"]),
                            Packing_Packing = Convert.ToString(i["Packing_Packing"]),
                            TotalWeight = Convert.ToDouble("0.00"), //kg
                            Etcstring1 = "", //pack
                            EtcDouble1 = 0, //ProTtl

                            // Product_linkas = Convert.ToString(i["Product_linkas"]),
                            //Etcstring2= Convert.ToString(dtstock.Tables[0].Rows[0]["warehouse2"]),
                            // Etcstring3 = Convert.ToString(dtstock.Tables[0].Rows[0]["warehouse3"]),

                        });
                    }
                    return Json(cart, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult changeQOqty(Int32 D, Int32 Q, Int32 M, string L)
        {
            try
            {
                if (Session["U_Id"] == null)
                {
                    //return Redirect("/login");
                    return Json("/login", JsonRequestBehavior.AllowGet);
                }

                else
                {

                    // Int32 D, Int32 Q,  Int32 M, string L
                    // D is ProductID , Q is Qty  ,A is button name, M is MOQ and L is linkas.

                    //Int32 D   =13;
                    //Int32 Q   =1;
                    //Int32 M  =0;
                    //string L ="Product";
                    L = "Product";
                    TempData["MOQ"] = M;
                    TempData["linkas"] = L;
                    TempData.Keep("MOQ");
                    TempData.Keep("linkas");
                    if (L == "Sample")
                    {
                        Q = 1;
                    }

                    DataSet ds = new DataSet();
                    string flag = "false";
                    if (Q <= 0) { Q = 1; }
                    if (Q >= Convert.ToInt32(TempData["MOQ"])) { flag = "true"; }
                    if (flag == "true")
                    {
                        int orQty = 0;
                        int smQty = 0;
                        if (TempData["linkas"].ToString() == "Sample")
                        {
                            ds = dbPt.GetProductDetailbyid(D.ToString(), Session["U_Id"].ToString(), "checkSampleorder");
                            orQty = Convert.ToInt32(ds.Tables[0].Rows[0]["totalorder"]);
                            if (Session["OrderProduct"] == null)
                            {
                            }
                            else
                            {
                                DataTable dt1 = (DataTable)Session["OrderProduct"];
                                smQty = dt1.AsEnumerable().Where(x => x["Product_linkas"].ToString() == "Sample").ToList().Count;
                            }
                        }
                        else
                        {
                            smQty = 0;
                        }
                        if (orQty == 0 || orQty <= 2)
                        {

                            if (smQty == 0 || smQty <= 4)
                            {

                                DataSet dsPrd = new DataSet();
                                dsPrd = dbPt.Getcartproduct("getaddtocart", Convert.ToInt32(Session["U_Id"].ToString()), D, Q);

                                // placeorderQ(D, Q);
                                // var list = JsonConvert.SerializeObject((DataTable)Session["OrderProduct"], Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });


                                IList<CommonStruct> updateQO = new List<CommonStruct>();
                                //DataTable dt = (DataTable)Session["OrderProduct"];
                                foreach (DataRow i in dsPrd.Tables[0].Rows)
                                {
                                    updateQO.Add(new CommonStruct()
                                    {

                                        //Id = Convert.ToInt32(i["Srno"]),
                                        //Product_Id = Convert.ToInt32(i["Product_Id"]),
                                        //Product_Name = Convert.ToString(i["Product_Name"]),
                                        //Product_Code = Convert.ToString(i["Product_Code"]),
                                        Product_Rate = Convert.ToInt32(i["Product_Rate"]),
                                        //Qty = Convert.ToInt32(i["Quantity"]),
                                        //Packing_Weight = Convert.ToDouble(i["Packing_Weight"]),
                                        //TotalWeight = Convert.ToDouble(i["TotalWeight"]),
                                        //Amount = Convert.ToDouble(i["Amount"]),
                                        //vatinr = Convert.ToDouble(i["vatinr"]),
                                        //vatper = Convert.ToDouble(i["vatper"]),
                                        //unit = Convert.ToString(i["unit"]),
                                        //Product_MOQ = Convert.ToDouble(i["Product_MOQ"]),
                                        //MainImg_Path = Convert.ToString(i["MainImg_Path"]),
                                        //href = Convert.ToString(i["href"]),

                                    });
                                }


                                return Json(updateQO, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json("You have reached maximum sample limit for this order", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("You have already bought this sample", JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        //return Content("<script language='javascript' type='text/javascript'>alert('Minimum Order Quantity for this product is '" + ViewBag.MOQ + "');</script>");
                        // return Json("Minimum Order Quantity for this product is " + TempData["MOQ"] + " KG", JsonRequestBehavior.AllowGet);
                        return Json("Minimum Order Quantity for this product is " + TempData["MOQ"] + " No. of pack", JsonRequestBehavior.AllowGet);
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult QOaddcart(Int32 D, Int32 Q)
        {
            try
            {
                if (Session["U_Id"] == null)
                {
                    //return Redirect("/login");
                    return Json("/login", JsonRequestBehavior.AllowGet);
                }

                else
                {
                    TempData["MOQ"] = 1;
                    TempData["linkas"] = "Product";
                    // D is ProductID , Q is Qty and A is button name
                    TempData.Keep("MOQ");
                    TempData.Keep("linkas");

                    DataSet ds = new DataSet();
                    string flag = "false";
                    if (Q <= 0) { Q = 1; }
                    if (Q >= Convert.ToInt32(TempData["MOQ"])) { flag = "true"; }
                    if (flag == "true")
                    {
                        int orQty = 0;
                        int smQty = 0;
                        if (TempData["linkas"].ToString() == "Sample")
                        {
                            Q = 1;
                            ds = dbPt.GetProductDetailbyid(D.ToString(), Session["U_Id"].ToString(), "checkSampleorder");
                            orQty = Convert.ToInt32(ds.Tables[0].Rows[0]["totalorder"]);
                            if (Session["OrderProduct"] == null)
                            {
                            }
                            else
                            {
                                DataTable dt1 = (DataTable)Session["OrderProduct"];
                                smQty = dt1.AsEnumerable().Where(x => x["Product_linkas"].ToString() == "Sample").ToList().Count;
                            }
                        }
                        else
                        {
                            smQty = 0;
                        }
                        if (orQty == 0 || orQty <= 2)
                        {

                            if (smQty == 0 || smQty <= 4)
                            {


                                placeorder(D, Q);
                                //return PartialView();
                                //  return PartialView("_PartialCart", Session["OrderProduct"]);
                                // return Json((DataTable)Session["OrderProduct"]);
                                var list = JsonConvert.SerializeObject((DataTable)Session["OrderProduct"], Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });


                                IList<CommonStruct> cart = new List<CommonStruct>();
                                DataTable dt = (DataTable)Session["OrderProduct"];
                                foreach (DataRow i in dt.Rows)
                                {
                                    cart.Add(new CommonStruct()
                                    {

                                        Id = Convert.ToInt32(i["Srno"]),
                                        Product_Id = Convert.ToInt32(i["Product_Id"]),
                                        Product_Name = Convert.ToString(i["Product_Name"]),
                                        Product_Code = Convert.ToString(i["Product_Code"]),
                                        Product_Rate = Convert.ToInt32(i["Product_Rate"]),
                                        Qty = Convert.ToInt32(i["Quantity"]),
                                        Packing_Weight = Convert.ToDouble(i["Packing_Weight"]),
                                        TotalWeight = Convert.ToDouble(i["TotalWeight"]),
                                        Amount = Convert.ToDouble(i["Amount"]),
                                        vatinr = Convert.ToDouble(i["vatinr"]),
                                        vatper = Convert.ToDouble(i["vatper"]),
                                        unit = Convert.ToString(i["unit"]),
                                        Product_MOQ = Convert.ToDouble(i["Product_MOQ"]),
                                        MainImg_Path = Convert.ToString(i["MainImg_Path"]),
                                        href = Convert.ToString(i["href"]),

                                    });
                                }

                                string[] result = new string[dt.Columns.Count];
                                DataRow dr = dt.Rows[0];
                                for (int i = 0; i < dr.ItemArray.Length; i++)
                                {
                                    result[i] = dr[i].ToString();
                                }
                                return Json(cart, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json("You have reached maximum sample limit for this order", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("You have already bought this sample", JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        //return Content("<script language='javascript' type='text/javascript'>alert('Minimum Order Quantity for this product is '" + ViewBag.MOQ + "');</script>");
                        // return Json("Minimum Order Quantity for this product is " + TempData["MOQ"] + " KG", JsonRequestBehavior.AllowGet);
                        return Json("Minimum Order Quantity for this product is " + TempData["MOQ"] + " No. of pack", JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
