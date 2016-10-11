using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class PaymentController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        public ActionResult Index()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            var payments = repository.Payments;

            if (currUser.Role != "Secretary")
            {
                return RedirectToAction("MyPayment", "Payment");
            }
            
            return View(payments.ToList());
        }
        
        public ActionResult MyPayment()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            var payments = repository.Payments.Where(p => p.Student_PIN == currUser.Login);

            if (currUser.Role != "Student")
            {
                return RedirectToAction("Index", "Payment");
            }

            return View(payments.ToList());
        }
        
        public ActionResult Details(int id = 0)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }
        
        //For students
        public ActionResult PaymentDetails(int id = 0)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }
        
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentID", "PIN");
            ViewBag.SubjectId = new SelectList(db.Disciplines, "Id", "Subject");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentID", "PIN", payment.StudentID);
            ViewBag.SubjectId = new SelectList(db.Disciplines, "Id", "Subject", payment.Discipline_Id);
            if (ModelState.IsValid)
            {
                Student student = db.Students.FirstOrDefault(s => s.StudentID == payment.StudentID);
                payment.Student_PIN = student.PIN;
                payment.Type = "cash";
                db.Payments.Add(payment);
                db.SaveChanges();
                ViewBag.Message = student.FirstName+ " "+ student.LastName + " has paid: <br/> R" + payment.Value + "<br/> Towards " + payment.Type;
                return RedirectToAction("Index");
            }
            return View(payment);
        }
        
        public ActionResult Edit(int id)
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentID", "PIN");
            ViewBag.SubjectId = new SelectList(db.Disciplines, "Id", "Subject");
            Payment payment = db.Payments.Find(id);

            if (payment == null)
            {
                return HttpNotFound();
            }

            return View(payment);
        }
        
        [HttpPost]
        public ActionResult Edit(Payment payment)
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "PIN", payment.StudentID);
            ViewBag.SubjectId = new SelectList(db.Disciplines, "Id", "Subject", payment.Discipline_Id);

            if (ModelState.IsValid)
            {
                Student student = db.Students.FirstOrDefault(s => s.StudentID == payment.StudentID);
                payment.Student_PIN = student.PIN;
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payment);
        }
        
        public ActionResult Delete(int id)
        {
            Payment payment = db.Payments.Find(id);

            if (payment == null)
            {
                return HttpNotFound();
            }

            return View(payment);
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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