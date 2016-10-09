using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class SmsController : Controller
    {
        private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        // GET: Sms
        public ActionResult Index()
        {

            var messages = db.Sms.ToList();

            //Try
            //http://api.clickatell.com/http/sendmsg?user=Lungelo1&password=YKfBOKLNUMYWOV&api_id=3585050&to=27789795029&text=Message
            string Message = "This is cool";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://api.clickatell.com/http/sendmsg?user=Lungelo1&password=YKfBOKLNUMYWOV&api_id=3585050&to=27789795029&text=" + Message);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            //Try

            return View(messages);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SmsModel sms)
        {
            string message = "Okay";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.mymobileapi.com/api5/http5.aspx?Type=send&Username=GodsProsperity&Password=gpsms&data=" + message + "&numto0789795029");
            HttpWebResponse myResp = (HttpWebResponse)request.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            return View();
        }


    }
}