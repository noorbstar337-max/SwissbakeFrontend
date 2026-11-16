
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;

using System.Text;
using prolib;
using SwissbakeIn.App_Start;

namespace SwissbakeIn.Controllers
{
    public class FeedbackController : Controller
    {
        Feedback dbFeedback = new Feedback();
        // GET: Feedback
        [HttpGet]
        public ActionResult Index(string feedbackid, string AgentId, string ActivtyID)
        {
            ViewBag.feedbackId = "nongrayscale" + feedbackid;
            ViewBag.AgentId = Encryption.Decrypt(AgentId.Replace(" ", "+"));
           // ViewBag.AgentId = Encryption.Decrypt(AgentId);
            ViewBag.ActivtyID = Encryption.Decrypt(ActivtyID.Replace(" ", "+"));
           // ViewBag.ActivtyID = Encryption.Decrypt(ActivtyID);
           
            Session["Typeid"] = feedbackid;
            Session["AgentId"] = Encryption.Decrypt(AgentId.Replace(" ", "+"));
            Session["ActivtyID"] = Encryption.Decrypt(ActivtyID.Replace(" ", "+"));
            //Session["AgentId"] = 7900;
            //Session["ActivtyID"] = 793;
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))// This is give the connectionStrings on SqlConnection class’ Construrctor
                {
                    var cmd = new SqlCommand("Proc_tblRating", con);// Give The command to SqlCommand  
                    var id = Session["ActivtyID"];
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar)).Value = "CheckExist";//Pass the parameter
                    cmd.Parameters.Add("@Rating_ActivityId", SqlDbType.Int).Value = Session["ActivtyID"];
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        object Activityid = cmd.ExecuteScalar();
                        bool CheckStatus = Convert.ToBoolean(Activityid);
                        if (CheckStatus == true)
                        {
                          dbFeedback.Rating(Convert.ToInt32(Session["AgentId"]),  Session["ActivtyID"].ToString(), 0, 0, 0, "", "", Convert.ToInt32(Session["AgentId"]), "", "");
                        }
                        else
                        {
                           // dbFeedback.Rating(1, "4141", 0, 0, 0, "", "", 1, "", "");
                        }
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try 
            {
              using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))// This is give the connectionStrings on SqlConnection class’ Construrctor
                {
                    var cmd = new SqlCommand("Proc_tblRating", con);// Give The command to SqlCommand  
                    var id =Session["ActivtyID"];
                     cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar)).Value = "CheckStatus";//Pass the parameter
                    cmd.Parameters.Add("@Rating_ActivityId", SqlDbType.Int).Value = Session["ActivtyID"];
                    try
                    {
                       if (con.State != ConnectionState.Open)
                            con.Open();
                        object Activityid = cmd.ExecuteScalar();

                        bool CheckStatus = Convert.ToBoolean(Activityid);

                        if (CheckStatus == false)
                        {
                            return View();
                        }
                        else
                        {
                            return new RedirectResult("~/Email/Rating.html");
                        }
                    }
                   finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
             
              try
                {
                    Update(form);
                    SendMail();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return new RedirectResult("~/Email/SuccessfulRating.html");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(FormCollection form)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))// This is give the connectionStrings on SqlConnection class’ Construrctor

                {
                    // tblRating Rating = new tblRating();

                    var cmd = new SqlCommand("Proc_tblRating", con);// Give The command to SqlCommand  

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar)).Value = "UpdateRating";//Pass the parameter

                    cmd.Parameters.Add(new SqlParameter("@Rating_TypeId", SqlDbType.VarChar)).Value = (string)Session["Typeid"];//Pass the parameter

                    cmd.Parameters.Add(new SqlParameter("@Rating_Status", SqlDbType.Bit)).Value = "true";//Pass the parameter

                    cmd.Parameters.Add(new SqlParameter("@Rating_ActivityId", SqlDbType.VarChar)).Value = (string)Session["ActivtyID"];//Pass the parameter

                    cmd.Parameters.Add(new SqlParameter("@Rating_AgentId", SqlDbType.VarChar)).Value = (string)Session["AgentId"]; //Pass the parameter

                    cmd.Parameters.Add(new SqlParameter("@Rating_ModifyDate", SqlDbType.DateTime)).Value = DateTime.Now; //Pass the parameter

                    cmd.Parameters.Add(new SqlParameter("@Rating_Listening", SqlDbType.Int)).Value = form["stars"];//Pass the parameter

                    cmd.Parameters.Add(new SqlParameter("@Rating_Knowledge", SqlDbType.Int)).Value = form["stars1"];//Pass the parameter

                    cmd.Parameters.Add(new SqlParameter("@Rating_Understandability", SqlDbType.Int)).Value = form["stars2"];//Pass the parameter

                    cmd.Parameters.Add(new SqlParameter("@Rating_Remark", SqlDbType.VarChar)).Value = form["Remark"];//Pass the parameter

                    Session["Remark"] = form["Remark"];

                    var Typeid = (string)Session["Typeid"];
                    if(Typeid=="1") //checking for bad Rating..
                    {
                        cmd.Parameters.Add(new SqlParameter("@Rating_BadStatus", SqlDbType.VarChar)).Value = "Pending";//Pass the parameter
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Rating_BadStatus", SqlDbType.VarChar)).Value = "Ok";//Pass the parameter
                    }
                    try

                    {

                        if (con.State != ConnectionState.Open)

                            con.Open();

                        cmd.ExecuteNonQuery();
                    }

                    finally

                    {
                        if (con.State != ConnectionState.Closed)

                            con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SendMail()
        {
            string Rating_Listening = "";
            string AgentEmail = "";
            string Rating_Knowledge = "";
            string Rating_Understandability = "";
            string Sales_Man = "";
            string Contact_Name = "";
            string Meeting_Or_Call = "";
            string Meeting_Date = "";
            string FinalRating = "";
            string Account_Name = "";
                
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))// This is give the connectionStrings on SqlConnection class’ Construrctor
                {
                    var cmd = new SqlCommand("Proc_tblRating", con);// Give The command to SqlCommand  
                     var id = (string)Session["ActivtyID"];
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.VarChar)).Value = "EmailSend";//Pass the parameter
                    cmd.Parameters.Add("@Rating_ActivityId", SqlDbType.Int).Value = (string)Session["ActivtyID"];
                    //(string)Session["ActivtyID"]  4113;
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();
                         cmd.ExecuteScalar();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                             da.Fill(dt);
                            cmd.Parameters.Clear();
                            DataRow row;
                            if(dt.Rows.Count >0)
                            {
                                row = dt.Rows[0];
                                AgentEmail = row["Email"].ToString();
                                Rating_Listening = row["Rating_Listening"].ToString();
                                Rating_Knowledge = row["Rating_Knowledge"].ToString();
                                Rating_Understandability = row["Rating_Understandability"].ToString();
                                Sales_Man = row["Sales_Man"].ToString();
                                Contact_Name = row["Contact_Name"].ToString();
                                Meeting_Or_Call= row["Meeting_Or_Call"].ToString();
                                Meeting_Date= row["Meeting_Date"].ToString();
                                string Remark= (string)Session["Remark"];
                                if (Remark=="")
                                {
                                    FinalRating = row["FinalRating"].ToString();
                                }
                                else
                                {
                                    FinalRating = (string)Session["Remark"];
                                }
                                Account_Name= row["Account_Name"].ToString();
                                if (Rating_Listening=="1")
                                    {
                                    Rating_Listening = "★☆☆☆☆";
                                }else if(Rating_Listening == "2")
                                {
                                    Rating_Listening = "★★☆☆☆";
                                }
                                else if (Rating_Listening == "3")
                                {
                                    Rating_Listening = "★★★☆☆";
                                }
                                else if (Rating_Listening == "4")
                                {
                                    Rating_Listening = "★★★★☆";
                                }
                                else if (Rating_Listening == "5")
                                {
                                    Rating_Listening = "★★★★★";
                                }
                                else
                                {
                                    Rating_Listening = "☆☆☆☆☆";
                                }
                                //===============================================================
                                if (Rating_Knowledge == "1")
                                {
                                    Rating_Knowledge = "★☆☆☆☆";
                                }
                                else if (Rating_Knowledge == "2")
                                {
                                    Rating_Knowledge = "★★☆☆☆";
                                }
                                else if (Rating_Knowledge == "3")
                                {
                                    Rating_Knowledge = "★★★☆☆";
                                }
                                else if (Rating_Knowledge == "4")
                                {
                                    Rating_Knowledge = "★★★★☆";
                                }
                                else if (Rating_Knowledge == "5")
                                {
                                    Rating_Knowledge = "★★★★★";
                                }
                                else
                                {
                                    Rating_Knowledge = "☆☆☆☆☆";
                                }

                                //if (Rating_Knowledge == "1")
                                //{
                                //    Rating_Knowledge = "☆";
                                //}
                                //else if (Rating_Knowledge == "2")
                                //{
                                //    Rating_Knowledge = "☆☆";
                                //}
                                //else if (Rating_Knowledge == "3")
                                //{
                                //    Rating_Knowledge = "☆☆☆";
                                //}
                                //else if (Rating_Knowledge == "4")
                                //{
                                //    Rating_Knowledge = "☆☆☆☆";
                                //}
                                //else if (Rating_Knowledge == "5")
                                //{
                                //    Rating_Knowledge = "☆☆☆☆☆";
                                //}
                                //else
                                //{
                                //    Rating_Knowledge = "";
                                //}
                                //===================================================================
                                if (Rating_Understandability == "1")
                                {
                                    Rating_Understandability = "★☆☆☆☆";
                                }
                                else if (Rating_Understandability == "2")
                                {
                                    Rating_Understandability = "★★☆☆☆";
                                }
                                else if (Rating_Understandability == "3")
                                {
                                    Rating_Understandability = "★★★☆☆";
                                }
                                else if (Rating_Understandability == "4")
                                {
                                    Rating_Understandability = "★★★★☆";
                                }
                                else if (Rating_Understandability == "5")
                                {
                                    Rating_Understandability = "★★★★★";
                                }
                                else
                                {
                                    Rating_Understandability = "☆☆☆☆☆";
                                }
                                 
                            }
                        }
                    }
                    finally

                    {
                       if (con.State != ConnectionState.Closed)
                            con.Close();
                    }
                    string body = string.Empty;
                    StreamReader reader = new StreamReader(Server.MapPath("~/Email/SalesMan_Review.html"));
                    body = reader.ReadToEnd();
                    body = body.Replace("#SALES_MAN", Sales_Man);
                    body = body.Replace("#CONTACT_NAME", Contact_Name);
                    body = body.Replace("#MEETING", Meeting_Or_Call);
                    body = body.Replace("#DATE", Meeting_Date);
                    body = body.Replace("#COMPANY", Account_Name);
                    body = body.Replace("#FEEDBACK", FinalRating);
                    body = body.Replace("#Communication", Rating_Listening);
                    body = body.Replace("#Attitude", Rating_Knowledge);
                    body = body.Replace("#Product", Rating_Understandability);
                     SendMAIL.FeedbackMail(AgentEmail, body, "Your Activity review");

                    Account dbaccount = new Account();
                    SendSMS.Send(dbaccount.CustomerNo(Session["AgentId"].ToString()), "Dear " + Sales_Man + ", the Customer " + Contact_Name + " from " + Account_Name + " has shared his feedback on your meeting with him on " + Meeting_Date + ". Kindly check Feedback in your CRM.");

                    
                    // SendMAIL.FeedbackMail("vikassingh@acaira.in", body, "Your Activity review");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
