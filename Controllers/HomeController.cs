using SwissbakeIn.App_Start;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prolib;
using System.Data;
using System.Web.UI.WebControls;
//using SwissbakeIn.Models;
using SwissbakeIn.Entities;
using SwissbakeIn.Controllers;
using reCAPTCHA.MVC;
using System.IO;
using System.Text;
using System.Net;

namespace SwissbakeIn.Controllers
{
    [AuditLog]
    [BuildCart]
    [ETag]
    [WhitespaceFilter]
    [GZipOrDeflate]
    public class HomeController : Controller
    {


        Category dbct = new Category();
        Recipe dbrp = new Recipe();
        Home dbhome = new Home();
        Comon dbcommon = new Comon();
        Product dbPt = new Product();
        Checkexist dbcheck = new Checkexist();
        Dropdown dbdrop = new Dropdown();
        Order dbOt = new Order();
        Account dbAt = new Account();
        payment dbPayt = new payment();
        Solution dbsol = new Solution();
        string custName, custAddress, custCity, custState, custPinCode, custCountry, custPhone, custMobileNo, custEmailId, deliveryName, deliveryAddress, deliveryCity, deliveryState, deliveryPinCode, deliveryCountry, deliveryMobileNo, deliveryEmailId, Fail, Success, Other, Amount, OrdNo, UploadPath;
        //   string path = "https://b2b.swissbake.ch"; //Path used for images from b2b

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Index()
        {
            ViewBag.browser = Request.Browser.Browser;
            if (Request.Browser.Browser == "Chrome" && Request.Browser.MajorVersion >= 65)
            {
                Session["imgtype"] = "format=webp";
            }
            else
            {
                Session["imgtype"] = "format=png";
            }

            return View();
        }
        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Index2()
        {
            return View();
        }
        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        [OutputCache(Duration = 18600, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index3()
        {
            ViewBag.browser = Request.Browser.Browser;
            if (Request.Browser.Browser == "Chrome" && Request.Browser.MajorVersion >= 65)
            {
                Session["imgtype"] = "quality=5&quality=0&format=webp";
            }
            else
            {
                Session["imgtype"] = "quality=5&quality=0&format=jpg";
            }
            return View();
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        //[OutputCache(Duration = 18600, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index4()
        {
            //ViewBag.browser = Request.Browser.Browser;
            //if (Request.Browser.Browser == "Chrome" && Request.Browser.MajorVersion >= 65)
            //{
            //    Session["imgtype"] = "quality=5&quality=0&format=webp";
            //}
            //else
            //{
            //    Session["imgtype"] = "quality=5&quality=0&format=jpg";
            //}

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            ds = dbhome.GetList();


            //var translator = new Translator();

            dt = ds.Tables[0];

            IList<CommonStruct> duibannerlist = new List<CommonStruct>();

            var counter = 0;

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                duibannerlist.Add(new CommonStruct()
                {
                    Image = Convert.ToString(i["Banner_ImgPath"]).Replace("®", ""),
                    Url = Convert.ToString(i["Banner_URL"]),
                    //Name = counter > 0 ? translator.Translate(Convert.ToString(i["BannerContent"]), "English", "Arabic") : Convert.ToString(i["BannerContent"]),
                    Name = Convert.ToString(i["BannerContent"]),
                    Type = Convert.ToString(i["Banner_Type"]),
                    Etcstring1 =Convert.ToString(i["BannerTitle"])
                });
                counter++;
            }




            ViewData["dualbanner"] = duibannerlist; // dt.AsEnumerable();
                                                    // ViewData["dualbanner"] = dualbanner;
                                                    //  ViewData["duibanner"] = duibanner;

            //  ViewData["dualbanner"] = ds;

            dt.Clear();
            ds.Clear();

            ds = dbct.GetList6();

            DataSet ds2 = new DataSet();

            ds2 = dbhome.GetBlogCat("", "Top3Blog");

            dt = ds2.Tables[0];
            dt.TableName = "Tb1";
            ds.Tables.Add(dt.Copy());
            dt.Clear();
            ds2.Clear();

            ds2 = dbhome.GetSolution3("Top3solution");

            dt = ds2.Tables[0];
            dt.TableName = "Tb2";
            ds.Tables.Add(dt.Copy());
            dt.Clear();
            ds2.Clear();
            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Product(string url)
        {

            //   DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            //  var yr = "";


            ds = dbct.GetList(); //dbdrop.FillYearById(4, "Yeardropdown");

            //   dt = ds.Tables[0];
            DataRow[] results = ds.Tables[0].Select("PrdCategory_Seourl ='" + url + "'");
            ViewBag.mode = "getproduct";
            if (results.Length > 0)
            {

                ViewBag.Title = results[0][3];
                ViewBag.Catname = results[0][0];
                ViewBag.Oneline = results[0][7];
                ViewBag.Catdesc = results[0][8];
            }

            IList<CommonStruct> category = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                category.Add(new CommonStruct()
                {
                    PrdCategory_ID = Convert.ToInt32(i["PrdCategory_ID"]),
                    PrdCategory_Name = Convert.ToString(i["PrdCategory_Name"]),
                    PrdCategory_Seotitle = Convert.ToString(i["PrdCategory_Seotitle"]),
                    PrdCategory_Seourl = Convert.ToString(i["PrdCategory_Seourl"]),
                    //Code = Convert.ToString(i["Order_Code"]),
                    //Datetime = Convert.ToString(i["Order_CreatedDate"]),
                    //Etcstring1 = Convert.ToString(i["Order_Amount"])
                });

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["catli"] = category;


            // dt.Clear();
            ds.Clear();

            //var caturl  = url;

            //ViewBag.sltyear = null;

            //if (Request.Form["url"] != null)
            //{
            //    caturl = Request.Form["url"];
            //    ViewBag.sltyear = Request.Form["url"];
            //}




            return View("product");
        }
        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        //[Route("category")]
        public ActionResult Category()
        {
            DataSet ds = new DataSet();

            ds = dbct.GetList();





            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        //[Route ("{Curl}/{Purl}")]
        public ActionResult Detail(string Purl)
        {
            ViewBag.error = Session["error"];
            Session["error"] = null;
            TempData["InCart"] = "false";
            string id = dbPt.GetProductID(Purl, "getproductIDbyurl");
            //  bool result = id.Any(x => !char.IsLetter(x));
            bool digitsOnly = id.All(char.IsDigit);
            if (digitsOnly == true)
            {

                ViewBag.PID = id;

                DataSet ds = new DataSet();
                if (Session["U_Id"] != null)
                {
                    // ds = dbPt.GetProductDetail(Purl, Session["U_Id"].ToString());
                    ds = dbPt.GetProductDetailbyid(id, Session["U_Id"].ToString(), "getproductbyID");
                    TempData["MOQ"] = ds.Tables[0].Rows[0]["Product_MOQ"];
                    TempData["linkas"] = ds.Tables[0].Rows[0]["Product_linkas"];
                    if (Session["OrderProduct"] != null)
                    {
                        DataTable dt = (DataTable)Session["OrderProduct"];
                        // Dim id As inte
                        if (dt.Rows.Count > 0)
                        {
                            bool contains = dt.AsEnumerable().Any(row => Convert.ToInt32(id) == row.Field<int>("Product_Id"));
                            // Dim contains As Boolean = dt.AsEnumerable().Any(Function(row) lblcode.Text = row.Field(Of [String])("Code"))
                            if (contains)
                            {
                                TempData["InCart"] = "true";
                            }
                            else { TempData["InCart"] = "false"; }
                        }
                    }
                }
                else
                {
                    // ds = dbPt.GetProductDetail(Purl, "0");
                    ds = dbPt.GetProductDetailbyid(id, "0", "getproductbyID");
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.partialdetails = RenderRazorViewToString("~/Views/Access/_Productdetails.cshtml", ds);
                    return View(ds);
                }
                else
                {
                    return new EmptyResult();
                }
            }
            else
            {
                return Redirect("/");
                // return new EmptyResult();
            }
            //DataRow[] results = ds.Tables[0].Select("PrdCategory_Seourl ='" + u.Replace("/", "") + "'");
            //if (results.Length > 0)
            //{
            //    ViewBag.Title = results[0][0];
            //    ViewBag.Catname = results[0][2];
            //}
            //IList<CommonStruct> product = new List<CommonStruct>();

            //foreach (DataRow i in ds.Tables[0].Rows)
            //{
            //    product.Add(new CommonStruct()
            //    {


            //        Product_MainImage = Convert.ToString(i["Product_MainImage"]),

            //        Product_Rate = Convert.ToDouble(i["Product_Rate"]),
            //        Product_Vat = Convert.ToDouble(i["Product_Vat"]),
            //       Product_Dosage = Convert.ToString(i["Product_Dosage"]),
            //        Product_Name = Convert.ToString(i["Product_Name"]),
            //        Product_Code = Convert.ToString(i["Product_Code"]),
            //         Product_Usage = Convert.ToString(i["Product_Usage"]),
            //        Product_Stock = Convert.ToString(i["Product_Stock"]),
            //          Product_Recipes = Convert.ToString(i["Product_Recipes"]),
            //        PrdCategory_Seourl = Convert.ToString(i["PrdCategory_Seourl"]),
            //        Product_Seotitle = Convert.ToString(i["Product_Seotitle"]),
            //        Product_Seourl = Convert.ToString(i["Product_Seourl"]),
            //        Product_New = Convert.ToString(i["Product_New"]),
            //        minprice = Convert.ToString(i["minprice"]),
            //        maxprice = Convert.ToString(i["maxprice"]),
            //        Discount_Discount = Convert.ToString(i["Discount_Discount"]),
            //        discounted_Rate = Convert.ToString(i["discounted_Rate"]),
            //        Distdiscounted_Rate = Convert.ToString(i["Distdiscounted_Rate"]),
            //        DistDiscount_Discount = Convert.ToString(i["DistDiscount_Discount"]),
            //        Discount_MinQty = Convert.ToString(i["Discount_MinQty"]),
            //        Product_OneLineDesc = Convert.ToString(i["Product_OneLineDesc"]),

            //        Product_Desc =Convert.ToString(i["Product_Desc"]),
            //        Product_Description = Convert.ToString(i["Product_Description"]),
            //        Product_Nutrionfacts = Convert.ToString(i["Product_Nutrionfacts"]),
            //        Product_Ingrdt = Convert.ToString(i["Product_Ingrdt"]),
            //        Product_Contains = Convert.ToString(i["Product_Contains"]),
            //        Product_HSCODE = Convert.ToString(i["Product_HSCODE"]),
            //        Product_Msdc = Convert.ToString(i["Product_Msdc"]),
            //        Product_Seodesc = Convert.ToString(i["Product_Seodesc"]),
            //        Product_Seokeywords = Convert.ToString(i["Product_Seokeywords"]),
            //        Product_Seotags = Convert.ToString(i["Product_Seotags"]),

            //    });
            //}
            //  ViewData["productgrid"] = product;


            //ds2 = dbdrop.FillDrop(Convert.ToInt32(Session["U_Id"]), "DesignationDropDown");

            //dt = ds2.Tables[0];

            ////IList<CommonStruct> packaging = new List<CommonStruct>();
            ////int count = ds.Tables.Count - 2;
            ////foreach (DataRow i in ds.Tables[count].Rows)
            ////{
            ////    packaging.Add(new CommonStruct()
            ////    {
            ////        Id = Convert.ToInt32(i["Packing_ID"]),
            ////        Name = Convert.ToString(i["Packing_Packing"])
            ////    });

            ////}

            ////if (ds.Tables[count].Rows.Count > 0)
            ////{
            ////    ViewBag.packaging = new SelectList(packaging, "Id", "Name", Convert.ToString(ds.Tables[0].Rows[0].ItemArray[0]));
            ////}
            ////else
            ////{
            ////    ViewBag.packaging = new SelectList(packaging, "Id", "Name");
            ////}
            //string part = returnpartial("_ProductDetails", ds).ToString();
            //   string part = ConvertViewToString("_ProductDetails", ds);

        }



        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }





        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Register()
        {
            string country_name = Helper.checkapi();
            //if(country_name != "Bahrain")
            //{
            //    return Redirect("/export");
            //}

            //else
            //{


            if (Session["U_Id"] != null)
            {
                var t = Session["U_Id"];
                //DataTable dt = new DataTable();
                //dt = Session["LogDet"] as DataTable;

                return Redirect("/account/index");
            }

            if (Session["regId"] != null)
            {
                DataSet ds2 = new DataSet();
                if (Session["regStep"].ToString() == "4")
                {
                    ds2 = dbcheck.GetRegStepDet(Convert.ToInt32(Session["regId"]), "2");
                }
                else
                {
                    ds2 = dbcheck.GetRegStepDet(Convert.ToInt32(Session["regId"]), Session["regStep"].ToString());
                }


                DataRow i = ds2.Tables[0].Rows[0];

                ViewBag.Name = i["Contact_ContactPerson"];
                ViewBag.CompName = i["Acc_Name"];
                ViewBag.Email = i["Cust_EmailId"]?.ToString().ToUpper(); ;
                ViewBag.Mobile = i["Contact_Mobile"];
                ViewBag.Password = i["Cust_Password"];
                if (Session["regStep"].ToString() == "3")
                {
                    ViewBag.Industry = i["Acc_IndustryId"];
                }

                ViewBag.step = Session["regStep"].ToString();
                ViewBag.hfid = Session["regId"].ToString();

                if (Session["regStep"].ToString() == "4")
                {
                    //ViewBag.script = "<script>" +
                    //    "$(document).ready(function(){" +
                    //    " $('.wizard-card').addClass('d-none');$('.hb-blk').removeClass('d-none');" +
                    //    "});" +
                    //    "</script>";
                    Session["regId"] = null;
                    Session["regStep"] = null;
                    return Redirect("https://www.josefmarc.in");
                }
                else
                {
                    ViewBag.script = "<script>" +
                        "$(document).ready(function(){" +
                        " setTimeout(function(){ $('.wizard-navigation .nav-pills li:nth-child(" + Session["regStep"].ToString() + ") a').click()},500);" +
                        "});" +
                        "</script>";
                }
                Session["regId"] = null;
                Session["regStep"] = null;



            }

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            ds = dbcommon.GetRegTypeList();

            dt = ds.Tables[0];

            IList<CommonStruct> regtype = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                regtype.Add(new CommonStruct()
                {
                    Image = Convert.ToString(i["RImage"]),
                    Name = Convert.ToString(i["RName"]),
                    Id = Convert.ToInt32(i["Rid"])
                });
            }

            ViewData["regtype"] = regtype;


            ds = dbdrop.FillDrop(17, "StateDropDown");  // 105 for india

            dt = ds.Tables[0];

            IList<CommonStruct> State = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                State.Add(new CommonStruct()
                {
                    Id = Convert.ToInt32(i["ID"]),
                    Name = Convert.ToString(i["StateName"]).ToUpper()
                });

            }

            ViewBag.sttlist = new SelectList(State, "Id", "Name");
            //}
            return View();
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Login()
        {
            ViewBag.error = Session["error"];
            Session["error"] = null;
            if (Session["U_Id"] != null)
            {
                var t = Session["U_Id"];
                //DataTable dt = new DataTable();
                //dt = Session["LogDet"] as DataTable;

                return Redirect("Account/index");
            }
            try
            {
                string rur = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
                Session["Redirect_Url"] = rur;
            }
            catch
            {
                Session["Redirect_Url"] = null;
            }




            HttpCookie reqCookies = Request.Cookies["uslg"];
            var id = "";
            var pwd = "";
            if (reqCookies != null)
            {
                id = reqCookies["usid"].ToString();
                pwd = reqCookies["uspwd"].ToString();

                var result = new SwissbakeIn.Controllers.AccessController().Checklogin("", pwd, "", id);

                if (result.Data != null)
                {
                    try
                    {
                        if (result.Data != null && result.Data.ToString() != "false")
                        {
                            Session["OrderProduct"] = null;
                            return Redirect("Account/index");
                        }
                        else
                        {
                            if (Session["EmailVerifyMessage"] != null)
                            {
                                ViewBag.mess = Session["EmailVerifyMessage"].ToString();
                                Session["EmailVerifyMessage"] = null;
                            }

                            return View();
                        }
                    }
                    catch
                    {
                        if (Session["EmailVerifyMessage"] != null)
                        {
                            ViewBag.mess = Session["EmailVerifyMessage"].ToString();
                            Session["EmailVerifyMessage"] = null;
                        }
                        return View();
                    }
                }
                if (Session["EmailVerifyMessage"] != null)
                {
                    ViewBag.mess = Session["EmailVerifyMessage"].ToString();
                    Session["EmailVerifyMessage"] = null;
                }
                return View();
            }
            if (Session["EmailVerifyMessage"] != null)
            {
                ViewBag.mess = Session["EmailVerifyMessage"].ToString();
                Session["EmailVerifyMessage"] = null;
            }

            Session.Clear();
            Session.Abandon();
            return View();
        }


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult ContactUs()
        {

            ViewBag.error = Session["error"];
            Session["error"] = null;
            string country_name = Helper.checkapi();
            //if (country_name != "Bahrain")
            //{
            //    return Redirect("/export");
            //}

            //else
            //{
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            ds = dbdrop.FillDrop(105, "StateDropDown");

            dt = ds.Tables[0];

            IList<CommonStruct> State = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                State.Add(new CommonStruct()
                {
                    Id = Convert.ToInt32(i["ID"]),
                    Name = Convert.ToString(i["StateName"])
                });

            }

            ViewBag.sttlist = new SelectList(State, "Id", "Name");


            //}
            return View("Contact");
        }

        [HttpPost]
        //[CaptchaValidator]
        public ActionResult ContactUs(FormCollection form)
        {
            var captchaValid = true;
            if (captchaValid == true)
            {

                int res = dbhome.InsertEnq(form["Name"], form["Email"], form["Mobile"], form["Organisation"], form["Comment"], "Contact", "", Convert.ToInt32(form["State"]), form["City"], 17, form["Pin"], "InsertEnquiry");

                string body = "";

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/Contact.html"));
                body = read.ReadToEnd();
                body = body.Replace("#NAME_CONTACT", form["Name"]);

                //SendMAIL.SendMail(form["Name"], form["Email"], form["Mobile"], body, "Thank you ! for contacting us | SwissBake®");

                read.Dispose();

                body = "<strong>CUSTOMER DETAIL:</strong><br><br><table><tr><td>NAME </td><td>:&nbsp;" + form["Name"] + "</td></tr><tr><td>EMAIL  </td><td>:&nbsp;" + form["Email"] + "</td></tr><tr><td>ORGANISATION </td><td>:&nbsp;" + form["Organisation"] + "</td></tr><tr><td>TELEPHONE NUMBER </td><td>:&nbsp;" + form["Mobile"] + "</td></tr><tr><td>MESSAGE </td><td>:&nbsp;" + form["Comment"] + "</td></tr><tr><td>CITY </td><td>:&nbsp;" + form["City"] + "</td></tr></table><br>";

                //SendMAIL.SendMail("", "", "", body, "Contact Detail | Swiss Bake Ingredients Pvt. Ltd.");

                form.Clear();


                Session["error"] = "sucess()";
            }
            else
            {
                ViewBag.captcha = "This is required";
            }
            //var c = captchaValid;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            //ds = dbdrop.FillDrop(105, "StateDropDown");
            ds = dbdrop.FillDrop(17, "CountryDropDown");

            dt = ds.Tables[0];

            IList<CommonStruct> Country = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                Country.Add(new CommonStruct()
                {
                    Id = Convert.ToInt32(i["ID"]),
                    Name = Convert.ToString(i["CountryName"])
                });

            }

            ViewBag.sttlist = new SelectList(Country, "Id", "Name");

            return Redirect("/ContactUs");
        }
        [HttpPost]
        public ActionResult Faq(FormCollection form)
        {
            string redirect = form["redirect"];
            int res = dbhome.InsertEnq(form["Name"], form["Email"], form["Mobile"], form["Organisation"], form["Comment"], "FAQ", form["ProID"], Convert.ToInt32(form["State"]), form["City"], 17, form["Pin"], "InsertEnquiry");
            form.Clear();


            Session["error"] = "sucess()";
            return Redirect(redirect);
        }
        [Authenticate]
        [HttpPost]
        public ActionResult placeorder()
        {
            if (Session["U_Id"] == null)
            {
                return Redirect("/login");
            }

            else
            {

                //  string redirect = form["redirect"];
                //int res = dbhome.InsertEnq(form["Name"], form["Email"], form["Mobile"], form["Organisation"], form["Comment"], "Contact", "", Convert.ToInt32(form["State"]), form["City"], 105, form["Pin"], "InsertEnquiry");
                string redirect = Request["ddmodeofpayment"];

                //  Get all files from Request object  
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                    //string filename = Path.GetFileName(Request.Files[i].FileName);  

                    HttpPostedFileBase file = files[i];
                    string fname;
                    //Use Namespace called :  System.IO  
                    string FileName = Request["PONum"];// Path.GetFileNameWithoutExtension(file.FileName);

                    //To Get File Extension  
                    string FileExtension = Path.GetExtension(file.FileName);

                    //Add Current Date To Attached File Name  
                    FileName = FileName.Trim() + "-" + DateTime.Now.ToString("yyyyMMdd") + FileExtension;

                    //Get Upload path from Web.Config file AppSettings.  
                    UploadPath = "/Content/PO/" + FileName;

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/Content/PO"), FileName);
                    file.SaveAs(fname);
                }
                Insert(UploadPath);
                Session["paymenttype"] = "order";
                // Request.Clear();
                if (redirect == "Online Payment")
                {
                    //  Session["OrderProduct"] = null;
                    //  Session.Remove("OrderProduct");
                    //  return Redirect("/Paynow");
                    return Json("/Paynow", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //  Session["OrderProduct"] = null;
                    //  Session.Remove("OrderProduct");
                    // return Redirect("/");
                    return Json("/", JsonRequestBehavior.AllowGet);

                }

                //  Session["error"] = "sucess()";
            }
        }
        public void Insert(string UploadPath)
        {
            try
            {
                DataTable dt = (DataTable)Session["OrderProduct"];
                if (dt.Rows.Count > 0)
                {


                    // int ProductId = 0;
                    double totalamount = 0, totalGST = 0, freight = 0;
                    int OrderID;
                    double FreightTax = 0; //Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["freightvalue"]);// ConfigurationManager.AppSettings("freightvalue");
                    OrderID = dbOt.InsertOrder(Convert.ToInt32(Session["U_Id"]), Request["ddmodeofpayment"], FreightTax, Request["PONum"], Request["PODate"], Request["ODComment"], UploadPath);

                    foreach (DataRow row1 in dt.Rows)
                    {
                        DataSet dsPrd = new DataSet();
                        dsPrd = dbPt.Getcartproduct("getaddtocart", Convert.ToInt32(Session["U_Id"].ToString()), Convert.ToInt32(row1["Product_Id"]), Convert.ToInt32(row1["Quantity"]));
                        foreach (DataRow row in dsPrd.Tables[0].Rows)
                        {
                            dbOt.OrderDetails(OrderID, Convert.ToInt32(row["Product_Id"]), Convert.ToInt32(row["Quantity"]), Convert.ToDouble(row["Product_Rate"]), Convert.ToDouble(row["Amount"]), Convert.ToDouble(row["vatinr"]));
                        }
                    }
                    dbOt.InserOrderaddress(Convert.ToInt32(Session["DeliveryaddID"]), OrderID);
                    dbOt.updatefreight(OrderID, Convert.ToInt32(Session["U_Id"].ToString()), FreightTax);

                    DataSet dt1 = dbOt.getOrderDetails("Getorderdetails", OrderID);
                    DataTable dtUser = new DataTable();
                    dtUser = dt1.Tables[0];
                    DataRow drLogin;
                    drLogin = dtUser.Rows[0];

                    string ordercode = (drLogin["Order_Code"].ToString());

                    totalamount =Convert.ToDouble(drLogin["Order_Amount"]);
                    totalGST = (Convert.ToDouble(drLogin["Order_TotalVat"]));
                    //freight = Math.Round(Convert.ToDouble(drLogin["Order_Freight"])); // zayan 07sept23
                    //totalamount = totalamount + freight;   // zayan 07sept23

                    RegistrationMail(ordercode, OrderID, totalamount, totalGST, freight, Request["ODComment"]); //comment added on 31-8-19
                                                                                                                // faisal  
                    string host = "localhost";// HttpContext.Current.Request.Url.Host;
                    if (host == "localhost")
                    {
                    }
                    else
                    {
                        //zayan 09sept23 SendSMS.Send(dbAt.CustomerNo(Session["U_Id"].ToString()), "To " + Session["U_Name"].ToString() + ", Thank You for your order " + ordercode + " amounting to Rs. " + totalamount.ToString("N2") + ". You can check your 
                       // by login into your account at www.swissbake.bh.");
                        //zayan 09sept23 SendSMS.Send("9702044786", "*NEW ORDER* New order " + ordercode + " of amount " + totalamount.ToString("N2") + " received from " + Session["U_Name"].ToString() + " on " + DateTime.Now.ToShortDateString() + ".");
                        //zayan 09sept23 SendSMS.Send(dbAt.UserNoFromCust(Convert.ToInt32(Session["U_Id"])), "*NEW ORDER* Hi " + dbAt.FillManager(Convert.ToInt32(Session["U_Id"])).Tables[0].Rows[0][0].ToString() + ", New order received from " + Session["U_Name"].ToString() + " of Rs." + totalamount.ToString("N2") + ". Order ID  " + ordercode);

                    }
                    HttpCookie cartCookie;
                    cartCookie = Request.Cookies["TheCart"];
                    cartCookie.Value = "";
                    Response.Cookies.Add(cartCookie);
                    Session["OrderProduct"] = null;
                    Session.Remove("OrderProduct");

                    // orderCode|amt|custId|orderID
                    Session["OrderID&Amount"] = ordercode + "," + totalamount + "," + Session["U_ID"].ToString() + "," + OrderID.ToString();
                }
                else
                {
                    Response.Redirect("/");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegistrationMail(string dice, int orderid, double totalamount, double totalGST, double freight, string comment)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                int count = 1;
                string s = "";
                string body = string.Empty;
                StreamReader reader = new StreamReader(Server.MapPath("~/Content/Mail/Order_Placing.html"));
                body = reader.ReadToEnd();
                double totalQty = 0;
                DataSet ds = dbOt.ViewByID(orderid, "OrderProductViewByID");
                DataTable dt = ds.Tables[0];
                sb.Append("<table border='1' cellspacing='0' cellpadding='5' style='border-color: rgb(247, 247, 247);width:100%;text-align:center;'><tr>");
                sb.Append("<th><center>SR</center></th>");
                sb.Append("<th><center>PRODUCT NAME</center></th>");
                sb.Append("<th style='width:15%;'><center>SKU</center></th>");
                sb.Append("<th style='width:15%;'><center>ORDERED QTY</center></th>");
                sb.Append("<th style='width:15%;'><center>ORDERED QTY Total</center></th>");
                sb.Append("<th style='width:15%;'><center>TOTAL AMOUNT</center></th></tr>");
                double subtotal = 0.0;
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr><td>" + count.ToString() + "</td>");
                    sb.Append("<td>" + dr["Product_Name"].ToString() + "</td>");
                    sb.Append("<td>" + dr["Packing_Packing"].ToString() + "</td>");
                    sb.Append("<td>" + dr["OrDetail_Qty"].ToString() + "</td>");
                    sb.Append("<td>" + (Convert.ToDouble(dr["OrDetail_Qty"]) * Convert.ToDouble(dr["Packing_Weight"])).ToString() + " KG </td>");
                    sb.Append("<td style='text-align: right;'> BHD " + dr["OrDetail_Amount"].ToString() + "</td></tr>");
                    count = count + 1;
                    totalQty = totalQty + (Convert.ToDouble(dr["OrDetail_Qty"]) * Convert.ToDouble(dr["Packing_Weight"]));
                    subtotal = subtotal + Convert.ToDouble(dr["OrDetail_Amount"]);
                }
                totalQty = totalQty;
                sb.Append("<tr><th colspan='4' style='text-align: left;border-bottom:none;'><b style='font-weight:600'>SUB TOTAL</b></th><td><b style='font-weight:600;'>" + System.Convert.ToInt32(totalQty).ToString("N3") + " KG </b></td><td style='text-align: right;'><b style='font-weight:600;'>BHD " + subtotal.ToString("N2") + "</b></td></tr>");
                //sb.Append("<tr><th colspan='4' style='text-align: left;border-bottom:none;border-top:none;'><b style='font-weight:600'>FREIGHT AMOUNT</b></th><td> (+) </td><td style='text-align: right;'>BHD " + freight.ToString("N3") + "</td></tr>");
                sb.Append("<tr><th colspan='4' style='text-align: left;border-top:none;border-bottom:none;'><b style='font-weight:600'>TOTAL VAT</b></th><td> (+) </td><td style='text-align: right;'>BHD " + totalGST.ToString("N3") + "</td></tr>");
                sb.Append("<tr><th colspan='4' style='text-align: left;border-top:none;'><b style='font-weight:600'>AMOUNT PAYABLE</b></th><th colspan='2' style='text-align: right;'><b style='font-weight:600;'>BHD " + totalamount.ToString("N3") + "</b></th></tr>");

                sb.Append("</table>");

                body = body.Replace("#Table", sb.ToString());

                body = body.Replace("#ORDERID", dice);
                body = body.Replace("#NAME", Session["U_Name"].ToString());
                if (comment == "")
                {
                    body = body.Replace("#comments", "");
                }
                else
                {

                    body = body.Replace("#comments", "<p >Order instructions / Comments: " + comment + " </p>");
                }
                string email = dbAt.Email(Convert.ToInt32(Session["U_Id"]));
                email = Session["U_Email"] + "," + email;

                SendMAIL.SendMail("", email, "", body.ToString(), "Your SwissBake order no. " + dice + " has been placed");

                reader.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public ActionResult paynow()
        {

            if (Session["OrderID&Amount"] != null && Session["U_Id"] != null)
            {
                string test = Session["OrderID&Amount"].ToString();
                string[] Order = Session["OrderID&Amount"].ToString().Split(',');
                // orderCode|amt|custId|orderID
                OrdNo = Order[0].ToString();
                Amount = Order[1].ToString();
                Other = Order[2].ToString() + "," + Amount + "," + Order[3].ToString();
                // CustId|Amt|OrderId
                string str = Helper.url();
                // Response.Write(str + "<br>")
                //  str = HttpContext.Current.Request.Url.Host;
                //Success = "http://" + str + ":" + Helper.urlport().ToString() + "/paysuccess";
                //Fail = "http://" + str + ":" + Helper.urlport().ToString() + "/payfailure";
                Success = "https://www.swissbake.bh/paysuccess";
                Fail = "https://www.swissbake.bh/payfailure";
                ViewAddressBilling();
                // Button1_Click(Nothing, Nothing)
                TransferFund(); // payment()
                return Redirect(Session["url"].ToString());
            }
            else
            {
                return Redirect("/");
            }

        }
        public void TransferFund()
        {
            string strru = "";

            // // '''''''''''''''''''''''''''''///LIVE
            string strlogin = "41371";
            string strpass = "INVICING@123";
            string strreqHashKey = "de5b5268a0dc89c7f1";
            string strprodid = "INVOICING";
            strru = "https://www.swissbake.bh/paysuccess";
            // // '''''''''''''''''''''''''''''//ENDS


            ////  '''''''''''''''''''''''''''//LOCAL
            //string strlogin = "197";
            //string strpass = "Test@123";
            //string strreqHashKey = "KEY123657234";
            //string strprodid = "NSE";
            // strru = "http://localhost:50372/paysuccess";
            ////   '''''''''''''''''''''''''//ENDS


            string strResponse = "";
            string strURL = "";

            string strttype = "NBFundTransfer";

            string stramt = Amount;
            string strtxncurr = "INR";
            string strtxnid = OrdNo.Replace(" ", "");
            string strclientcode = "007";
            string strdatew = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string strdate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss").Replace(" ", "%20").Replace("-", "/"); // "29/10/2014 12:09:06"
            string strcustacc = "123456789";
            string strtxnamt = "0";
            string str = Helper.url();

            string reqsignature = "";

            string strsignature = strlogin + strpass + strttype + strprodid + strtxnid + stramt + strtxncurr;
            byte[] bytes = Encoding.UTF8.GetBytes(strreqHashKey);
            byte[] b = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
            reqsignature = byteToHexString(b).ToLower();
            // strURL = strURL.Replace("[signature]", reqsignature)
            strURL = strURL.Replace("[ru]", strru + "&");

            //// //  ''''''''''''''''''''''''''//Live
            strURL = "https://payment.atomtech.in/paynetz/epi/fts?login=" + strlogin + "&pass=" + strpass + "&ttype=" + strttype + "&prodid=" + strprodid + "&amt=" + stramt + "&txncurr=" + strtxncurr + "&txnscamt=0&clientcode=" + strclientcode + "&txnid=" + strtxnid + "&date=" + strdate + "&custacc=" + strcustacc + "&ru=" + strru + "&signature=" + reqsignature; // //For RU
                                                                                                                                                                                                                                                                                                                                                                            //////  '''''''''' '   strURL = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=" + strlogin + "&pass=" + strpass + "&ttype=" + strttype + "&prodid=" + strprodid + "&amt=" + stramt + "&txncurr=" + strtxncurr + "&txnscamt=0&clientcode=" + strclientcode + "&txnid=" + strtxnid + "&date=" + strdate + "&custacc=" + strcustacc + "&signature=" + reqsignature

            //////   '''''''''''''''''''''''''''''END


            //       /////      '''''''''''''''''''''''''''//LOCAL
            //       strURL = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=" + strlogin + "&pass=" + strpass + "&ttype=" + strttype + "&prodid=" + strprodid + "&amt=" + stramt + "&txncurr=" + strtxncurr + "&txnscamt=0&clientcode=" + strclientcode + "&txnid=" + strtxnid + "&date=" + strdate + "&custacc=" + strcustacc + "&ru=" + strru + "&signature=" + reqsignature;//For RU
            ////////       ''''''''''  strURL = "https://paynetzuat.atomtech.in/paynetz/epi/fts?login=" + strlogin + "&pass=" + strpass + "&ttype=" + strttype + "&prodid=" + strprodid + "&amt=" + stramt + "&txncurr=" + strtxncurr + "&txnscamt=0&clientcode=" + strclientcode + "&txnid=" + strtxnid + "&date=" + strdate + "&custacc=" + strcustacc + "&signature=" + reqsignature

            ////////        '''''''''''''''end


            strURL = strURL + "&udf1=udf11&udf2=udf12&udf3=udf13&udf9=udf19&udf4=udf14";
            strURL = strURL.Replace("udf11", custName.Replace("&", "and"));// Replace(strURL, "udf11", custName.Replace("&", "and"));
            strURL = strURL.Replace("udf12", custEmailId);
            strURL = strURL.Replace("udf13", custMobileNo);
            strURL = strURL.Replace("udf19", Other);
            strURL = strURL.Replace("udf14", Session["paymenttype"].ToString());
            strURL = strURL.Replace("udf5", custMobileNo);
            Session["url"] = strURL;
            // HttpContext.Current.Response.Redirect(strURL, false);
        }
        public static string byteToHexString(byte[] byData)
        {
            StringBuilder sb = new StringBuilder((byData.Length * 2));
            int i = 0;

            while ((i < byData.Length))
            {
                int v = (byData[i] & 255);

                if ((v < 16))
                    sb.Append('0');

                sb.Append(v.ToString("X"));
                i += 1;
            }

            return sb.ToString();
        }
        public void ViewAddressBilling()
        {
            try
            {
                DataSet dt = dbOt.ViewAddressByID("Billing", Convert.ToInt32(Session["U_Id"]));
                DataTable dtCustomer = new DataTable();
                dtCustomer = dt.Tables[0];

                DataRow drCustomerRow;
                drCustomerRow = dtCustomer.Rows[0];
                custAddress = BstarGeneral.GetStringVal(drCustomerRow["AccAddr_Address"].ToString()).ToString();
                custCity = BstarGeneral.GetStringVal(drCustomerRow["AccAddr_City"].ToString()).ToString();
                custPinCode = BstarGeneral.GetStringVal(drCustomerRow["AccAddr_Pincode"].ToString()).ToString();
                custCountry = BstarGeneral.GetStringVal(drCustomerRow["CountryName"].ToString()).ToString();
                custName = BstarGeneral.GetStringVal(drCustomerRow["AccAddr_Name"].ToString()).ToString();
                custEmailId = BstarGeneral.GetStringVal(drCustomerRow["AccAddr_Email"].ToString()).ToString();
                custMobileNo = BstarGeneral.GetStringVal(drCustomerRow["Contact_Mobile"].ToString()).ToString();
                custState = BstarGeneral.GetStringVal(drCustomerRow["StateName"].ToString()).ToString();
                custPhone = "24944101";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private string orderno;
        private int PayId;
        public ActionResult paysuccess()
        {
            // new''' 
            if (Request.Form.Get("F_code").ToString().ToLower() == "ok")
            {
                var keys = Request.Form;
                //// Dim payment1 As String() = Request.Form().ToString.Split("&")
                //string otherdetails1 = keys[13].ToString();
                //string otherdetails2 = keys[6].ToString();
                //string otherdetails3 = keys[10].ToString();
                //string otherdetails4 = keys[0].ToString();
                //string otherdetails5 = keys[2].ToString();
                //string otherdetails6 = keys[1].ToString();

                // string[] payment = Request.Form.ToString().Split('&');
                string direcpayreferenceid = Request.Form.Get("mmp_txn").ToString();// payment[10].ToString().Replace("ipg_txn_id =", "");
                // Dim responseflag As String = payment(1).ToString
                string otherdetails = Request.Form.Get("udf9").ToString();// keys[13].ToString();
                                                                          // string postingamount = payment[6].ToString();
                orderno = Request.Form.Get("mer_txn").ToString();
                string[] paid = otherdetails.Split(',');
                // 'CustId|Amt|OrderId
                string CustId = paid[0].ToString().Replace("udf9=", "");
                double Amount = Convert.ToDouble(paid[1].ToString().Replace("2c", ""));
                string merchantorderno = paid[2].ToString().Replace("2c", "");
                // If responseflag = "SUCCESS" Then
                // PaymentBLL.InsertSwiss(CustId, Amount, Date.Now, direcpayreferenceid, 0, merchantorderno)
                Session["U_Id"] = CustId;
                Session["paymenttype"] = Request.Form.Get("udf4").ToString();
                Session["U_Name"] = Request.Form.Get("udf1").ToString();
                Session["U_Email"] = Request.Form.Get("udf2").ToString();
                if (Session["paymenttype"].ToString() == "order")
                {
                    dbPayt.OnlinePaymentInsert(Convert.ToInt32(CustId), Amount, "Online", DateTime.Now, direcpayreferenceid, Convert.ToInt32(merchantorderno));
                    // SendUsingSMPP.SendSMS("Dear " + SmsBLL.CustomerName(CustId).Replace(" ", "_") + ", We have received your payment of Rs. " + Amount.ToString("N2") + " against order " + orderno + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.", SmsBLL.CustomerNo(CustId))
                    SendSMS.Send(dbAt.CustomerNo(CustId), "To " + Session["U_Name"].ToString() + ", We have received your payment of Rs. " + Amount.ToString("N2") + " against order " + orderno + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.");

                    SendSMS.Send("9702044786", "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " against order " + orderno + " by Online/CC transaction no " + direcpayreferenceid + ".");
                    SendSMS.Send(dbAt.UserNoFromCust(Convert.ToInt32(Session["U_Id"])), "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " against order " + orderno + " by Online/CC transaction no " + direcpayreferenceid + ".");

                    RegistrationMail(Session["U_Name"].ToString(), DateTime.Now.ToShortDateString(), Amount.ToString("N2"), direcpayreferenceid);
                }
                else if (Session["paymenttype"].ToString() == "payinvoice")
                {
                    PayId = dbPayt.Insert(Convert.ToInt32(CustId), System.Convert.ToDouble(Amount), "Online", DateTime.Now, "", direcpayreferenceid, Convert.ToInt32(CustId));
                    DataTable dtpay = (DataTable)Session["paymentrequest"];

                    // Dim type As Type = Session("paymentrequest").GetType()
                    // If type.Name = "DataTable" Then End If 
                    for (int i = 0; i <= dtpay.Rows.Count - 1; i++)
                    {
                        // Dim a As String = dtpay.Rows(i)("Payamount")
                        // Dim a1 As String = dtpay.Rows(i)("Invoice_ID")
                        // Dim a2 As String = dtpay.Rows(i)("Order_ID")
                        dbPayt.PayRefInsert(PayId, "Invoice", Convert.ToInt32(dtpay.Rows[i]["Invoice_ID"]), Convert.ToDouble(dtpay.Rows[i]["amt"]), Convert.ToInt32(Session["U_Id"]), Convert.ToInt32(dtpay.Rows[i]["Order_ID"]));
                    }
                    dbPayt.UpdatePaylinkStatus(Convert.ToInt32(dtpay.Rows[0]["Paylink_ID"]), Convert.ToInt32(Session["U_Id"]), System.Convert.ToDouble(Amount));
                    // SendUsingSMPP.SendSMS("Dear " + SmsBLL.CustomerName(CustId).Replace(" ", "_") + ", We have received your payment of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.", SmsBLL.CustomerNo(CustId))
                    SendSMS.Send(dbAt.CustomerNo(CustId), "To " + Session["U_Name"].ToString() + ", We have received your payment of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.");
                    SendSMS.Send("9702044786", "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ".");
                    SendSMS.Send(dbAt.UserNoFromCust(Convert.ToInt32(Session["U_Id"])), "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ".");
                    RegistrationMail(Session["U_Name"].ToString(), DateTime.Now.ToShortDateString(), Amount.ToString("N2"), direcpayreferenceid);
                }
                else if (Session["paymenttype"].ToString() == "paySelectinvoice")
                {
                    // PayId = PaymentBLL.Insert(CustId, System.Convert.ToDouble(Amount), "Online", DateTime.Now, "", direcpayreferenceid, CustId);
                    PayId = dbPayt.Insert(Convert.ToInt32(CustId), System.Convert.ToDouble(Amount), "Online", DateTime.Now, "", direcpayreferenceid, Convert.ToInt32(CustId));
                    DataTable dtpay = (DataTable)Session["paymentrequest"];

                    // Dim type As Type = Session("paymentrequest").GetType()
                    // If type.Name = "DataTable" Then End If 
                    for (int i = 0; i <= dtpay.Rows.Count - 1; i++)
                    {
                        // Dim a As String = dtpay.Rows(i)("Payamount")
                        // Dim a1 As String = dtpay.Rows(i)("Invoice_ID")
                        // Dim a2 As String = dtpay.Rows(i)("Order_ID")
                        dbPayt.PayRefInsert(PayId, "Invoice", Convert.ToInt32(dtpay.Rows[i]["Invoice_ID"]), Convert.ToDouble(dtpay.Rows[i]["amt"]), Convert.ToInt32(Session["U_Id"]), Convert.ToInt32(dtpay.Rows[i]["Order_ID"]));
                    }
                    // SendUsingSMPP.SendSMS("Dear " + SmsBLL.CustomerName(CustId).Replace(" ", "_") + ", We have received your payment of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.", SmsBLL.CustomerNo(CustId))
                    SendSMS.Send(dbAt.CustomerNo(CustId), "To " + Session["U_Name"].ToString() + ", We have received your payment of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.");
                    SendSMS.Send("9702044786", "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ".");
                    SendSMS.Send(dbAt.UserNoFromCust(Convert.ToInt32(Session["U_Id"])), "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ".");
                    RegistrationMail(Session["U_Name"].ToString(), DateTime.Now.ToShortDateString(), Amount.ToString("N2"), direcpayreferenceid);
                }
                else if (Session["paymenttype"].ToString() == "payorder")
                {
                    //PayId = PaymentBLL.Insert(CustId, System.Convert.ToDouble(Amount), "Online", DateTime.Now, "", direcpayreferenceid, CustId);
                    PayId = dbPayt.Insert(Convert.ToInt32(CustId), System.Convert.ToDouble(Amount), "Online", DateTime.Now, "", direcpayreferenceid, Convert.ToInt32(CustId));
                    DataTable dtpay = (DataTable)Session["paymentrequest"];

                    // Dim type As Type = Session("paymentrequest").GetType()
                    // If type.Name = "DataTable" Then End If 
                    for (int i = 0; i <= dtpay.Rows.Count - 1; i++)
                    {
                        // Dim a As String = dtpay.Rows(i)("Payamount")
                        // Dim a1 As String = dtpay.Rows(i)("Invoice_ID")
                        // Dim a2 As String = dtpay.Rows(i)("Order_ID")
                        //dbPayt.PayRefInsert(PayId, "Order", dtpay.Rows[i]["Order_ID"], dtpay.Rows[i]["Payamount"], Session["U_Id"], dtpay.Rows[i]["Order_ID"]);
                        dbPayt.PayRefInsert(PayId, "Order", Convert.ToInt32(dtpay.Rows[i]["Order_ID"]), Convert.ToDouble(dtpay.Rows[i]["amt"]), Convert.ToInt32(Session["U_Id"]), Convert.ToInt32(dtpay.Rows[i]["Order_ID"]));
                    }

                    dbPayt.UpdatePaylinkStatus(Convert.ToInt32(dtpay.Rows[0]["Paylink_ID"]), Convert.ToInt32(Session["U_Id"]), System.Convert.ToDouble(Amount));
                    // SendUsingSMPP.SendSMS("Dear " + SmsBLL.CustomerName(CustId).Replace(" ", "_") + ", We have received your payment of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.", SmsBLL.CustomerNo(CustId))
                    SendSMS.Send(dbAt.CustomerNo(CustId), "To " + Session["U_Name"].ToString() + ", We have received your payment of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.");
                    SendSMS.Send("9702044786", "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ".");
                    SendSMS.Send(dbAt.UserNoFromCust(Convert.ToInt32(Session["U_Id"])), "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ".");
                    RegistrationMail(Session["U_Name"].ToString(), DateTime.Now.ToShortDateString(), Amount.ToString("N2"), direcpayreferenceid);
                }
                else
                {
                    dbPayt.PaynowInsert(Convert.ToInt32(CustId), Amount, "Online", DateTime.Now, "", direcpayreferenceid, Convert.ToInt32(CustId));
                    // SendUsingSMPP.SendSMS("Dear " + SmsBLL.CustomerName(CustId).Replace(" ", "_") + ", We have received your payment of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.", SmsBLL.CustomerNo(CustId))
                    SendSMS.Send(dbAt.CustomerNo(CustId), "To " + Session["U_Name"].ToString() + ", We have received your payment of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ". Thank You.");
                    SendSMS.Send("9702044786", "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ".");
                    SendSMS.Send(dbAt.UserNoFromCust(Convert.ToInt32(Session["U_Id"])), "*PAYMENT* We have received payment from " + Session["U_Name"].ToString() + " of Rs. " + Amount.ToString("N2") + " by Online/CC transaction no " + direcpayreferenceid + ".");
                    RegistrationMail(Session["U_Name"].ToString(), DateTime.Now.ToShortDateString(), Amount.ToString("N2"), direcpayreferenceid);
                }
                return Redirect("/success");
            }
            else
            {
                return Redirect("/payfailure");
            }
        }
        public void RegistrationMail(string companyname, string date1, string amount, string refif)
        {
            string body = string.Empty;
            StreamReader reader = new StreamReader(Server.MapPath("~/Content/Mail/PaymentUpdate.html"));
            double amount1 = Convert.ToDouble(amount);
            body = reader.ReadToEnd();
            body = body.Replace("#Company", companyname);
            body = body.Replace("#Date", date1);
            body = body.Replace("#Amount", "BHD " + amount1.ToString("N2"));
            body = body.Replace("#Ref_Id", refif);
            string email = dbAt.Email(Convert.ToInt32(Session["U_Id"]));
            email = Session["U_Email"].ToString() + "," + email;
            //SendMailBLL.SendMail(email, body.ToString(), "We have received your payment");
            SendMAIL.SendMail("", email, "", body.ToString(), "We have received your payment");
            reader.Dispose();
        }
        public ActionResult payfailure()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult payfailure1()
        //{
        //    return View();
        //}
        public ActionResult success()
        {
            return View();
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Recipe()
        {
            DataSet ds = new DataSet();

            ds = dbrp.GetList();


            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Recipedet(string url)
        {
            DataSet ds = new DataSet();
            int id = 0;
            if (Session["U_Id"] != null)
            {
                id = Convert.ToInt32(Session["U_Id"]);
            }

            ds = dbrp.getRecipedet(url, id);

            return View(ds);
        }


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Cart()
        {

            if (Session["U_Id"] == null)
            {
                return Redirect("/login");
            }

            else
            {
                ViewBag.error = Session["error"];
                Session["error"] = null;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ds = dbdrop.FillDrop(17, "StateDropDown");    // 105 india default   17 bahrain

                dt = ds.Tables[0];

                IList<CommonStruct> State = new List<CommonStruct>();

                foreach (DataRow i in ds.Tables[0].Rows)
                {
                    State.Add(new CommonStruct()
                    {
                        Id = Convert.ToInt32(i["ID"]),
                        Name = Convert.ToString(i["StateName"])
                    });

                }

                ViewBag.sttlist = new SelectList(State, "Id", "Name");

                ds.Clear();
                dt.Clear();

                ds = dbdrop.FillDrop(Convert.ToInt32(Session["U_Id"]), "CountryDropDown");

                dt = ds.Tables[0];

                IList<CommonStruct> Country = new List<CommonStruct>();

                foreach (DataRow i in ds.Tables[0].Rows)
                {
                    Country.Add(new CommonStruct()
                    {
                        Id = Convert.ToInt32(i["Id"]),
                        Name = Convert.ToString(i["CountryName"])
                    });

                }

                ViewBag.cntrylist = new SelectList(Country, "Id", "Name", 17);



                DataSet dA = dbAt.AddressById(Convert.ToInt32(Session["U_Id"]), 0, "Billing", "GetPOBILLAddress");

                dt = ds.Tables[0];

                return View(dA);
            }
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Blog()
        {
            DataSet ds = new DataSet();

            ds = dbhome.GetBlogCatList("ViewBlogCat");
            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult BlogCat(string url, string search)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataSet ds2 = new DataSet();
            if (search != "")
            {
                ds = dbhome.GetBlogCat(search, "ViewBlogBySearch");
            }
            else
            {
                ds = dbhome.GetBlogCat(url, "ViewBlogByCat");
            }


            dt = ds.Tables[0];
            dt.TableName = "Tb1";
            ds2.Tables.Add(dt.Copy());
            dt.Clear();
            dt = ds.Tables[1];
            dt.TableName = "Tb2";
            ds2.Tables.Add(dt.Copy());
            ds.Clear();
            dt.Clear();

            ds = dbhome.GetBlogCatList("ViewBlogSideBar");
            dt = ds.Tables[0];
            dt.TableName = "Tb3";
            ds2.Tables.Add(dt.Copy());
            ds.Clear();
            dt.Clear();

            return View(ds2);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult BlogSearch(string search)
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataSet ds2 = new DataSet();

            ds = dbhome.GetBlogCat(search, "ViewBlogBySearch");

            dt = ds.Tables[0];
            dt.TableName = "Tb1";
            ds2.Tables.Add(dt.Copy());
            dt.Clear();
            dt = ds.Tables[1];
            dt.TableName = "Tb2";
            ds2.Tables.Add(dt.Copy());
            ds.Clear();
            dt.Clear();

            ds = dbhome.GetBlogCatList("ViewBlogSideBar");
            dt = ds.Tables[0];
            dt.TableName = "Tb3";
            ds2.Tables.Add(dt.Copy());
            ds.Clear();
            dt.Clear();

            return View();
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult BlogDetail(string url)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataSet ds2 = new DataSet();

            ds = dbhome.GetBlogCat(url, "ViewBlogDet");

            dt = ds.Tables[0];
            dt.TableName = "Tb1";
            ds2.Tables.Add(dt.Copy());
            dt.Clear();
            dt = ds.Tables[1];
            dt.TableName = "Tb2";
            ds2.Tables.Add(dt.Copy());
            dt.Clear();
            dt = ds.Tables[2];
            dt.TableName = "Tb3";
            ds2.Tables.Add(dt.Copy());
            ds.Clear();
            dt.Clear();

            ds = dbhome.GetBlogCatList("ViewBlogSideBar");
            dt = ds.Tables[0];
            dt.TableName = "Tb4";
            ds2.Tables.Add(dt.Copy());
            ds.Clear();
            dt.Clear();


            return View(ds2);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Faq()
        {
            DataSet ds = new DataSet();

            ds = dbhome.GetFaq();

            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public JsonResult FaqCat()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds = dbhome.GetFaq();

            dt = ds.Tables[1];

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

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public JsonResult FaqDet(string T)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds = dbhome.GetFaq();

            dt = ds.Tables[0];

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["FAQ_CatID"]) == Convert.ToInt32(T))
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);

                    }
                    rows.Add(row);
                }
            }

            //string js = JsonConvert.SerializeObject(Address);

            return Json(rows, JsonRequestBehavior.AllowGet);
        }


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Search(string value = "")
        {
            ViewBag.search = value;

            if (Request.Url.AbsolutePath.ToLower() == "/exclusive")
            {
                ViewBag.Title = "Exclusive";
            }
            else
            {
                ViewBag.Title = "Search";
                DataSet ds = new DataSet();
                value = value.TrimEnd('-').TrimStart('-').Replace("%20", " ").Replace("-", " ");
                ds = dbhome.GetBlogCat(value, "GlobalSearch");

                return View(ds);
            }



            return View();
        }

        //[Authenticate]
        public FileResult Brochure()
        {
            return File("/Content/images/document/SwissBakeBrochure.pdf", "application/pdf");
        }
        public ActionResult Detaildemo()
        {
            return View();
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Solution()
        {
            try
            {
                DataSet ds = new DataSet();

                ds = dbsol.GetList();

                return View(ds);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult SolDetail(string url)
        {
            try
            {

                //   DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                //  var yr = "";


                ds = dbsol.GetList(); //dbdrop.FillYearById(4, "Yeardropdown");

                //   dt = ds.Tables[0];
                DataRow[] results = ds.Tables[0].Select("PrdCategory_Seourl ='" + url + "'");
                ViewBag.mode = "getSolution";
                if (results.Length > 0)
                {

                    ViewBag.Title = results[0][1];
                    ViewBag.Catname = results[0][1];
                    ViewBag.Oneline = results[0]["ProdSolution_ShortDesc"];
                    ViewBag.Catdesc = results[0]["ProdSolution_LongDesc"];
                }

                IList<CommonStruct> category = new List<CommonStruct>();

                foreach (DataRow i in ds.Tables[0].Rows)
                {
                    category.Add(new CommonStruct()
                    {
                        PrdCategory_ID = Convert.ToInt32(i["ProdSolution_ID"]),
                        PrdCategory_Name = Convert.ToString(i["ProdSolution_Name"]),
                        PrdCategory_Seotitle = Convert.ToString(i["ProdSolution_Name"]),
                        PrdCategory_Seourl = Convert.ToString(i["PrdCategory_Seourl"]),
                        //Code = Convert.ToString(i["Order_Code"]),
                        //Datetime = Convert.ToString(i["Order_CreatedDate"]),
                        //Etcstring1 = Convert.ToString(i["Order_Amount"])
                    });

                }

                //ViewData["dualbanner"] = dt.AsEnumerable();
                ViewData["catli"] = category;


                // dt.Clear();
                ds.Clear();

                //var caturl  = url;

                //ViewBag.sltyear = null;

                //if (Request.Form["url"] != null)
                //{
                //    caturl = Request.Form["url"];
                //    ViewBag.sltyear = Request.Form["url"];
                //}

                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult TrackOrder(FormCollection form)
        {

            if (Session["U_Id"] != null)
            {
                return Redirect("/account/trackorder");
            }

            //DataTable dt = new DataTable();
            //DataSet ds = new DataSet();

            //var yr = "";


            //ds = dbdrop.FillYearById(4, "Invoicedropdownbyid");

            //dt = ds.Tables[0];

            //IList<CommonStruct> Year = new List<CommonStruct>();

            //foreach (DataRow i in ds.Tables[0].Rows)
            //{
            //    var x = 1;
            //    Year.Add(new CommonStruct()
            //    {
            //        Datetime = Convert.ToString(i["InvoiceYear"])
            //    });
            //    if (x == 1)
            //    {
            //        yr = Convert.ToString(i["InvoiceYear"]);
            //    }

            //}

            ////ViewData["dualbanner"] = dt.AsEnumerable();
            //ViewData["Year"] = Year;

            ViewData["Year"] = null;
            //dt.Clear();
            //ds.Clear();

            //var year = yr;

            //ViewBag.sltyear = null;

            //if (Request.Form["Year"] != null)
            //{
            //    year = Request.Form["Year"];
            //    ViewBag.sltyear = Request.Form["Year"];
            //}

            ViewBag.mode = "masterInvTrack";

            ViewBag.Title = "Track Order";

            ViewBag.access = "AwbTrack";


            return View("Myorder");
        }

        public ActionResult BakerSupport()
        {
            ViewBag.error = Session["error"];
            Session["error"] = null;

            return View();
        }

        [HttpPost]
        public ActionResult BakerSupport(FormCollection form)
        {
            try
            {
                Boolean tip = false, rec = false, sol = false;
                string msb = "", mssb;
                if (form["group-radio"] == "1")
                {
                    tip = true;
                    msb = "BAKING QUERY / TIP REQUIRED";
                    //msb = "<strong>BAKING QUERY / TIP REQUIRED</strong><br><br><table><tr><td style='width:20%;'>PRODUCT BEING BACKED</td><td>:</td><td style='text-align:left;'>" + form["val1"] + "</td></tr><tr><td style='width:20%;'>DESCRIBE THE QUERY / REQUIREMENT IN DETAIL</td><td>:</td><td style='text-align:left;'>" + form["val2"] + "</td></tr></table><br>";
                }

                if (form["group-radio"] == "2")
                {
                    rec = true;
                    msb = "PRODUCT DETAIL OR RECIPE";
                    //msb = "<strong>PRODUCT DETAIL OR RECIPE</strong><br><br><table><tr><td style='width:20%;'>SWISS BAKE PRODUCT NAME </td><td>:</td><td style='text-align:left;'>" + form["val1"] + "</td></tr><tr><td style='width:20%;'>DESCRIBE THE QUERY / REQUIREMENT IN DETAIL</td><td>:</td><td  style='text-align:left;'>" + form["val2"] + "</td></tr></table><br>";
                }

                if (form["group-radio"] == "3")
                {
                    sol = true;
                    msb = "NEW PROJECT OR CUSTOMIZED SOLUTION";
                    //msb = "<strong>NEW PROJECT OR CUSTOMIZED SOLUTION</strong><br><br><table><tr><td style='width:25%;'>DESCRIBE THE PROJECT OR REQUIRED SOLUTION IN DETAIL </td><td>:</td><td style='text-align:left;'>" + form["val1"] + "</td></tr><tr><td style='width:25%;'>ESTIMATED MONTHLY REQUIREMENT</td><td>:</td><td style='text-align:left;'>" + form["val2"] + "</td></tr><tr><td style='width:25%;'>TIME DURATION FOR US TO DEVELOP THE SOLUTION</td><td>:</td><td style='text-align:left;'>" + form["val3"] + "</td></tr></table><br>";
                }

                dbhome.InsertBakSup(0, form["Name"], form["Email"], form["Comp"], form["Cont"], "", form["Country"], false, tip, rec, sol, form["val1"], form["val2"], form["val3"], "InsertBakerSupport");


                string body = "";

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/BakingSupportRequest.html"));
                body = read.ReadToEnd();
                body = body.Replace("#NAME_CONTACT", form["Name"].ToString());
                body = body.Replace("#CONTACT_NAME", form["Name"].ToString());
                body = body.Replace("#CONTACT_NO", form["Cont"].ToString());
                body = body.Replace("#CONTACT_EMAIL", form["Email"].ToString());
                body = body.Replace("#COMPANY_NAME", form["Name"].ToString());
                body = body.Replace("#COUNTRY", form["Country"].ToString());
                body = body.Replace("#SUPPORT_AREA", msb);
                body = body.Replace("#PRODUCT_BAKED", form["val1"].ToString());
                body = body.Replace("#QUERY_DETAIL", form["val2"].ToString());

                //mssb = "<strong>CUSTOMER DETAILS:</strong><br><br><table><tr><td>NAME </td><td>:&nbsp;" + form["Name"] + "</td></tr><tr><td>COMAPNY NAME </td><td>:&nbsp;" + form["Comp"] + "</td></tr><tr><td>COUNTRY </td><td>:&nbsp;" + form["Country"] + "</td></tr><tr><td>EMAIL ID  </td><td>:&nbsp;" + form["Email"] + "</td></tr><tr><td>CONTACT NO.</td><td>:&nbsp;" + form["Cont"] + "</td></tr></table><br>";

                //mssb = mssb + "<br/>" + msb;
                SendMAIL.BakerDirectSendMail("", "", "", body, "New request for Baking Support from website | SwissBake®");



                body = "";

                StreamReader read1 = new StreamReader(Server.MapPath("~/Content/Mail/ExpertBaking.html"));
                body = read1.ReadToEnd();
                body = body.Replace("#NAME_CONTACT", form["Name"].ToString());
                body = body.Replace("#SUPPORT_AREA", msb);
                body = body.Replace("#PRODUCT_BAKED", form["val1"].ToString());
                body = body.Replace("#QUERY_DETAIL", form["val2"].ToString());

                SendMAIL.BakerSendMail(form["Name"], form["Email"], "", body, "We have received your request for baker support | SwissBake®");


                Session["error"] = "sucess()";

                return Redirect("/bakersupport");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }


        public ActionResult Payment()
        {
            try
            {
                init();
                DataSet ds = new DataSet();
                //HttpUtility.UrlEncode(Helper.Encrypt(i["Order_ID"].ToString()))
                //string id = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sD7Vvjs5hBsdi"]));
                //string custID = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["yapPmaMxSdtsuc"]));
                //string type = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sXcj7sdCvmePyt"]));
                ValidateUserLogin();

                if (Request.QueryString["sD7Vvjs5hBsdi"] != null)
                {
                    string checkexist = dbPayt.checkPaylinkPaid(Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sD7Vvjs5hBsdi"])));
                    if (checkexist == "Already Paid")
                    {
                        //show alert here
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Call", "alert('You have already paid through this Link !');window.location ='dashboard.aspx';", true);
                        Session["error"] = "sucess()";
                        return Redirect("/login");
                    }
                    else if (Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sXcj7sdCvmePyt"])) == "Invoice")
                    {
                        ViewBag.type = "invoice";
                        ds = fillinvoice();
                    }
                    else
                    {
                        ViewBag.type = "Order";

                        ds = fillOrder();
                    }
                }
                else
                {
                    ViewBag.type = "Selectinvoice";
                    ds = fillSelectinvoice();
                }




                //ViewBag.id = id + "- " + custID;
                //ViewBag.type = "Order";
                return View(ds);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        public DataSet fillSelectinvoice()
        {
            try
            {

                //string LinkID = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sD7Vvjs5hBsdi"]));
                //string id = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sD7Vvjs5hBsdi"]));
                //string custID = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["yapPmaMxSdtsuc"]));
                //string type = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sXcj7sdCvmePyt"]));

                string custID = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["yapPmaMxSdtsuc"]));


                DataSet dsPendingBal = dbPayt.ViewByID(Convert.ToInt32(custID), "FillallInvoicePayViewById");


                if (dsPendingBal.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Order_ID", typeof(int)), new DataColumn("Invoice_ID", typeof(int)), new DataColumn("Order_Code", typeof(string)), new DataColumn("Invoice_Date", typeof(string)), new DataColumn("Invoice_Number", typeof(string)), new DataColumn("Invoice_Amount", typeof(double)), new DataColumn("PayRef_Amount", typeof(double)), new DataColumn("Balance", typeof(double)), new DataColumn("amt", typeof(string)) });
                    for (int i = 0; i <= dsPendingBal.Tables[0].Rows.Count - 1; i++)
                    {

                        var ltOrder_ID = dsPendingBal.Tables[0].Rows[i]["Order_ID"];
                        var ltInvoice_ID = dsPendingBal.Tables[0].Rows[i]["Invoice_ID"];
                        var ltOrder_Code = dsPendingBal.Tables[0].Rows[i]["Order_Code"];
                        var ltInvoice_Date = dsPendingBal.Tables[0].Rows[i]["Invoice_Date"];
                        var ltInvoice_Number = dsPendingBal.Tables[0].Rows[i]["Invoice_Number"];
                        var ltInvoice_Amount = dsPendingBal.Tables[0].Rows[i]["Invoice_Amount"];
                        var lblpaidamt = dsPendingBal.Tables[0].Rows[i]["PayRef_Amount"];
                        var lblbal = dsPendingBal.Tables[0].Rows[i]["Balance"];
                        var txtscamount = dsPendingBal.Tables[0].Rows[i]["amt"];


                        dt.Rows.Add(ltOrder_ID, ltInvoice_ID, ltOrder_Code, ltInvoice_Date, ltInvoice_Number, ltInvoice_Amount, lblpaidamt, lblbal, lblbal);
                    }

                    Session["OrderID&Amount"] = dt;
                    DataSet finalds = new DataSet();
                    finalds.Tables.Add(dt);

                    return finalds;

                }
                else
                {
                    //show alert
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Call", "alert('No Record Found !')", true);
                }
                return null;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet fillinvoice()
        {
            try
            {
                string LinkID = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sD7Vvjs5hBsdi"]));
                //string id = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sD7Vvjs5hBsdi"]));
                //string custID = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["yapPmaMxSdtsuc"]));
                //string type = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sXcj7sdCvmePyt"]));

                DataSet dsPendingBal = dbPayt.SelectPendingBalance(Convert.ToInt32(Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["yapPmaMxSdtsuc"]))), "PaymentBalance");


                if (Convert.ToInt32(dsPendingBal.Tables[0].Rows[0][0]) > 0)
                {
                    if (Convert.ToInt32(dsPendingBal.Tables[0].Compute("sum(Balance)", string.Empty)) > 0)
                    {
                        DataSet maindt = dbPayt.viewpaylinkbyid("Paylinkbyid", Convert.ToInt32(LinkID));

                        if (Convert.ToDouble(maindt.Tables[0].Rows[0]["PayLink_Amount"]) > 0)
                        {
                            if (dsPendingBal.Tables[0].Rows.Count > 0)
                            {
                                double amount = 0.0;
                                DataTable dt = new DataTable();
                                amount = Convert.ToDouble(maindt.Tables[0].Rows[0]["PayLink_Amount"]);
                                dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Order_ID", typeof(int)), new DataColumn("Invoice_ID", typeof(int)), new DataColumn("Order_Code", typeof(string)), new DataColumn("Invoice_Date", typeof(string)), new DataColumn("Invoice_Number", typeof(string)), new DataColumn("Invoice_Amount", typeof(double)), new DataColumn("PayRef_Amount", typeof(double)), new DataColumn("Balance", typeof(double)), new DataColumn("amt", typeof(string)) });
                                for (int i = 0; i <= dsPendingBal.Tables[0].Rows.Count - 1; i++)
                                {

                                    var ltOrder_ID = dsPendingBal.Tables[0].Rows[i]["Order_ID"];
                                    var ltInvoice_ID = dsPendingBal.Tables[0].Rows[i]["Invoice_ID"];
                                    var ltOrder_Code = dsPendingBal.Tables[0].Rows[i]["Order_Code"];
                                    var ltInvoice_Date = dsPendingBal.Tables[0].Rows[i]["Invoice_Date"];
                                    var ltInvoice_Number = dsPendingBal.Tables[0].Rows[i]["Invoice_Number"];
                                    var ltInvoice_Amount = dsPendingBal.Tables[0].Rows[i]["Invoice_Amount"];
                                    var lblpaidamt = dsPendingBal.Tables[0].Rows[i]["PayRef_Amount"];
                                    var lblbal = dsPendingBal.Tables[0].Rows[i]["Balance"];
                                    var txtscamount = dsPendingBal.Tables[0].Rows[i]["amt"];

                                    if (amount > 0)
                                    {
                                        if (amount > Convert.ToDouble(lblbal))
                                        {
                                            dt.Rows.Add(ltOrder_ID, ltInvoice_ID, ltOrder_Code, ltInvoice_Date, ltInvoice_Number, ltInvoice_Amount, lblpaidamt, lblbal, lblbal);
                                            amount = amount - Convert.ToDouble(lblbal);
                                        }
                                        else
                                        {
                                            dt.Rows.Add(ltOrder_ID, ltInvoice_ID, ltOrder_Code, ltInvoice_Date, ltInvoice_Number, ltInvoice_Amount, lblpaidamt, lblbal, amount.ToString());
                                            amount = 0;
                                        }
                                    }
                                    else
                                        break;
                                }
                                DataSet finalds = new DataSet();
                                finalds.Tables.Add(dt);

                                return finalds;
                            }
                            else
                            {
                                //show alert
                                //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Call", "alert('No Record Found !')", true);
                            }

                        }
                        else
                        {
                            //show alert
                            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Call", "alert('Enter valid amount !')", true);
                        }
                    }
                }




                return null;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet fillOrder()
        {
            try
            {

                string LinkID = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sD7Vvjs5hBsdi"]));
                //string id = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sD7Vvjs5hBsdi"]));
                //string custID = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["yapPmaMxSdtsuc"]));
                //string type = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sXcj7sdCvmePyt"]));

                DataSet dsPendingBal = dbPayt.PendingBalbyorder(Convert.ToInt32(Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["yapPmaMxSdtsuc"]))), "PendingBalbyorder"); // AccountBLL.Accview(Session("CustId"))

                if (dsPendingBal.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToDouble(dsPendingBal.Tables[0].Rows[0]["Balance"]) > 0)
                    {
                        if (Convert.ToDouble(dsPendingBal.Tables[0].Compute("sum(Balance)", string.Empty)) > 0)
                        {
                            DataSet maindt = dbPayt.viewpaylinkbyid("Paylinkbyid", Convert.ToInt32(LinkID));

                            if (Convert.ToDouble(maindt.Tables[0].Rows[0]["PayLink_Amount"]) > 0)
                            {
                                if (dsPendingBal.Tables[0].Rows.Count > 0)
                                {
                                    double amount = 0.0;
                                    DataTable dt = new DataTable();
                                    amount = Convert.ToDouble(maindt.Tables[0].Rows[0]["PayLink_Amount"]);
                                    dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Order_ID", typeof(int)), new DataColumn("Invoice_ID", typeof(int)), new DataColumn("Order_Code", typeof(string)), new DataColumn("Order_ModifiedDate", typeof(string)), new DataColumn("Invoice_Number", typeof(string)), new DataColumn("Order_Amount", typeof(double)), new DataColumn("PayRef_Amount", typeof(double)), new DataColumn("Balance", typeof(double)), new DataColumn("amt", typeof(string)) });
                                    for (int i = 0; i <= dsPendingBal.Tables[0].Rows.Count - 1; i++)
                                    {
                                        var ltOrder_ID = dsPendingBal.Tables[0].Rows[i]["Order_ID"];
                                        var ltInvoice_ID = dsPendingBal.Tables[0].Rows[i]["Order_ID"];
                                        var ltOrder_Code = dsPendingBal.Tables[0].Rows[i]["Order_Code"];
                                        var ltOrder_Date = dsPendingBal.Tables[0].Rows[i]["Order_ModifiedDate"];
                                        var ltInvoice_Number = dsPendingBal.Tables[0].Rows[i]["Order_Code"];
                                        var ltOrder_Amount = dsPendingBal.Tables[0].Rows[i]["Order_Amount"];
                                        var lblpaidamt = dsPendingBal.Tables[0].Rows[i]["PayRef_Amount"];
                                        var lblbal = dsPendingBal.Tables[0].Rows[i]["Balance"];
                                        var txtscamount = dsPendingBal.Tables[0].Rows[i]["amt"];
                                        if (amount > 0)
                                        {
                                            if (amount > Convert.ToDouble(lblbal))
                                            {
                                                dt.Rows.Add(ltOrder_ID, 0, ltOrder_Code, ltOrder_Date, ltOrder_Code, ltOrder_Amount, lblpaidamt, lblbal, lblbal);
                                                amount = amount - Convert.ToDouble(lblbal);
                                            }
                                            else
                                            {
                                                dt.Rows.Add(ltOrder_ID, 0, ltOrder_Code, ltOrder_Date, ltOrder_Code, ltOrder_Amount, lblpaidamt, lblbal, amount.ToString());
                                                amount = 0;
                                            }
                                        }
                                        else
                                            break;
                                    }
                                    DataSet finalds = new DataSet();
                                    finalds.Tables.Add(dt);

                                    return finalds;
                                }
                                else
                                {
                                    //show alert
                                    // ScriptManager.RegisterStartupScript(Page, this.GetType(), "Call", "alert('No Record Found !')", true);
                                    //    Response.Redirect("/login");

                                }

                            }
                            else
                            {
                                //show alert
                                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Call", "alert('Enter valid amount !')", true);
                                //  Response.Redirect("/login");
                            }
                        }
                    }
                }
                else
                {
                    // return null;
                    // //show alert no record found
                    //GridOrderPay.DataSource = ds1;
                    //GridOrderPay.DataBind();
                    //btnPayNow.Visible = false;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void init()
        {
            if (Request.QueryString["sXcj7sdCvmePyt"] != null)
            {
                if (Request.QueryString["yapPmaMxSdtsuc"] != null)
                {
                }
                else
                {
                    Response.Redirect("/login");
                }
            }
            else
            {
                Response.Redirect("/login");
            }
        }
        public void ValidateUserLogin()
        {
            try
            {
                string custID = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["yapPmaMxSdtsuc"]));

                DataTable dtEmp = dbPayt.PaymentLogin(Convert.ToInt32(custID)).Tables[0];

                if (dtEmp.Rows.Count == 0)
                {
                    //thorw message here
                    //   ScriptManager.RegisterStartupScript(Page, this.GetType(), "Call", "alert('Invalid Login !');window.location ='login.aspx';", true);
                    Session["error"] = "sucess()";
                    Response.Redirect("/login");
                }
                else if (dtEmp.Rows[0]["Acc_Status"].ToString() == "Active")
                {

                    //make this active
                    //  Response.Cookies("vialink").Expires = DateTime.Now.AddDays(7);
                    // Response.Cookies("vialink").Value = "True";

                    Session["vialink"] = "True";
                    DataRow drLogin;
                    drLogin = dtEmp.Rows[0];
                    // Acc_Status
                    System.Web.HttpContext.Current.Session["U_Id"] = dtEmp.Rows[0][0];
                    System.Web.HttpContext.Current.Session["U_Name"] = dtEmp.Rows[0][3];
                    System.Web.HttpContext.Current.Session["U_Email"] = dtEmp.Rows[0][1];
                    System.Web.HttpContext.Current.Session["U_Verified"] = dtEmp.Rows[0][5];
                }
                else
                {
                    //show alert here
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Call", "alert('Your account is not active, Please contact customer care at +973 35376746')", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult Payment(FormCollection form)
        {
            //return View();
            //string xyx = 
            //  HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sXcj7sdCvmePyt"];

            //string type = Helper.Decrypt(HttpUtility.UrlDecode(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sXcj7sdCvmePyt"]));
            //string id = Helper.Decrypt(HttpUtility.UrlDecode(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sD7Vvjs5hBsdi"]));
            //string custID = Helper.Decrypt(HttpUtility.UrlDecode(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["yapPmaMxSdtsuc"]));

            //string id1 = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sD7Vvjs5hBsdi"]));
            //string custID1 = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["yapPmaMxSdtsuc"]));
            //string type1 = Helper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sXcj7sdCvmePyt"]));

            try
            {
                if (HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sD7Vvjs5hBsdi"] != null)
                {
                    if (Helper.Decrypt(HttpUtility.UrlDecode(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sXcj7sdCvmePyt"])) == "Invoice")
                    {
                        Session["paymenttype"] = "payinvoice";

                        DataSet maindt = dbPayt.viewpaylinkbyid("Paylinkbyid", Convert.ToInt32(Helper.Decrypt(HttpUtility.UrlDecode(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sD7Vvjs5hBsdi"]))));
                        if (Convert.ToDouble(maindt.Tables[0].Rows[0]["PayLink_Amount"]) > 0)
                        {
                            DataSet newds = new DataSet();
                            newds = fillinvoice();
                            if (newds.Tables[0].Rows.Count > 0)
                            {
                                DataTable dt = new DataTable();
                                dt = newds.Tables[0];
                                System.Data.DataColumn newColumn = new System.Data.DataColumn("Paylink_ID", typeof(System.String));
                                newColumn.DefaultValue = Helper.Decrypt(HttpUtility.UrlDecode(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sD7Vvjs5hBsdi"]));//"Your DropDownList value";
                                dt.Columns.Add(newColumn);

                                Session["paymentrequest"] = dt;
                            }
                        }

                        Random rnd = new Random();
                        int verfirycode = rnd.Next(10000, 999999);
                        string dice = "OD" + rnd.Next(10000, 999999).ToString() + " ";
                        Session["OrderID&Amount"] = dice + "," + Convert.ToDouble(maindt.Tables[0].Rows[0]["PayLink_Amount"]).ToString() + "," + Session["U_Id"].ToString() + "," + dice; // invoice id is order id added here by mandar for payment getway

                        Response.Redirect("/PayNow");
                    }
                    else
                    {
                        Session["paymenttype"] = "payorder";

                        DataSet maindt = dbPayt.viewpaylinkbyid("Paylinkbyid", Convert.ToInt32(Helper.Decrypt(HttpUtility.UrlDecode(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sD7Vvjs5hBsdi"]))));
                        if (Convert.ToDouble(maindt.Tables[0].Rows[0]["PayLink_Amount"]) > 0)
                        {
                            DataSet newds = new DataSet();
                            newds = fillOrder();
                            if (newds.Tables[0].Rows.Count > 0)
                            {
                                //double amount = 0.0;
                                DataTable dt = new DataTable();
                                dt = newds.Tables[0];
                                System.Data.DataColumn newColumn = new System.Data.DataColumn("Paylink_ID", typeof(System.String));
                                newColumn.DefaultValue = Helper.Decrypt(HttpUtility.UrlDecode(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sD7Vvjs5hBsdi"]));//"Your DropDownList value";
                                dt.Columns.Add(newColumn);


                                Session["paymentrequest"] = dt;
                            }
                        }
                        Random rnd = new Random();
                        int verfirycode = rnd.Next(10000, 999999);
                        string dice = "OD" + rnd.Next(10000, 999999).ToString() + " ";
                        var txid = Helper.Decrypt(HttpUtility.UrlDecode(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["sD7Vvjs5hBsdi"]));
                        Session["OrderID&Amount"] = dice + "," + Convert.ToDouble(maindt.Tables[0].Rows[0]["PayLink_Amount"]).ToString() + "," + Session["U_Id"].ToString() + "," + txid; // invoice id is order id added here by mandar for payment getway

                        Response.Redirect("/PayNow");
                    }
                }
                else
                {
                    Session["paymenttype"] = "paySelectinvoice";

                    DataSet newds = new DataSet();
                    newds = fillSelectinvoice();
                    if (newds.Tables[0].Rows.Count > 0)
                    {

                        DataTable dt = new DataTable();

                        dt.Columns.AddRange(new DataColumn[10] {
                                new DataColumn("Order_Code", typeof(string)),
                                new DataColumn("Order_ID", typeof(int)),
                                new DataColumn("Invoice_ID", typeof(int)),
                                new DataColumn("Invoice_Date", typeof(string)),
                                new DataColumn("Invoice_Number", typeof(string)),
                                new DataColumn("Invoice_Amount", typeof(double)),
                                new DataColumn("PayRef_Amount", typeof(double)),
                                new DataColumn("Balance", typeof(double)),
                                new DataColumn("amt", typeof(string)),
                                new DataColumn("Payamount", typeof(double))
                            });

                        double amount = 0.0;
                        foreach (var key in form.AllKeys)
                        {
                            //  var value = form[key];
                            if (form[key] == "True")
                            {

                                //int val1 = Convert.ToInt32(form[key.Replace("chk", "iv")]);

                                DataTable dsPendingBal = newds.Tables[0].AsEnumerable().Where(r => (r.Field<Int32>("Invoice_ID") == (Convert.ToInt32(form[key.Replace("chk", "iv")])))
                                ).CopyToDataTable();

                                if (dsPendingBal.Rows.Count > 0)
                                {

                                    for (int i = 0; i <= dsPendingBal.Rows.Count - 1; i++)
                                    {

                                        var ltOrder_ID = dsPendingBal.Rows[i]["Order_ID"];
                                        var ltInvoice_ID = dsPendingBal.Rows[i]["Invoice_ID"];
                                        var ltOrder_Code = dsPendingBal.Rows[i]["Order_Code"];
                                        var ltInvoice_Date = dsPendingBal.Rows[i]["Invoice_Date"];
                                        var ltInvoice_Number = dsPendingBal.Rows[i]["Invoice_Number"];
                                        var ltInvoice_Amount = dsPendingBal.Rows[i]["Invoice_Amount"];
                                        var lblpaidamt = dsPendingBal.Rows[i]["PayRef_Amount"];
                                        var lblbal = dsPendingBal.Rows[i]["Balance"];
                                        // var txtscamount = dsPendingBal.Tables[0].Rows[i]["amt"];
                                        dt.Rows.Add(ltOrder_Code, ltOrder_ID, ltInvoice_ID, ltInvoice_Date, ltInvoice_Number, ltInvoice_Amount, lblpaidamt, lblbal, lblbal, lblbal);
                                        amount = amount + Convert.ToDouble(lblbal);

                                    }
                                    //Session["paymentrequest"] = dt;
                                    //Session["OrderID&Amount"] = "OD12345 " + "," + Convert.ToDouble(amount).ToString() + "," + Session["U_Id"].ToString() + "," + "1";
                                    //Response.Redirect("/PayNow");
                                }

                            }

                        }
                        if (dt.Rows.Count > 0)
                        {
                            Session["paymentrequest"] = dt;
                            Session["OrderID&Amount"] = "OD12345 " + "," + Convert.ToDouble(amount).ToString() + "," + Session["U_Id"].ToString() + "," + "1";
                            Response.Redirect("/PayNow");
                        }
                        //foreach (var key in form.Keys)
                        //{
                        //    var value = form[key.ToString()];
                        //    // etc.
                        //}

                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        [HttpPost]
        public ActionResult SendtoHomebaker()
        {
            try
            {
                string body = "";

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/Homebaker.html"));
                body = read.ReadToEnd();
                body = body.Replace("#NAME_CONTACT", Request.Form["submit"].ToString());

                SendMAIL.DirectSendMail("", Request.Form["email"].ToString(), "", body, "We are unable to serve you at this time | SwissBake® B2B Portal");


                return Redirect("https://www.josefmarc.in");
            }
            catch (Exception ex)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        public ActionResult Export_stop()
        {
            try
            {
                ViewBag.error = Session["error"];
                Session["error"] = null;

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ds = dbdrop.FillDrop(Convert.ToInt32(Session["U_Id"]), "CountryDropDown");

                dt = ds.Tables[0];

                IList<CommonStruct> Country = new List<CommonStruct>();

                foreach (DataRow i in ds.Tables[0].Rows)
                {
                    Country.Add(new CommonStruct()
                    {
                        Id = Convert.ToInt32(i["Id"]),
                        Name = Convert.ToString(i["CountryName"])
                    });

                }

                ViewBag.cntrylist = new SelectList(Country, "Name", "Name", "Bahrain");
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        //[CaptchaValidator]
        public ActionResult Export_stop(FormCollection form)   //zyn 10cot10
        {
            try
            {

                ////if (captchaValid == true)
                ////{

                //string res = dbhome.InsertExport(form["Organisation"].ToString(), form["Type"].ToString(), form["Turnover"].ToString(), form["Name"].ToString(), form["Designation"].ToString(), form["Mobile"].ToString(), form["Email"].ToString(), form["Country"].ToString(), form["City"].ToString(), form["Pin"].ToString(), form["InterestFor"].ToString(), form["Comment"].ToString());

                //string body = "";

                //StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/Export.html"));
                //body = read.ReadToEnd();
                //body = body.Replace("#NAME_CONTACT", form["Name"]);

                //SendMAIL.SendMail("", form["Email"], "", body, "We are unable to serve you at this time | SwissBake® B2B Portal");

                ////read.Dispose();

                ////body = "<strong>CUSTOMER DETAIL:</strong><br><br><table><tr><td>NAME </td><td>:&nbsp;" + form["Name"] + "</td></tr><tr><td>EMAIL  </td><td>:&nbsp;" + form["Email"] + "</td></tr><tr><td>ORGANISATION </td><td>:&nbsp;" + form["Organisation"] + "</td></tr><tr><td>TELEPHONE NUMBER </td><td>:&nbsp;" + form["Mobile"] + "</td></tr><tr><td>MESSAGE </td><td>:&nbsp;" + form["Comment"] + "</td></tr><tr><td>CITY </td><td>:&nbsp;" + form["City"] + "</td></tr></table><br>";

                ////SendMAIL.SendMail("", "", "", body, "Contact Detail | Swiss Bake Ingredients Pvt. Ltd.");

                //form.Clear();


                //Session["error"] = "sucess()";
                ////}
                ////else
                ////{
                ////    ViewBag.captcha = "This is required";
                ////}


                return Redirect("/export");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Bakertools()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public JsonResult Closepopup()
        {
            try
            {
                Session["nopopup"] = "hide";
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public ActionResult Trackingdetails()
        {
            return View();
        }


        public ActionResult Thankyou()
        {
            return View();
            
        }


        [HttpPost]
        public ActionResult Index4Contact(FormCollection form) //zyn 10oct23
        {
            try
            {
				if (!string.IsNullOrEmpty(form["Mobile"].ToString().Trim())) {
				   //
                string res = dbhome.InsertExport(form["Organisation"].ToString(), "", "", form["Name"].ToString(), "", form["Mobile"].ToString(), form["Email"].ToString(), "", "", "", "", form["Comment"].ToString());

                string body = "";

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/Export.html"));
                body = read.ReadToEnd();
                body = body.Replace("#NAME_CONTACT", form["Name"]);

                SendMAIL.SendMail(form["Name"], form["Email"], "", body, "We are unable to serve you at this time | SwissBake® B2B Portal");

				}

                form.Clear();

                Session["error"] = "sucess()";



                return Redirect("/");
            }
            catch (Exception ex)
            {
                return View("index4");
            }
        }


        /// </summary>

        /// <param name="input">Input string</param>

        /// <param name="languagePair">2 letter Language Pair, delimited by "|".

        /// E.g. "ar|en" language pair means to translate from Arabic to English</param>

        /// <returns>Translated to String</returns>

        //public static string Translate(string input, string languagePair)
        //{

        //    string url = String.Format("https://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);

        //    WebClient webClient = new WebClient();

        //    webClient.Encoding = System.Text.Encoding.UTF8;

        //    string result = webClient.DownloadString(url);

        //    result = result.Substring(result.IndexOf("id=result_box") + 22, result.IndexOf("id=result_box") + 500);

        //    result = result.Substring(0, result.IndexOf("</div"));

        //    return result;

        //}
    }

}