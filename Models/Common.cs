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
}