using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mail;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace SchoolManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        // GET: Index
        public ActionResult Index()
        {
            IndexModelView imv = new IndexModelView();

            imv.Posts = (from p in repository.News

                         orderby p.Date descending
                         select p).Take(3).ToList();

            imv.Students = (from e in db.Students
                            orderby e.LastName descending
                            select e).ToList();

            imv.Events = (from e in db.Event
                          orderby e.EventDate descending
                          select e).ToList();

            imv.Teachers = (from m in db.Teachers
                            orderby m.LastName ascending
                            select m).Take(5).ToList();

            ViewBag.Members = db.Teachers.Count();
            ViewBag.Students = db.Students.Count();

            return View(imv);
        }
        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact(Contact c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    MailAddress from = new MailAddress(c.Email.ToString());
                    StringBuilder sb = new StringBuilder();
                    msg.To.Add("youremail@email.com");
                    msg.Subject = "Contact Us";
                    msg.IsBodyHtml = false;
                    smtp.Host = "mfd.dut.ac.za";
                    smtp.Port = 443;
                    sb.Append("First name: " + c.FirstName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Last name: " + c.LastName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Email: " + c.Email);
                    sb.Append(Environment.NewLine);
                    sb.Append("Comments: " + c.Comment);
                    msg.Body = sb.ToString();
                    smtp.Send(msg);
                    msg.Dispose();
                    return View("Success");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View();
        }

        public ActionResult Subjects()
        {
            return View();
        }

    }
}
