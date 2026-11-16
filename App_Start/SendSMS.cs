using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace SwissbakeIn.App_Start
{
    public class SendSMS
    {
        public static string Send(string Number, string Mess)
        {
            string ozRecipients;
            //we creating the necessary URL string:
            string ozSURL = "http://103.16.101.52"; //where the SMS Gateway is running
            string ozSPort = "8080"; //port number where the SMS Gateway is listening
            string ozUser = HttpUtility.UrlEncode("atec-swissb"); //username for successful login
            string ozPassw = HttpUtility.UrlEncode("123456"); //user's password
            int ozMessageType = 0; //type of message
            int ozDLR = 0;
            string Source = "Swissb";
            if (HttpContext.Current.Request.Url.Host == "localhost")
            {
                ozRecipients = HttpUtility.UrlEncode(9773438089.ToString()); //who will get the message
            }
            else
            {
                ozRecipients = HttpUtility.UrlEncode(Number); //who will get the message
            }
            string ozMessageData = HttpUtility.UrlEncode(Mess); //body of message
            string createdURL = ozSURL + ":" + ozSPort + "/bulksms/bulksms?" +
                "username=" + ozUser +
                "&password=" + ozPassw +
                "&type=" + ozMessageType +
                "&dlr=" + ozDLR +
                "&destination=" + ozRecipients +
                "&source=" + Source +
                "&message=" + ozMessageData;

            string responseString = "";
            //Create the request and send data to the SMS Gateway Server by HTTP connection
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);
            myReq.Timeout = 25000;
            try
            {
                //Get response from the SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch (Exception ex)
            {
                responseString = "Request Timeout";
            }

            return responseString;
        }




    }
}