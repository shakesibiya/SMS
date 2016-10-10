using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;
using System.Data.Entity;
using System.Linq;
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
            var bookings = repository.Bookings.Where(b => b.Student.PIN == currUser.Login);

            return View(bookings.ToList());
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
            ViewBag.Event = new SelectList(db.Event, "Id", "Name", id);
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }
        
        public ActionResult Edit(int id)
        {
            ViewBag.Event = new SelectList(db.Event, "Id", "Name");
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
            ViewBag.Event = new SelectList(db.Event, "Id", "Name", booking.Event.Id);

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

        private ActionResult CheckUserRights()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            if (currUser == null || currUser.Role != "Secretary" && currUser.Role != "Student")
            {
                return RedirectToAction("Login", "Account");
            }

            return null;
        }
    }
}