using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Mail;
using prolib;
using Newtonsoft.Json;

namespace SwissbakeIn.App_Start
{
    public class Helper
    {


        public static string GetStatus(string stats)
        {
            try
            {
                String ret = "";

                switch (stats)
                {
                    case "Pending":
                        ret = "pend";
                        break;
                    case "In-Process":
                        ret = "pend";
                        break;
                    case "Pending Approval":
                        ret = "pend";
                        break;
                    case "Complete":
                        ret = "Complet";
                        break;
                    case "Paid":
                        ret = "Complet";
                        break;
                    case "Confirmation Sent":
                        ret = "Approve";
                        break;
                    case "Resolved":
                        ret = "Complet";
                        break;
                    case "Approved":
                        ret = "Approve";
                        break;
                    case "Placed":
                        ret = "Place";
                        break;
                    case "Cancelled":
                        ret = "cancl";
                        break;
                    case "Cancel":
                        ret = "cancl";
                        break;
                    case "Invoice Sent":
                        ret = "sent";
                        break;
                    case "Delivered":
                        ret = "Complet";
                        break;
                    case "Dispatched":
                        ret = "sent";
                        break;
                    case "Part Dispatched":
                        ret = "PartDisp";
                        break;
                    case "Partial":
                        ret = "PartDisp";
                        break;
                    case "On Hold":
                        ret = "onhold";
                        break;
                }

                return ret;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string InPrice(double price)
        {
            System.Globalization.CultureInfo Bahrainn = new System.Globalization.CultureInfo("hi-IN");
            return @String.Format(Bahrainn, "{0:N3}", price);
        }
        public static string url()
        {

            return HttpContext.Current.Request.Url.Host;

        }
        public static int urlport()
        {


            return HttpContext.Current.Request.Url.Port;
        }
        public static int checkexclusive(int id)
        {
            Account dbAt = new Account();
            return dbAt.checkexclusive(id);

        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        //public static string Encrypt(string encryptString)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
        //    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        //});
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            encryptString = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return encryptString;
        //}

        //public static string Decrypt(string cipherText)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    cipherText = cipherText.Replace(" ", "+");
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
        //    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        //});
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return cipherText;
        //}

        public static int GenerateOtp()
        {
            int _min = 100000;
            int _max = 999900;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }





        public static int checknoticount(int id, string mode)
        {
            Account dbAt = new Account();
            return dbAt.checkcount(id, mode);

        }
        public static string checkapi(){
            return string.Empty;
            String UserIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(UserIP))
            {
                UserIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (UserIP == "::1"){
                    UserIP = "27.4.149.210";
                        }
            }
            string url = "https://freegeoip.app/json/" + UserIP.ToString();
            WebClient client = new WebClient();
            string jsonstring = client.DownloadString(url);
            dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);
          //  System.Web.HttpContext.Current.Session["UserCountryCode"] = dynObj.country_code;
            return dynObj.country_name;
        }


    }
}