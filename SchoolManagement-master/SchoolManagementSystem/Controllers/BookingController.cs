using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class BookingController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();
        
        public ActionResult Index()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            var bookings = repository.Bookings;

            if (currUser.Role != "Secretary")
            {
                return RedirectToAction("MyBookings", "Booking");
            }

            return View(bookings.ToList());
        }

        public ActionResult MyBookings() // for student
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            var booking = db.Bookings.AsNoTracking().Where(b => b.BookedStudent.PIN == currUser.Login);
            if (booking == null)
            {

            }
            return View(booking.ToList());
        }
        
        public ActionResult Details(int id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        public ActionResult MyDetails(int id) // for student
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        public ActionResult Create(int? id)
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(int? id, Booking booking)
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            booking.BookedEvent = db.Event.AsNoTracking().FirstOrDefault(e => e.Id == id);
            booking.BookedStudent = db.Students.AsNoTracking().FirstOrDefault(s => s.PIN == currUser.Login);
            booking.Status = "Booked";

            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                SendEmail(booking);
                return RedirectToAction("Index");
            }
            return View(booking);
        }
        
        public ActionResult Edit(int id)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            Booking booking = db.Bookings.Find(id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }
        
        [HttpPost]
        public ActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        public ActionResult Delete(int id)
        {
            Booking booking = db.Bookings.Find(id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void SendEmail(Booking model)
        {
            var boddy = new StringBuilder();
            
            boddy.Append("Dear: " + model.UserName + ","
                + "<br/>These are your booking details: "
                + "<br/>Event: " + model.BookedEvent.Name
                + "<br/>Date: " + model.Date.ToShortDateString()
                + "<br/>Time: " + model.BookTime.ToShortTimeString()
                + "<br/>Venue: " + model.BookedEvent.Venue
                + "<br"
                + "<br/>We miss you already, visit us again :) ");

            string bodyFor = boddy.ToString();
            string subjectFor = "Booking";

            string toFor = model.Email;
            var mail = new MailAddress("21226357@dut4life.ac.za", "Matric Excemption Centre");
            WebMail.SmtpServer = "pod51014.outlook.com";
            WebMail.SmtpPort = 587;
            WebMail.UserName = "21226357@dut4life.ac.za";
            WebMail.Password = "Dut930623";
            WebMail.From = mail.ToString();
            WebMail.EnableSsl = true;

            try { WebMail.Send(toFor, subjectFor, bodyFor); }
            catch
            {
                // ignored
            }
        }

        private ActionResult CheckUserRights()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            if (currUser == null || currUser.Role != "Secretary")
            {
                return RedirectToAction("MyBookings", "Booking");
            }

            return null;
        }
    }
}