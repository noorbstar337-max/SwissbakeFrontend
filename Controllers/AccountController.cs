using SwissbakeIn.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prolib;
using System.Data;
//using SwissbakeIn.Models;
using SwissbakeIn.Entities;
using SwissbakeIn.Controllers;
using System.Dynamic;
using reCAPTCHA.MVC;
using System.IO;
using System.Web.Http;

namespace SwissbakeIn.Controllers
{
    [Authenticate]
    [BuildCart]

    public class AccountController : Controller
    {
       
        Checkexist dbcheck = new Checkexist();
        Account dbaccount = new Account();
        Dropdown dbdrop = new Dropdown();
        Home dbhome = new Home();
        Recipe dbrecipe = new Recipe();
        Complaint dbcomplaint = new Complaint();

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        [System.Web.Http.Route("Account/index")]
        public ActionResult Index()
        {

            DataSet ds,ds2 = new DataSet();
            DataTable dt = new DataTable();

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
            if (Session["EmailVerifyMessage"] != null)
            {
                ViewBag.mess = Session["EmailVerifyMessage"].ToString();
                Session["EmailVerifyMessage"] = null;
            }


            ds2 = dbaccount.GetDashSubsById(Convert.ToInt32(Session["U_Id"]));
            dt = ds2.Tables[0];
            dt.TableName = "Tb2";
            ds.Tables.Add(dt.Copy());
            dt.Clear();
            dt = ds2.Tables[1];
            dt.TableName = "Tb3";
            ds.Tables.Add(dt.Copy());
            dt.Clear();
            dt = ds2.Tables[2];
            dt.TableName = "Tb4";
            ds.Tables.Add(dt.Copy());
            ds2.Clear();
            dt = ds2.Tables[3];
            dt.TableName = "Tb5";
            ds.Tables.Add(dt.Copy());
            ds2.Clear();

            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Myprofile(FormCollection form)
        {

            try
            {
                if (form.Count > 0)
                {
                    int Pro1 = dbaccount.UpdateProfile(form["Companyname"], form["storename"], Convert.ToInt32(Session["U_Id"]));
                    int intDesg = 0;
                    try
                    {
                        intDesg = Convert.ToInt32(form["desig"]);
                    }
                    catch (Exception ex)
                    {
                    }

                    int Pro2 = dbaccount.UpdateProfileCont(form["fullname"],form["mob"], intDesg, form["telephone"], Convert.ToInt32(Session["U_Id"]));

                    int Bcontid = dbaccount.InsertAccAdd(Convert.ToInt32(Session["U_Id"]), "Billing", form["BillingName"], form["Email"], form["Mobile"], 17, Convert.ToInt32(form["State"]), form["City"], form["pinCode"], form["StreetName"], form["gst"], "UpdateAccAddress",0);

                    int tax = dbaccount.UpdateTaxAcc(form["pan"], form["gst"], Convert.ToInt32(Session["U_Id"]));

                    string body = "";

                    StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/profileupdate.html"));
                    body = read.ReadToEnd();
                    body = body.Replace("#NAME", Session["U_Name"].ToString());

                    SendMAIL.DirectSendMail("", Session["U_Email"].ToString(), "", body, "Profile has been updated | SwissBake® B2B Portal");
                    
                    ViewBag.error = "sucess()";


                }



                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();
                DataTable dt = new DataTable();

                //Filling Data

                ds = dbaccount.ProfileById(Convert.ToInt32(Session["U_Id"]), "SwissViewById");

                dt = ds.Tables[0];
                dt.TableName = "Tb1";
                if (dt.Rows.Count <= 0)
                {
                    dt.NewRow();
                }
                ds3.Tables.Add(dt.Copy());
                

                ds.Clear();
                dt.Clear();

                ds = dbaccount.ProfileById(Convert.ToInt32(Session["U_Id"]), "SwissContactViewById");

                dt = ds.Tables[0];
                dt.TableName = "Tb2";
                if (dt.Rows.Count <= 0)
                {
                    dt.NewRow();
                }
                ds3.Tables.Add(dt.Copy());
                


                ds = dbaccount.AddressById(Convert.ToInt32(Session["U_Id"]),0, "Billing", "FillBillAddress");
                dt = ds.Tables[0];
                dt.TableName = "Tb3";

                int AccAddrsCntryId = 0;

                try
                {
                    AccAddrsCntryId = Convert.ToInt32(dt.Rows[0]["AccAddr_Country"]);
                }
                catch (Exception ex)
                { }


                if (dt.Rows.Count <= 0)
                {
                    var dtcol = dt.Columns.Count;
                    var cells = new object[dtcol];
                    for (var i = 0; i < dtcol; i++)
                    {
                        if (i == 0 || i == 1 || i == 11 || i == 12 || i == 15 || i == 17 || i == 18)
                        {
                            cells[i] = 0;
                        }
                        else if (i == 14 || i == 16)
                        {
                            cells[i] = DateTime.Now;
                        }
                        else
                        {
                            cells[i] = "";
                        }
                        
                    }

                    dt.Rows.Add(cells);
                }
                ds3.Tables.Add(dt.Copy());

                ds.Clear();
                dt.Clear();


                ds = dbaccount.GetAccDetById(Convert.ToInt32(Session["U_Id"]), "Taxation_ViewById");

                dt = ds.Tables[0];
                dt.TableName = "Tb4";
                if (dt.Rows.Count <= 0)
                {
                    dt.NewRow();
                }
                ds3.Tables.Add(dt.Copy());

                //Filling Dropdown


                ds2 = dbdrop.FillDrop(Convert.ToInt32(Session["U_Id"]), "DesignationDropDown");
                
                dt = ds2.Tables[0];

                IList<CommonStruct> Desig = new List<CommonStruct>();

                foreach (DataRow i in ds2.Tables[0].Rows)
                {
                    Desig.Add(new CommonStruct()
                    {
                        Id = Convert.ToInt32(i["Desig_Id"]),
                        Name = Convert.ToString(i["Desig_Name"])
                    });

                }

                if (ds3.Tables[1].Rows.Count > 0)
                {
                    ViewBag.desiglist = new SelectList(Desig, "Id", "Name", Convert.ToString(ds3.Tables[1].Rows[0].ItemArray[1]));
                }
                else
                {
                    ViewBag.desiglist = new SelectList(Desig, "Id", "Name");
                }

                
                
                ds2.Clear();
                dt.Clear();

                ds2 = dbdrop.FillDrop(Convert.ToInt32(Session["U_Id"]), "CountryDropDown");

                dt = ds2.Tables[0];

                IList<CommonStruct> Country = new List<CommonStruct>();

                foreach (DataRow i in ds2.Tables[0].Rows)
                {
                    Country.Add(new CommonStruct()
                    {
                        Id = Convert.ToInt32(i["Id"]),
                        Name = Convert.ToString(i["CountryName"])
                    });

                }

                ViewBag.cntrylist = new SelectList(Country, "Id", "Name", AccAddrsCntryId);  //  17 for bahrain zayan 07sept23 105 india

                ds2.Clear();
                dt.Clear();

                ds2 = dbdrop.FillDrop(17, "StateDropDown"); // 105 for india

                dt = ds2.Tables[0];

                IList<CommonStruct> State = new List<CommonStruct>();

                foreach (DataRow i in ds2.Tables[0].Rows)
                {
                    State.Add(new CommonStruct()
                    {
                        Id = Convert.ToInt32(i["ID"]),
                        Name = Convert.ToString(i["StateName"])
                    });

                }

                if (ds3.Tables[2].Rows.Count > 0)
                {
                    ViewBag.sttlist = new SelectList(State, "Id", "Name", Convert.ToString(ds3.Tables[2].Rows[0].ItemArray[11]));
                }
                else
                {
                    ViewBag.sttlist = new SelectList(State, "Id", "Name");
                }

                

                ds2.Clear();
                dt.Clear();

                DataSet dsbanner = new DataSet();
                // DataTable dt = new DataTable();



                dsbanner = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
                dt = dsbanner.Tables[0];
                dt.TableName = "TbBanner";
                ds3.Tables.Add(dt.Copy());
                dsbanner.Clear();


                return View(ds3);
            }
            catch (Exception ex)
            {
                return View(ex);
            }


            
        }

        static void Updatepro()
        {

        }




        //[HttpPost]
        //public ActionResult Myprofile(FormCollection form)
        //{
        //    return View();
        //}


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Address()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds = dbdrop.FillDrop(17, "StateDropDown");
            //ds = dbdrop.FillDrop(105, "StateDropDown");

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

            ViewBag.cntrylist = new SelectList(Country, "Id", "Name", 17); // zayan 07sept23 105

            ds.Clear();

            
            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
            DataSet dsbanner = new DataSet();
            // DataTable dt = new DataTable();



            dsbanner = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
            dt = dsbanner.Tables[0];
            dt.TableName = "TbBanner";
            ds.Tables.Add(dt.Copy());
            dsbanner.Clear();


            return View(ds);
        }


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Myorder(FormCollection form)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Yeardropdown");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["orderyear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["orderyear"]);
                }
                
            }
            
            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "ViewOrderByMemberID";
            ViewBag.Title = "All Orders";
            ViewBag.oneLdesc = "This section shows you the history of all your previous orders";
            ViewBag.access = "Myorder";
            ViewBag.link = "order";
            ViewBag.Act1 = "active";
            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult CancelOrder(FormCollection form)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Yeardropdown");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["orderyear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["orderyear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "ViewOrderByMemberID_Cancel";
            ViewBag.Title = "Cancelled Orders";
            ViewBag.oneLdesc = "You can view details related to your cancelled orders from here";
            ViewBag.access = "Myorder";
            ViewBag.link = "order";
            ViewBag.Act2 = "active";
            return View("Myorder",ds);
        }


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult PendingOrder(FormCollection form)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Yeardropdown");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["orderyear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["orderyear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "ViewOrderByMemberID_Pending";

            ViewBag.Title = "Pending Orders";
            ViewBag.oneLdesc = "You can view details related to your pending orders from here";
            ViewBag.access = "Myorder";
            ViewBag.link = "order";
            ViewBag.Act3 = "active";
            return View("Myorder",ds);
        }


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult CompletedOrder(FormCollection form)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Yeardropdown");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["orderyear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["orderyear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "ViewOrderByMemberID_Completed";

            ViewBag.Title = "Completed Orders";
            ViewBag.oneLdesc = "You can view all your past successfully completed orders from here";
            ViewBag.access = "Myorder";
            ViewBag.link = "order";
            ViewBag.Act4 = "active";
            return View("Myorder",ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Reorder(FormCollection form)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Yeardropdown");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["orderyear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["orderyear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "ViewOrderByMemberID_Completed";
            ViewBag.Title = "Re-orders";
            ViewBag.oneLdesc = "You can re-order an earlier completed ordered directly from this section. This is best suited if you repeatedly use the same products";
            ViewBag.access = "Myorder";
            ViewBag.link = "order";
            ViewBag.Act5 = "active";
            return View(ds);
        }



        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Invoice(FormCollection form)
        {
            DataTable dt = new DataTable();
            DataSet ds, ds2 = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Invoicedropdownbyid");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["InvoiceYear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["InvoiceYear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }
            
            ds2 = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "MyInvoiceGridview";

            ViewBag.Title = "All Invoices";
            ViewBag.oneLdesc = "This section shows you the history of all your previous Invoices";
            ViewBag.access = "Invoice";
            ViewBag.link = "invoice";
            ViewBag.act1 = "active";

            return View("Myorder",ds2);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult CancelInvoice(FormCollection form)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Invoicedropdownbyid");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["InvoiceYear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["InvoiceYear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "MyInvoiceGridview_cancel";

            ViewBag.Title = "Cancelled Invoice";
            ViewBag.oneLdesc = "This section shows you the list of all cancelled invoices";
            ViewBag.access = "Invoice";
            ViewBag.link = "invoice";
            ViewBag.act2 = "active";

            return View("Myorder",ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult PendingInvoice(FormCollection form)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Invoicedropdownbyid");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["InvoiceYear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["InvoiceYear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "MyInvoiceGridview_InProcess";

            ViewBag.Title = "Pending Invoice";
            ViewBag.oneLdesc = "Here you can view all the invoices which are in process & to be dispatched";
            ViewBag.access = "Invoice";
            ViewBag.link = "invoice";
            ViewBag.act3 = "active";

            return View("Myorder",ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult DispatchInvoice(FormCollection form)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Invoicedropdownbyid");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["InvoiceYear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["InvoiceYear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "MyInvoiceGridview_Dispatch";

            ViewBag.Title = "Dispatched Invoice";
            ViewBag.oneLdesc = "You can view all the invoices against which materials have been dispatched but not yet delivered";
            ViewBag.access = "Invoice";
            ViewBag.link = "invoice";
            ViewBag.act4 = "active";

            return View("Myorder",ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult DeliveredInvoice(FormCollection form)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Invoicedropdownbyid");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["InvoiceYear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["InvoiceYear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;


            dt.Clear();
            ds.Clear();

            var year = yr;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ViewBag.mode = "MyInvoiceGridview_delivered";

            ViewBag.Title = "Delivered Invoice";
            ViewBag.oneLdesc = "This section will show you all your past invoices against which deliveries have been successfully completed";
            ViewBag.access = "Invoice";
            ViewBag.link = "invoice";
            ViewBag.act5 = "active";

            return View("Myorder",ds);
        }


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult TrackOrder(FormCollection form)
        {
            //DataTable dt = new DataTable();
            DataSet ds = new DataSet();

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

            ViewBag.mode = "TrackingGridview";

            ViewBag.Title = "Track My Order";
            ViewBag.oneLdesc = "You can track the delivery status of your shipments from this section.";

            ViewBag.access = "Track";

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            return View("Myorder",ds);
        }


        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult MyPayment(FormCollection form)
        {
            DataTable dt = new DataTable();
            DataSet ds, ds2 = new DataSet();

            var yr = "";


            ds = dbdrop.FillYearById(Convert.ToInt32(Session["U_Id"]), "Paymentdropdown");

            dt = ds.Tables[0];

            IList<CommonStruct> Year = new List<CommonStruct>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                var x = 1;
                Year.Add(new CommonStruct()
                {
                    Datetime = Convert.ToString(i["PaymentYear"])
                });
                if (x == 1)
                {
                    yr = Convert.ToString(i["PaymentYear"]);
                }

            }

            //ViewData["dualbanner"] = dt.AsEnumerable();
            ViewData["Year"] = Year;
            
            dt.Clear();
            ds.Clear();

            var year = Year[0].Datetime;

            ViewBag.sltyear = null;

            if (Request.Form["Year"] != null)
            {
                year = Request.Form["Year"];
                ViewBag.sltyear = Request.Form["Year"];
            }
            
            ds = dbaccount.FillPaymentById(Convert.ToInt32(Session["U_Id"]), year, "MyFillPayment");

            ds2 = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
            dt = ds2.Tables[0];
            dt.TableName = "Tb2";
            ds.Tables.Add(dt.Copy());
            dt.Clear();
            //dt = ds.Tables[0];

            //IList<CommonStruct> Payment = new List<CommonStruct>();

            //foreach (DataRow i in ds.Tables[0].Rows)
            //{
            //    Payment.Add(new CommonStruct()
            //    {
            //        Id = Convert.ToInt32(i["Order_ID"]),
            //        Url = Convert.ToString(i["href"]),
            //        Freight = Convert.ToString(i["Order_Freight"]),
            //        Status = Convert.ToString(i["Order_OrderStatus"]),
            //        Code = Convert.ToString(i["otherinfo"]),
            //        Datetime = Convert.ToString(i["Payment_Date"]),
            //        Etcstring1 = Convert.ToString(i["Payment_Amount"])
            //    });
            //}

            //ViewData["Payment"] = Payment;

            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        [System.Web.Mvc.HttpGet]
        public ActionResult Gensupport()
        {
            DataSet ds,ds2 = new DataSet();
            DataTable dt = new DataTable();


            ViewBag.error = Session["error"];
            Session["error"] = null;
            ViewBag.act1 = "selected";

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
            ds2 = dbaccount.FillTicketCount(Convert.ToInt32(Session["U_Id"]));

            dt = ds2.Tables[0];
            dt.TableName = "Tb2";
            ds.Tables.Add(dt.Copy());
            ds2.Clear();


            return View(ds);
        }

        [System.Web.Http.HttpPost]
        public ActionResult Gensupport(FormCollection form, HttpPostedFileBase file)
        {
            int ID = Convert.ToInt32(Session["U_Id"]);
            var allowedExtensions = new[] { ".jpg", ".xlsx", ".docx", ".pdf", ".doc" };
            var Image_url = "";
            if (file != null)
            {
                Image_url = file.ToString(); //getting complete url  
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + name + "-" + Session["U_Id"].ToString() + ext; //appending the name with id  
                                                                                // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("~/Content/images/document/"), myfile);
                    Image_url = "/Content/images/document/" + myfile;
                    file.SaveAs(path);

                }
            }


            int compid = dbaccount.InsertGensupport(0, ID, form["subject"], form["desc"], Image_url);

            DataSet ds, ds2 = new DataSet();

            ds = dbaccount.GetTicket(compid);

            //ds2 = dbaccount.ProfileById(Convert.ToInt32(Session["U_Id"]), "SwissViewById");

            string body = "";

            StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/complaint.html"));
            body = read.ReadToEnd();
            body = body.Replace("#NAME", Session["U_Name"].ToString());
            body = body.Replace("#TICKET_NO", compid.ToString());
            body = body.Replace("#TYPE", "General Support");
            body = body.Replace("#SUPPORT_DESCRIPTION", form["desc"].ToString());
            if (Image_url != "")
            {
                body = body.Replace("#YES_NO", "Yes");
            }
            else
            {
                body = body.Replace("#YES_NO", "No");
            }

            string Oemail = dbaccount.Email(Convert.ToInt32(Session["U_Id"]));
            string email = dbaccount.CustEmail(Convert.ToInt32(Session["U_Id"]));

            if (email == "")
            {
                email = Oemail + "," + email;
            }
            else
            {
                email = email + "," + Oemail;
            }

            SendMAIL.SendMail("", email, "", body, "New support ticket "+ compid +" raised | SwissBake® B2B");
            SendSMS.Send(dbaccount.CustomerNo(Convert.ToString(Session["U_Id"])), "To " + Session["U_Name"].ToString() + ", We have received your concerns against ticket " + compid + " and will get back to you within 3 working days. Please check for updates by login into your account at www.swissbake.bh.");
            SendSMS.Send(dbaccount.AdminNo(), "*NEW COMPLAINT* Ticked no " + compid + " has been raised by " + Session["U_Name"].ToString() + " for " + form["subject"].Replace(" ", "_") + ".");
            SendSMS.Send(dbaccount.CustomerNo(Convert.ToString(Session["U_Id"])), "*NEW COMPLAINT* Hi " + dbaccount.FillManager(Convert.ToInt32(Session["U_Id"])).Tables[0].Rows[0][0].ToString().Replace(" ", "_") + ", Your customer " + Session["U_Name"].ToString().Replace(" ", "_") + " has raised a new complaint for " + form["subject"].Replace(" ", "_") + ". Please check Ticket no " + compid + " for more details.");


            Session["error"] = "sucess()";
            return Redirect("/Account/Gensupport");
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        [System.Web.Mvc.HttpGet]
        public ActionResult Feedback()
        {
            DataSet ds,ds2 = new DataSet();
            DataTable dt = new DataTable();

            ds2 = dbdrop.FillDrop(0, "DepartmentDropDown");

            dt = ds2.Tables[0];

            IList<CommonStruct> Depart = new List<CommonStruct>();

            foreach (DataRow i in ds2.Tables[0].Rows)
            {
                Depart.Add(new CommonStruct()
                {
                    Id = Convert.ToInt32(i["Deprt_Id"]),
                    Name = Convert.ToString(i["Deprt_Name"])
                });

            }

            if (ds2.Tables[0].Rows.Count > 0)
            {
                ViewBag.departlist = new SelectList(Depart, "Id", "Name", Convert.ToString(ds2.Tables[0].Rows[0].ItemArray[1]));
            }
            else
            {
                ViewBag.departlist = new SelectList(Depart, "Id", "Name");
            }

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            ds2 = dbaccount.FillTicketCount(Convert.ToInt32(Session["U_Id"]));

            dt = ds2.Tables[0];
            dt.TableName = "Tb2";
            ds.Tables.Add(dt.Copy());
            ds2.Clear();


            ViewBag.error = Session["error"];
            Session["error"] = null;
            ViewBag.act2 = "selected";
            return View(ds);
        }

        [System.Web.Http.HttpPost]
        public ActionResult Feedback(FormCollection form, HttpPostedFileBase file)
        {
            int ID = Convert.ToInt32(Session["U_Id"]);
            var allowedExtensions = new[] { ".jpg", ".xlsx", ".docx", ".pdf", ".doc" };
            var Image_url = "";
            if (file != null)
            {
                Image_url = file.ToString(); //getting complete url  
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + fileName + "-" + Session["U_Id"].ToString() + ext; //appending the name with id  
                                                                                   // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("~/Content/images/document/"), myfile);
                    Image_url = "/Content/images/document/" + myfile;
                    file.SaveAs(path);

                }
            }


            int compid = dbaccount.InsertGensupport(Convert.ToInt32(form["dept"]), ID, form["subject"], form["desc"], Image_url);

            string body = "";

            StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/complaint.html"));
            body = read.ReadToEnd();
            body = body.Replace("#NAME", Session["U_Name"].ToString());
            body = body.Replace("#TICKET_NO", compid.ToString());
            body = body.Replace("#TYPE", "Feedback & Complaint");
            body = body.Replace("#SUPPORT_DESCRIPTION", form["desc"].ToString());
            if (Image_url != "")
            {
                body = body.Replace("#YES_NO", "Yes");
            }
            else
            {
                body = body.Replace("#YES_NO", "No");
            }
            string Oemail = dbaccount.Email(Convert.ToInt32(Session["U_Id"]));
            string email = dbaccount.CustEmail(Convert.ToInt32(Session["U_Id"]));

            if (email == "")
            {
                email = Oemail + "," + email;
            }
            else
            {
                email = email + "," + Oemail;
            }

            SendMAIL.SendMail("", email, "", body, "New support ticket " + compid + " raised | SwissBake® B2B");


            Session["error"] = "sucess()";
            return Redirect("/Account/Feedback");
        }
        

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Casehistory()
        {
            ViewBag.error = Session["error"];
            Session["error"] = null;
            DataSet ds,ds2 = new DataSet();
            DataTable dt = new DataTable();

            ds = dbaccount.FillTicket(Convert.ToInt32(Session["U_Id"]));
            ds2 = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));

            dt = ds2.Tables[0];
            dt.TableName = "Tb2";
            ds.Tables.Add(dt.Copy());
            ds2.Clear();

            ViewBag.act3 = "selected";
            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult TicketView()
        {
            return Redirect("/account/casehistory");
        }

        [System.Web.Http.HttpPost]
        public ActionResult SupportTicketview(FormCollection form, HttpPostedFileBase file, int Tid = 0)
        {
            ViewBag.error = Session["error"];
            Session["error"] = null;
            
            if (form.Keys.Count > 1)
            {

                int ID = Convert.ToInt32(Session["U_Id"]);
                var allowedExtensions = new[] { ".jpg", ".xlsx", ".docx", ".pdf", ".doc" };
                int f1 = Convert.ToInt32(form["RId"]);
                var Image_url = "";


                if (file != null)
                {
                    Image_url = file.ToString(); //getting complete url  
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = "Tkt-" + f1 + "-" + form["Id"].ToString() + ext; //appending the name with id  
                                                                                         // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Content/images/document/"), myfile);
                        //if (System.IO.File.Exists(Server.MapPath("~" + form["docmg1"].ToString())))
                        //{
                        //    System.IO.File.Delete(Server.MapPath("~" + form["docmg1"].ToString()));
                        //}
                        Image_url = "/Content/images/document/" + myfile;
                        file.SaveAs(path);
                        
                    }
                    else
                    {
                        Image_url = "";
                    }
                    
                }

                dbcomplaint.RInsert(Convert.ToInt32(form["Id"]), Convert.ToInt32(Session["U_Id"]), "Customer", form["Desc"], Image_url);
                DataSet ds, ds2 = new DataSet();
                DataTable dt = new DataTable();

                ds = dbaccount.GetAccDetById(Convert.ToInt32(Session["U_Id"]), "getContactDetailById");
                ds2 = dbaccount.FillTicketByID(Convert.ToInt32(form["Id"]));
                string body = "";

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/complaintresponse.html"));
                body = read.ReadToEnd();
                body = body.Replace("#COMPANY_NAME", Session["U_Name"].ToString());
                body = body.Replace("#NAME", ds.Tables[0].Rows[0]["Contact_ContactPerson"].ToString());
                body = body.Replace("#CONTACT_NO", ds.Tables[0].Rows[0]["Contact_Mobile"].ToString());
                body = body.Replace("#TICKET_NO", form["Id"].ToString());
                body = body.Replace("#TYPE", ds2.Tables[0].Rows[0]["Complaint_Subject"].ToString());
                body = body.Replace("#SUPPORT_DESCRIPTION", form["desc"].ToString());
                if (Image_url != "")
                {
                    body = body.Replace("#YES_NO", "Yes");
                }
                else
                {
                    body = body.Replace("#YES_NO", "No");
                }
                string Oemail = dbaccount.Email(Convert.ToInt32(Session["U_Id"]));
                string email = dbaccount.CustEmail(Convert.ToInt32(Session["U_Id"]));

                if (email == "")
                {
                    email = Oemail + "," + email;
                }
                else
                {
                    email = email + "," + Oemail;
                }

                SendMAIL.SendMail("", email, "", body, "New communication on support ticket "+ form["Id"].ToString() + " raised by "+ Session["U_Name"].ToString() + " | SwissBake® B2B");

                ds.Clear();
                ds2.Clear();

                ds = dbaccount.FillTicketByID(Convert.ToInt32(form["Id"]));
                ds2 = dbaccount.FillReplyByID(Convert.ToInt32(form["Id"]));
                dt = ds2.Tables[0];
                dt.TableName = "Tb4";
                ds.Tables.Add(dt.Copy());
                ds2.Clear();
                ViewBag.error = "sucess()";

                return View(ds);

            }
            else if (Tid != 0)
            {
                DataSet ds, ds2 = new DataSet();
                DataTable dt = new DataTable();

                ds = dbaccount.FillTicketByID(Tid);
                ds2 = dbaccount.FillReplyByID(Tid);
                dt = ds2.Tables[0];
                dt.TableName = "Tb4";
                ds.Tables.Add(dt.Copy());
                ds2.Clear();
                return View(ds);
            }
            if (Tid == 0)
            {
                return Redirect("/account/casehistory");
            }



            return Redirect("/account/casehistory");
        }



        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Changepassword(FormCollection form)
        {
            DataSet ds = new DataSet();
            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
          
            if (form.Keys.Count > 0)
            {
                int res = dbaccount.UpdatePassword(form["oldpassword"], form["password"], Convert.ToInt32(Session["U_Id"]));

                string body = "";

                StreamReader read = new StreamReader(Server.MapPath("~/Content/Mail/changepass.html"));
                body = read.ReadToEnd();
                body = body.Replace("#NAME", Session["U_Name"].ToString());

                SendMAIL.DirectSendMail("", Session["U_Email"].ToString(), "", body, "Password has been updated | SwissBake® B2B Portal");


                if (res == -1)
                {
                    ViewBag.error = "sucess()";
                }
            }

            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        [System.Web.Mvc.HttpGet]
        public ActionResult Kyc()
        {

            ViewBag.error = Session["error"];
            Session["error"] = null;

            //DataSet ds = new DataSet();
            DataSet ds, ds2 = new DataSet();
            DataTable dt = new DataTable();

            ds = dbaccount.GetKYCDocument("KYC", Convert.ToInt32(Session["U_Id"]));
            ds2 = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
            dt = ds2.Tables[0];
            dt.TableName = "Tb2";
            ds.Tables.Add(dt.Copy());
            ds2.Clear();
            if (ds.Tables[0].Rows.Count == 0)
            {
                //for (var i = 1; i <= 3; i++)
                //{
                //    dbaccount.InsertKyc("KYC-"+i.ToString(), "", "", "KYC", Convert.ToInt32(Session["U_Id"]));
                //}

                dbaccount.InsertKyc("PHOTO ID", "", "", "KYC", Convert.ToInt32(Session["U_Id"]));
                dbaccount.InsertKyc("COMPANY REGISTRATION", "", "", "KYC", Convert.ToInt32(Session["U_Id"]));
                dbaccount.InsertKyc("ADDRESS PROOF OF THE OWNER", "", "", "KYC", Convert.ToInt32(Session["U_Id"]));

                ds.Clear();
                ds = dbaccount.GetKYCDocument("KYC", Convert.ToInt32(Session["U_Id"]));
               
            }

            //foreach (DataRow i in ds.Tables[0].Rows)
            //{
            //    string fullPath = Request.MapPath("~" + i["Doc_Path"].ToString());
            //    if (System.IO.File.Exists(fullPath))
            //    {
            //        System.IO.File.Delete(fullPath);
            //    }
            //}
            return View(ds);
        }

        [System.Web.Http.HttpPost]
        public ActionResult Kyc(FormCollection form, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {

            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();


                //ds = dbaccount.GetKYCDocument("KYC", Convert.ToInt32(Session["U_Id"]));

                //foreach (DataRow i in ds.Tables[0].Rows)
                //{
                //    string fullPath = Request.MapPath("~" + i["Doc_Path"].ToString());
                //    if (System.IO.File.Exists(fullPath))
                //    {
                //        System.IO.File.Delete(fullPath);
                //    }
                //}

                int ID = Convert.ToInt32(Session["U_Id"]);
                var allowedExtensions = new[] { ".jpg", ".xlsx", ".docx", ".pdf", ".doc" };
                int f1 = Convert.ToInt32(form["doc1"]);
                int f2 = Convert.ToInt32(form["doc2"]);
                int f3 = Convert.ToInt32(form["doc3"]);



                if (file1 != null)
                {
                    var Image_url = file1.ToString(); //getting complete url  
                    var fileName = Path.GetFileName(file1.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file1.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = "PHOTO ID-" + Session["U_Id"].ToString() + ext; //appending the name with id  
                                                                                    // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Content/images/document/"), myfile);
                        if (System.IO.File.Exists(Server.MapPath("~"+form["docmg1"].ToString())))
                        {
                            System.IO.File.Delete(Server.MapPath("~" + form["docmg1"].ToString()));
                        }
                        Image_url = "/Content/images/document/" + myfile;
                        file1.SaveAs(path);
                        dbaccount.UpdateKyc(Convert.ToInt32(form["doc1"]), Image_url, ext, ID);
                        //if (f1 != 0)
                        //{
                        //    dbaccount.UpdateKyc(Convert.ToInt32(form["doc1"]), Image_url, ext, ID);
                        //}
                        //else
                        //{
                        //    dbaccount.InsertKyc("KYC-1", Image_url, ext, "KYC", ID);
                        //}


                    }
                }


                if(file2 != null)
                {
                    var Image_url2 = file2.ToString(); //getting complete url  
                    var fileName2 = Path.GetFileName(file2.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext2 = Path.GetExtension(file2.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext2)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName2); //getting file name without extension  
                        string myfile = "COMPANY REGISTRATION-" + Session["U_Id"].ToString() + ext2; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Content/images/document/"), myfile);
                        if (System.IO.File.Exists(Server.MapPath("~" + form["docmg2"].ToString())))
                        {
                            System.IO.File.Delete(Server.MapPath("~" + form["docmg2"].ToString()));
                        }
                        Image_url2 = "/Content/images/document/" + myfile;
                        file2.SaveAs(path);
                        dbaccount.UpdateKyc(Convert.ToInt32(form["doc2"]), Image_url2, ext2, ID);
                        //if (form["doc2"] != null || form["doc2"] != "")
                        //{
                        //    dbaccount.UpdateKyc(Convert.ToInt32(form["doc2"]), Image_url2, ext2, ID);
                        //}
                        //else
                        //{
                        //    dbaccount.InsertKyc("KYC-2", Image_url2, ext2, "KYC", ID);
                        //}
                    }
                }

                if (file3 != null)
                {
                    var Image_url3 = file3.ToString(); //getting complete url  
                    var fileName3 = Path.GetFileName(file3.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext3 = Path.GetExtension(file3.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext3)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName3); //getting file name without extension  
                        string myfile = "ADDRESS PROOF OF THE OWNER-" + Session["U_Id"].ToString() + ext3; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Content/images/document/"), myfile);
                        if (System.IO.File.Exists(Server.MapPath("~" + form["docmg3"].ToString())))
                        {
                            System.IO.File.Delete(Server.MapPath("~" + form["docmg3"].ToString()));
                        }
                        Image_url3 = "/Content/images/document/" + myfile;
                        file3.SaveAs(path);
                        dbaccount.UpdateKyc(Convert.ToInt32(form["doc3"]), Image_url3, ext3, ID);
                        //if (form["doc3"] != null || form["doc3"] != "")
                        //{
                        //    dbaccount.UpdateKyc(Convert.ToInt32(form["doc3"]), Image_url3, ext3, ID);
                        //}
                        //else
                        //{
                        //    dbaccount.InsertKyc("KYC-3", Image_url3, ext3, "KYC", ID);
                        //}
                    }
                }


                //int res = dbaccount.DelKyc(Convert.ToInt32(Session["U_Id"]));

                //for ()
                //{

                //}
                Session["error"] = "sucess()";
                return Redirect("/Account/Kyc");

            }
            catch(Exception ex)
            {
                return Redirect("/Account/Kyc");
            }
            
        }

        //[ETag]
        //[WhitespaceFilter]
        //[GZipOrDeflate]
        //public ActionResult Quickorder()
        //{
        //    return View();
        //}

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Quickorder()
        {
            DataSet dsbanner = new DataSet();
            dsbanner = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
             
            return View("Quickorder2", dsbanner);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Paymenthistory()
        {
            return View();
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Notification()
        {
            DataSet ds, ds2 = new DataSet();
            DataTable dt = new DataTable();

            ds = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Reminders()
        {
            ViewBag.error = Session["error"];
            Session["error"] = null;

            DataSet ds, ds2 = new DataSet();
            DataTable dt = new DataTable();


            ds = dbaccount.GetSubsById(Convert.ToInt32(Session["U_Id"]));
            ds2 = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
            dt = ds2.Tables[0];
            dt.TableName = "Tb2";
            ds.Tables.Add(dt.Copy());
            ds2.Clear();


            return View(ds);
        }

        [ETag]
        [WhitespaceFilter]
        [GZipOrDeflate]
        public ActionResult Myrecipe()
        {
            DataSet ds,ds2 = new DataSet();
            DataTable dt = new DataTable();

            ds = dbrecipe.getRecipe(Convert.ToInt32(Session["U_Id"]));
            ds2 = dbaccount.GetCustBanner(Convert.ToInt32(Session["U_Id"]));
            dt = ds2.Tables[0];
            dt.TableName = "Tb2";
            ds.Tables.Add(dt.Copy());
            ds2.Clear();

            return View(ds);
        }
        //public ActionResult MyRecipe()
        //{

        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();

        //    ds = dbrecipe.getRecipe(Convert.ToInt32(Session["U_Id"]));

        //    return View(ds);
        //}



        public ActionResult Logout()
        {
            var rurl = System.Web.HttpContext.Current.Request.UrlReferrer;
            if (Session["logid"] != null) {
                dbaccount.updateTraillogout(Convert.ToInt32(Session["logid"]));
                dbaccount.Insertfronttrail( Convert.ToInt32(Session["logid"]), "/logout");
        }
            if (Request.Cookies["uslg"] != null)
            {
                var c = new HttpCookie("uslg");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return Redirect(rurl.ToString());

        }

        

    }
}
