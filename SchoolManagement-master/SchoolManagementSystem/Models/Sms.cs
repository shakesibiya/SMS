using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class Sms
    {
        private string p;
        private string p_2;
        private string username;
        private string password;

        public Sms()
        {
            p = String.Empty;
            p_2 = String.Empty;
        }
        public Sms(string p, string p_2)
        {
            this.p = p;
            this.p_2 = p_2;
        }
        public string MyString { get; set; }


        public string UserName
        {
            get { return username = "faniendlovu94@gmail.com"; }
            set { username = "faniendlovu94@gmail.com"; }
        }


        public string Password
        {
            get { return password = "Selinanyama02"; }
            set { password = "Selinanyama02"; }
        }
        public string Message { get; set; }
        public string Number { get; set; }
        public string MailError { get; set; }

        public string ReadHtmlPage(string url)
        {
            WebResponse objResponse;
            WebRequest objRequest;

            string result;
            try
            {
                objRequest = System.Net.WebRequest.Create(url);
                objResponse = objRequest.GetResponse();
                StreamReader sr = new StreamReader(stream: objResponse.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
                return result;
            }
            catch (Exception er)
            {
                string s = er.Message;
                return s;
            }

        }

        public bool Send_SMS(string num, string msg)
        {
            bool isvalid = false;
            try
            {
                string datesent = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();

                MyString = "https://www.winsms.co.za/api/batchmessage.asp?User=";
                MyString = MyString + UserName + "&Password=" + Password + "&Delivery=No";
                MyString = MyString + "&Message=" + msg + "&Numbers=" + num + ";";
                MailError = (ReadHtmlPage(MyString));
                MailError = ("Your message has been sent");
                isvalid = true;
                return isvalid;
            }
            catch
            {
                return isvalid;
            }

        }
    }
}