using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.DataSetExtensions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using prolib;
using System.Data.SqlClient;
using SwissbakeIn;

namespace SwissbakeIn.App_Start
{
    public class BuildCart : ActionFilterAttribute
    {
        Account dbAt = new Account();
        Product dbPt = new Product();
        Cart dbCart = new Cart();
        public HttpCookie cartCookie;
        private SqlConnection _conn;
        private int sid;
        public string conn;
        int count;
        double PID, Amount, vatinr, vatper, rate, Quantity, Packing_Weight, TotalWeight, Product_MOQ;
        string Code, ProductName, ProdImg, unit, href, cookiePID, Product_linkas;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                DataTable dtsumcart = new DataTable();
                string cookieID, NewQuery;
                cookieID = HttpContext.Current.Request.Cookies["TheCart"].Value;
                // Dim cartCookie As HttpCookie
                // cartCookie.Value = ""
                // Response.Cookies.Add(cartCookie)
                if (HttpContext.Current.Session["U_Id"] != null)
                {
                    if (HttpContext.Current.Session["OrderProduct"] == null)
                    {
                        // logged in and order cart is not having before login
                        //HttpContext.Current.Session["Update"].ToString() == "True"
                        if (HttpContext.Current.Session["Update"] != null )
                        {
                        }
                        else
                        {
                            //   DataTable dtcart = dbCart.ViewByID(Convert.ToInt32(HttpContext.Current.Session["U_Id"]), "getcartbyID").Tables[0];
                            // cartCookie = HttpContext.Current.Request.Cookies["TheCart"]

                            if ((((HttpContext.Current.Request.Cookies["TheCart"]) != null)))
                            {
                                cookieID = HttpContext.Current.Request.Cookies["TheCart"].Value;

                                if (cookieID == "" | cookieID == "NULL")
                                    cookieID = "NULL";
                                else
                                {
                                    cookieID = cookieID.Replace(",", "/7.3,");
                                    string str = cookieID.Substring(cookieID.Length - 1);
                                    if (str == ",")
                                        cookieID = cookieID.Remove(cookieID.Length - 1);
                                }
                            }
                            else
                            {
                                cookieID = "NULL";
                            }
                            // query for getby cookie' 
                            //                    NewQuery = ("select  ROW_NUMBER() over (ORDER BY ProductId desc) as Srno , ProductId,ProductName ,Code, Unit,  Product_Rate,WithoutdicRate,sum(Quantity) as Quantity,   sum(Amount) as Amount ,vat, sum(vat1) as vat1, "
                            //+ "Img_Path,Img_Pathhover ,Product_Discount,Product_CategoryId from ( "
                            //+ "SELECT     ROW_NUMBER() over (ORDER BY Product_Id desc) as Srno , tblProduct.Product_Id as ProductId, tblProduct.Product_Name AS ProductName,  tblProduct.Product_Code as Code,ISNULL(tblProduct.Product_UOM, 'KG') AS Unit, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) AS Product_Rate, tblProduct.Product_Rate AS WithoutdicRate,  isnull(tblCart.Cart_Qty,1) as Quantity, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  isnull(tblCart.Cart_Qty,1) as  Amount,  CONVERT(decimal(18, 2),tblProduct.Product_Vat) as vat,       CONVERT(decimal(18, 2),   (CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  isnull(tblCart.Cart_Qty,1))- (( CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  tblCart.Cart_Qty)/(1+  CONVERT(decimal(18, 2),tblProduct.Product_Vat)/100)) )as vat1, "
                            //+ "ISNULL(tblProduct.Product_MainImage, '/images/product/noimage.jpg') AS Img_Path,ISNULL(tblProduct.Product_HoverImage, '/images/product/noimage.jpg') AS Img_Pathhover, ISNULL(tblProduct.Product_Discount, 0.00) AS Product_Discount, tblProduct.Product_CategoryId FROM            tblProduct inner JOIN                         tblCart ON tblProduct.Product_Id = tblCart.Cart_ProductId "
                            //+ "WHERE    tblCart.Cart_CustId= " + HttpContext.Current.Session["U_Id"] + "   and Cart_Delete='False'  AND (tblProduct.Product_Del = 'False') AND (tblProduct.Product_Status = 'Active')UNION	SELECT     ROW_NUMBER() over (ORDER BY Product_Id desc) as Srno , tblProduct.Product_Id as ProductId, tblProduct.Product_Name AS ProductName, tblProduct.Product_Code as Code,ISNULL(tblProduct.Product_UOM, 'KG') AS Unit, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) AS Product_Rate, tblProduct.Product_Rate AS WithoutdicRate,  "
                            //+ "'1' as quantity, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) * 1 as  Amount,  CONVERT(decimal(18, 2),tblProduct.Product_Vat) as vat,       CONVERT(decimal(18, 2),   (CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  1)- (( CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  1)/(1+  CONVERT(decimal(18, 2),tblProduct.Product_Vat)/100)) )as vat1,"
                            //+ "ISNULL(tblProduct.Product_MainImage, '/images/product/noimage.jpg') AS Img_Path,ISNULL(tblProduct.Product_HoverImage, '/images/product/noimage.jpg') AS Img_Pathhover, ISNULL(tblProduct.Product_Discount, 0.00) AS Discount, tblProduct.Product_CategoryId FROM            tblProduct WHERE      Product_Id in (	 " + cookieID + " ) and   (tblProduct.Product_Del = 'False') AND (tblProduct.Product_Status = 'Active'))x group by  ProductId,ProductName ,Code, Unit,  Product_Rate,WithoutdicRate,    vat, Img_Path,Img_Pathhover ,Product_Discount,Product_CategoryId");
                            DataTable dtcookicart = dbCart.buildcart("buildemptycart", HttpContext.Current.Session["U_Id"].ToString(), cookieID).Tables[0];

                            for (int i = 0; i <= dtcookicart.Rows.Count - 1; i++)
                                //  placeorder(dtcookicart.Rows(i)("ProductId"), dtcookicart.Rows(i)("Quantity"));
                                placeorder(Convert.ToInt32(dtcookicart.Rows[i]["ProductId"]), Convert.ToInt32(dtcookicart.Rows[i]["Quantity"]));
                            HttpContext.Current.Session["Update"] = "True";
                        }
                    }
                    else
                    {
            //            // logged in and order cart is having before login
            //            if (HttpContext.Current.Session["Update"].ToString() == "True")
            //            {
            //            }
            //            else
            //            {
            //                // Dim dtcart As DataTable = MainBLL.ViewByID(HttpContext.Current.Session["U_Id"], "getcartbyID").Tables(0)
            //                // cartCookie = HttpContext.Current.Request.Cookies["TheCart"]
            //                // Dim cookieID, NewQuery As String
            //                if ((((HttpContext.Current.Request.Cookies["TheCart"]) == null)))
            //                {
            //                    cookieID = HttpContext.Current.Request.Cookies["TheCart"].Value;
            //                    if (cookieID == "")
            //                        cookieID = "NULL";
            //                    else
            //                    {
            //                        cookieID = cookieID.Replace(",", "/7.3,");
            //                        string str = cookieID.Substring(cookieID.Length - 1);
            //                        if (str == ",")
            //                            cookieID = cookieID.Remove(cookieID.Length - 1);
            //                    }
            //                }
            //                // query for getby cookie'  NewQuery = "SELECT ROW_NUMBER() over (ORDER BY (Select 1) desc) as Srno , '1' as qty, tblProduct.Product_Id as ProductId, tblProduct.Product_Name AS ProductName, tblProduct.Product_Code as Code,ISNULL(tblProduct.Product_UOM, 'KG') AS Unit, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) AS Product_Rate, tblProduct.Product_Rate AS WithoutdicRate,   CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) * 1 as  Amount,  CONVERT(decimal(18, 2),tblProduct.Product_Vat) as vat,       CONVERT(decimal(18, 2),   (CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  1)- (( CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  1)/(1+  CONVERT(decimal(18, 2),tblProduct.Product_Vat)/100)) )as vat1, ISNULL(tblProduct.Product_MainImage, '/images/product/noimage.jpg') AS Img_Path,ISNULL(tblProduct.Product_HoverImage, '/images/product/noimage.jpg') AS Img_Pathhover, ISNULL(tblProduct.Product_Discount, 0.00) AS discount, tblProduct.Product_CategoryId     FROM tblProduct WHERE      Product_Id in (" + cookieID + ")  and   (tblProduct.Product_Del = 'False') AND (tblProduct.Product_Status = 'Active')"

            //                NewQuery = ("SELECT     ROW_NUMBER() over (ORDER BY Product_Id desc) as Srno , tblProduct.Product_Id as ProductId, tblProduct.Product_Name AS ProductName,  tblProduct.Product_Code as Code,ISNULL(tblProduct.Product_UOM, 'KG') AS Unit, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) AS Product_Rate, tblProduct.Product_Rate AS WithoutdicRate,  isnull(tblCart.Cart_Qty,1) as Quantity, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  isnull(tblCart.Cart_Qty,1) as  Amount,  CONVERT(decimal(18, 2),tblProduct.Product_Vat) as vat,       CONVERT(decimal(18, 2),   (CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  isnull(tblCart.Cart_Qty,1))- (( CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  tblCart.Cart_Qty)/(1+  CONVERT(decimal(18, 2),tblProduct.Product_Vat)/100)) )as vat1, "
            //+ "ISNULL(tblProduct.Product_MainImage, '/images/product/noimage.jpg') AS Img_Path,ISNULL(tblProduct.Product_HoverImage, '/images/product/noimage.jpg') AS Img_Pathhover, ISNULL(tblProduct.Product_Discount, 0.00) AS Product_Discount, tblProduct.Product_CategoryId FROM            tblProduct inner JOIN                         tblCart ON tblProduct.Product_Id = tblCart.Cart_ProductId "
            //+ "WHERE    tblCart.Cart_CustId= " + HttpContext.Current.Session["U_Id"] + "  and Cart_Delete='False'    AND (tblProduct.Product_Del = 'False') AND (tblProduct.Product_Status = 'Active')UNION	SELECT     ROW_NUMBER() over (ORDER BY Product_Id desc) as Srno , tblProduct.Product_Id as ProductId, tblProduct.Product_Name AS ProductName, tblProduct.Product_Code as Code,ISNULL(tblProduct.Product_UOM, 'KG') AS Unit, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) AS Product_Rate, tblProduct.Product_Rate AS WithoutdicRate,  "
            //+ "'1' as quantity, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) * 1 as  Amount,  CONVERT(decimal(18, 2),tblProduct.Product_Vat) as vat,       CONVERT(decimal(18, 2),   (CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  1)- (( CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  1)/(1+  CONVERT(decimal(18, 2),tblProduct.Product_Vat)/100)) )as vat1,"
            //+ "ISNULL(tblProduct.Product_MainImage, '/images/product/noimage.jpg') AS Img_Path,ISNULL(tblProduct.Product_HoverImage, '/images/product/noimage.jpg') AS Img_Pathhover, ISNULL(tblProduct.Product_Discount, 0.00) AS Discount, tblProduct.Product_CategoryId FROM            tblProduct WHERE      Product_Id in (	 " + cookieID + " ) and   (tblProduct.Product_Del = 'False') AND (tblProduct.Product_Status = 'Active')");
            //                DataTable dtcookie = DynamicparcelDisplayFunctions.getbytwotier(NewQuery.ToString());

            //                // dtsumcart.Merge(dtcart)
            //                // dtsumcart.Merge(dtcookie)
            //                // Session("Sumcart") = dtsumcart
            //                if (dtcookie.Rows.Count > 0)
            //                {
            //                    // Dim dt As DataTable = dtcart
            //                    // ''''below comment to check new approach
            //                    // count = dtcart.Rows.Count + 1
            //                    // For i = 0 To dtcookie.Rows.Count - 1
            //                    // Dim PrdId As Integer
            //                    // Dim amt, kgs, vat, vat1, rate, WithoutdicRate, discount, qty As Double
            //                    // Dim Code, ProductName, ProdImg, unit As String
            //                    // Dim productCount As Boolean = False

            //                    // PrdId = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("ProductId").ToString())
            //                    // Code = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("Code").ToString())
            //                    // ProductName = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("ProductName").ToString())
            //                    // WithoutdicRate = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("WithoutdicRate").ToString())
            //                    // rate = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("Product_Rate").ToString())
            //                    // discount = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("Product_Discount").ToString())
            //                    // vat = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("Vat").ToString())
            //                    // vat1 = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("Vat1").ToString())
            //                    // ProdImg = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("Img_Path").ToString())
            //                    // unit = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("Unit").ToString())
            //                    // amt = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("Amount").ToString())
            //                    // qty = DynamicparcelDataManipulation.FixNullFromDB(dtcookie.Rows(i)("Quantity").ToString())
            //                    // If qty = 0 Then
            //                    // qty = 1
            //                    // End If
            //                    // productCount = True
            //                    // Dim contains As Boolean = dt.AsEnumerable().Any(Function(row) Code = row.Field(Of [String])("Code"))
            //                    // If contains Then
            //                    // Dim oldrow() As DataRow = dt.Select("Code='" + Code + "'")
            //                    // qty = Convert.ToDouble(qty) + Convert.ToDouble(oldrow(0)("Quantity").ToString)
            //                    // dt.Rows.Remove(oldrow(0))
            //                    // dt.Rows.Add(count, PrdId, ProductName, Code, unit, rate, WithoutdicRate, qty, amt, vat, vat1, ProdImg, discount)
            //                    // count = count + 1
            //                    // Else
            //                    // dt.Rows.Add(count, PrdId, ProductName, Code, unit, rate, WithoutdicRate, qty, amt, vat, vat1, ProdImg, discount)
            //                    // count = count + 1


            //                    // End If
            //                    // Next
            //                    // Session["OrderProduct"] = dt
            //                    for (int i = 0; i <= dtcookie.Rows.Count - 1; i++)
            //                        placeorder(Convert.ToInt32(dtcookie.Rows[i]["ProductId"]), Convert.ToInt32(dtcookie.Rows[i]["Quantity"]));
            //                }
            //                HttpContext.Current.Session["Update"] = "True";
            //            }
                    }
                }
                //else if (HttpContext.Current.Session["OrderProduct"] == null)
                //{
                //    // 'not login and session cart is empty
                //    // Dim dtcart As DataSet = MainBLL.ViewByID(HttpContext.Current.Session["U_Id"], "getcartbyID")
                //    // cartCookie = HttpContext.Current.Request.Cookies["TheCart"]
                //    if ((((HttpContext.Current.Request.Cookies["TheCart"]) != null)))
                //    {
                //        cookieID = HttpContext.Current.Request.Cookies["TheCart"].Value;
                //        if (cookieID == "" | cookieID == "NULL")
                //            cookieID = "NULL";
                //        else
                //        {
                //            cookieID = cookieID.Replace(",", "/7.3,");
                //            string str = cookieID.Substring(cookieID.Length - 1);
                //            if (str == ",")
                //                cookieID = cookieID.Remove(cookieID.Length - 1);
                //        }
                //    }
                    
                //    NewQuery = ("SELECT     ROW_NUMBER() over (ORDER BY Product_Id desc) as Srno , tblProduct.Product_Id as ProductId, tblProduct.Product_Name AS ProductName, tblProduct.Product_Code as Code,ISNULL(tblProduct.Product_UOM, 'KG') AS Unit, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) AS Product_Rate, tblProduct.Product_Rate AS WithoutdicRate,  '1' as quantity, CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) * 1 as  Amount,  CONVERT(decimal(18, 2),tblProduct.Product_Vat) as vat,       CONVERT(decimal(18, 2),   (CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  1)- (( CONVERT(decimal(18, 2), tblProduct.Product_Rate - tblProduct.Product_Rate * ISNULL(tblProduct.Product_Discount, 0.00) / 100) *  1)/(1+  CONVERT(decimal(18, 2),tblProduct.Product_Vat)/100)) )as vat1,ISNULL(tblProduct.Product_MainImage, '/images/product/noimage.jpg') AS Img_Path,ISNULL(tblProduct.Product_HoverImage, '/images/product/noimage.jpg') AS Img_Pathhover, ISNULL(tblProduct.Product_Discount, 0.00) AS Discount, tblProduct.Product_CategoryId FROM   tblProduct WHERE  "
                //             + "Product_Id in (	 " + cookieID + " ) and   (tblProduct.Product_Del = 'False') AND (tblProduct.Product_Status = 'Active')");
                //    DataTable dtcookie = DynamicparcelDisplayFunctions.getbytwotier(NewQuery.ToString());


                //    // Dim dtcookie As DataTable = MainBLL.getcartbycookie(cookieID, "getcartbycookie").Tables(0)

                //    if (dtcookie.Rows.Count > 0)
                //    {
                //        for (int i = 0; i <= dtcookie.Rows.Count - 1; i++)
                //            //placeorder(dtcookie.Rows(i)("ProductId"), dtcookie.Rows(i)("Quantity"));
                //            placeorder(Convert.ToInt32(dtcookie.Rows[i]["ProductId"]), Convert.ToInt32(dtcookie.Rows[i]["Quantity"]));
                //    }
                //}
                else
                {
                    // 'not login and session cart is not empty
                    //HttpContext.Current.Session["Update"].ToString() == "True"
                    if (HttpContext.Current.Session["Update"] != null)
                    {
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        public void placeorder(Int32 PrdId, Int32 Qty)
        {

            try
            {

                //HttpCookie cartCookie;

                if (PrdId > 0)
                {
                    DataSet dsPrd = new DataSet();
                    dsPrd = dbPt.Getcartproduct("getaddtocart", Convert.ToInt32(HttpContext.Current.Session["U_Id"].ToString()), PrdId, Qty);


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

                        PID =   BstarGeneral.GetIntVal(dsPrd.Tables[0].Rows[0]["Product_Id"].ToString());
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
                        cartCookie = HttpContext.Current.Request.Cookies["TheCart"];
                        if (cartCookie == null)
                            HttpContext.Current.Response.Cookies.Add(cartCookie);
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
                        HttpContext.Current.Response.Cookies.Add(cartCookie);

                    }
                }
                if (HttpContext.Current.Session["OrderProduct"] == null)
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
                    if (HttpContext.Current.Session["U_Id"].ToString() != null)
                    {
                        DataSet datacart = dbCart.CheckCartName(PrdId.ToString(), HttpContext.Current.Session["U_Id"].ToString(), "CartCheck");
                        Checkgroupname = datacart.Tables[0].Rows[0]["result"].ToString();
                        if ((Checkgroupname == "Already Exist"))
                        {
                            dbCart.Update(HttpContext.Current.Session["U_Id"].ToString(), PrdId, Qty, HttpContext.Current.Session["U_Id"].ToString(), Convert.ToInt32(datacart.Tables[1].Rows[0]["Cart_Id"]));
                        }
                        else
                            dbCart.Insert(HttpContext.Current.Session["U_Id"].ToString(), PrdId, Qty, HttpContext.Current.Session["U_Id"].ToString());
                    }




                    HttpContext.Current.Session["OrderProduct"] = dt;
                    if (dt.Rows.Count == 0)
                        throw new Exception(": Please Select Atleast One Product");
                }
                else
                {
                    DataTable dt = (DataTable)HttpContext.Current.Session["OrderProduct"];
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
                                    HttpContext.Current.Session["OrderProduct"] = dt;
                                    break;
                                }
                            }
                        }



                        string Checkgroupname = "";
                        if (HttpContext.Current.Session["U_Id"].ToString() != null)
                        {
                            DataSet datacart = dbCart.CheckCartName(PrdId.ToString(), HttpContext.Current.Session["U_Id"].ToString(), "CartCheck");
                            Checkgroupname = datacart.Tables[0].Rows[0]["result"].ToString();
                            if ((Checkgroupname == "Already Exist"))
                            {
                                dbCart.Update(HttpContext.Current.Session["U_Id"].ToString(), PrdId, Qty, HttpContext.Current.Session["U_Id"].ToString(), Convert.ToInt32(datacart.Tables[1].Rows[0]["Cart_Id"]));
                            }
                            else
                                dbCart.Insert(HttpContext.Current.Session["U_Id"].ToString(), PrdId, Qty, HttpContext.Current.Session["U_Id"].ToString());
                        }

                    }
                    else
                    {
                        //  Amount = ((Convert.ToInt32(Qty) * Convert.ToDouble(Packing_Weight)) * Convert.ToDouble(rate));
                        //  vatinr = Convert.ToDouble((vatper * Amount) / (double)100);


                        dt.Rows.Add(count, PID, ProductName, Code, rate, Quantity, Packing_Weight, TotalWeight, Amount, vatinr, vatper, unit, Product_MOQ, ProdImg, href, Product_linkas);

                        count = count + 1;
                        string Checkgroupname = "";
                        if (HttpContext.Current.Session["U_Id"].ToString() != null)
                        {
                            DataSet datacart = dbCart.CheckCartName(PrdId.ToString(), HttpContext.Current.Session["U_Id"].ToString(), "CartCheck");
                            Checkgroupname = datacart.Tables[0].Rows[0]["result"].ToString();
                            if ((Checkgroupname == "Already Exist"))
                            {
                                dbCart.Update(HttpContext.Current.Session["U_Id"].ToString(), PrdId, Qty, HttpContext.Current.Session["U_Id"].ToString(), Convert.ToInt32(datacart.Tables[1].Rows[0]["Cart_Id"]));
                            }
                            else
                                dbCart.Insert(HttpContext.Current.Session["U_Id"].ToString(), PrdId, Qty, HttpContext.Current.Session["U_Id"].ToString());
                        }

                    }


                    HttpContext.Current.Session["OrderProduct"] = dt;
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

    }
}