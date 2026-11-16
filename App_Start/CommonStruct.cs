using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissbakeIn.Entities
{
    public class CommonStruct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Pin { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public int CatId { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public string Product { get; set; }
        public int ProdId { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Etcstring1 { get; set; }    
        public string Etcstring2 { get; set; }
        public string Etcstring3 { get; set; }
        public string Etcstring4 { get; set; }
        public int Etcint1 { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double Qty { get; set; }
        public double EtcDouble1 { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Datetime { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public int Id2 { get; set; }
        public int Id3 { get; set; }
        public string Freight { get; set; }
        public string AWB { get; set; }
        public string Tracking { get; set; }
        public string Status2 { get; set; }
        public string Url2 { get; set; }
        public string Gst { get; set; }


        #region Faisal
        public int PrdCategory_ID { get; set; }
        public string PrdCategory_Name { get; set; }
        public string PrdCategory_Status { get; set; }
        public string PrdCategory_Seotitle { get; set; }
        public string PrdCategory_Seourl { get; set; }
        public string PrdCategory_Image { get; set; }





        public string Product_MainImage { get; set; }
        public int Product_Id { get; set; }
        public double Product_Rate { get; set; }

        public double Product_Vat { get; set; }
        public string Product_Dosage { get; set; }
        public string Product_Name { get; set; }
        public string Product_Code { get; set; }
        public string Product_Usage { get; set; }
        public string Product_Stock { get; set; }
        public string Product_Recipes { get; set; }
        public string Product_Type { get; set; }
        public string Product_Seotitle { get; set; }
        public string Product_Seourl { get; set; }
        public string Product_New { get; set; }
		public string Product_Instock { get; set; }
		public string Product_Onorder { get; set; }

        public string minprice { get; set; }
        public string maxprice { get; set; }
        public string Discount_Discount { get; set; }
        public string discounted_Rate { get; set; }
        public string Distdiscounted_Rate { get; set; }
        public string DistDiscount_Discount { get; set; }
        public string Discount_MinQty { get; set; }
        public string Product_OneLineDesc { get; set; }
        public string Packing_Packing { get; set; }

        public string Product_Desc { get; set; }
        public string Product_Description { get; set; }
        public string Product_Nutrionfacts { get; set; }
        public string Product_Ingrdt { get; set; }
        public string Product_Contains { get; set; }
        public string Product_HSCODE { get; set; }
        public string Product_Msdc { get; set; }
        public string Product_Seodesc { get; set; }
        public string Product_Seokeywords { get; set; }
        public string Product_Seotags { get; set; }

        public double Packing_Weight { get; set; }
        public double TotalWeight { get; set; }
        public double  Amount { get; set; }
        public double  vatinr { get; set; }
        public double  vatper { get; set; }
        public string unit { get; set; }
    public double Product_MOQ { get; set; }
        public string MainImg_Path { get; set; }
        public string href { get; set; }
        public string Product_linkas { get; set; }

        
        #endregion  






    }



    public static class QickCommonStruct
    {
        public static int Id { get; set; }
        public static string Name { get; set; }
        public static string CompName { get; set; }
        public static string Email { get; set; }
        public static string Mobile { get; set; }
        public static string Telephone { get; set; }
        public static string Pin { get; set; }
        public static string Address { get; set; }
        public static string City { get; set; }
        public static string State { get; set; }
        public static string Country { get; set; }
        public static string Category { get; set; }
        public static int CatId { get; set; }
        public static string Type { get; set; }
        public static int TypeId { get; set; }
        public static string Product { get; set; }
        public static int ProdId { get; set; }
        public static string Image { get; set; }
        public static string Url { get; set; }
        public static string Etcstring1 { get; set; }
        public static string Etcstring2 { get; set; }
        public static string Etcstring3 { get; set; }
        public static string Etcstring4 { get; set; }
        public static int Etcint1 { get; set; }
        public static double Price { get; set; }
        public static double Discount { get; set; }
        public static double Qty { get; set; }
        public static double EtcDouble1 { get; set; }
        public static DateTime CreatedDate { get; set; }
        public static DateTime ModifiedDate { get; set; }
        public static string Datetime { get; set; }
        public static string Status { get; set; }
        public static string Code { get; set; }
        public static int Id2 { get; set; }
        public static int Id3 { get; set; }
        public static string Freight { get; set; }
        public static string AWB { get; set; }
        public static string Tracking { get; set; }
        public static string Status2 { get; set; }
        public static string Url2 { get; set; }
        public static string Gst { get; set; }


        #region Faisal
        public static int PrdCategory_ID { get; set; }
        public static string PrdCategory_Name { get; set; }
        public static string PrdCategory_Status { get; set; }
        public static string PrdCategory_Seotitle { get; set; }
        public static string PrdCategory_Seourl { get; set; }
        public static string PrdCategory_Image { get; set; }





        public static string Product_MainImage { get; set; }
        public static int Product_Id { get; set; }
        public static double Product_Rate { get; set; }

        public static double Product_Vat { get; set; }
        public static string Product_Dosage { get; set; }
        public static string Product_Name { get; set; }
        public static string Product_Code { get; set; }
        public static string Product_Usage { get; set; }
        public static string Product_Stock { get; set; }
        public static string Product_Recipes { get; set; }
        public static string Product_Type { get; set; }
        public static string Product_Seotitle { get; set; }
        public static string Product_Seourl { get; set; }
        public static string Product_New { get; set; }
        public static string Product_Instock { get; set; }
        public static string Product_Onorder { get; set; }

        public static string minprice { get; set; }
        public static string maxprice { get; set; }
        public static string Discount_Discount { get; set; }
        public static string discounted_Rate { get; set; }
        public static string Distdiscounted_Rate { get; set; }
        public static string DistDiscount_Discount { get; set; }
        public static string Discount_MinQty { get; set; }
        public static string Product_OneLineDesc { get; set; }
        public static string Packing_Packing { get; set; }

        public static string Product_Desc { get; set; }
        public static string Product_Description { get; set; }
        public static string Product_Nutrionfacts { get; set; }
        public static string Product_Ingrdt { get; set; }
        public static string Product_Contains { get; set; }
        public static string Product_HSCODE { get; set; }
        public static string Product_Msdc { get; set; }
        public static string Product_Seodesc { get; set; }
        public static string Product_Seokeywords { get; set; }
        public static string Product_Seotags { get; set; }

        public static double Packing_Weight { get; set; }
        public static double TotalWeight { get; set; }
        public static double Amount { get; set; }
        public static double vatinr { get; set; }
        public static double vatper { get; set; }
        public static string unit { get; set; }
        public static double Product_MOQ { get; set; }
        public static string MainImg_Path { get; set; }
        public static string href { get; set; }
        public static string Product_linkas { get; set; }


        #endregion






    }
}