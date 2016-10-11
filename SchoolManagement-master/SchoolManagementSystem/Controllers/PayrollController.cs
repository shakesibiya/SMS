using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class PayrollController : Controller
    {
        private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();
        
        public ActionResult Index()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            var salaries = repository.Payrolls;
            if (currUser.Role != "Secretary")
            {
                return RedirectToAction("MyPayroll", "Payroll");
            }
            return View(salaries.ToList());
        }

        public ActionResult MyPayroll()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            var payroll = repository.Payrolls.Where(p => p.Teacher.PIN == currUser.Login);

            if (currUser.Role != "Teacher")
            {
                return RedirectToAction("Index", "Home");
            }

            return View(payroll.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        //For teachers
        public ActionResult MyPayrollDetails(int id = 0)
        {
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        public ActionResult Create()
        {
            ViewBag.PIN = new SelectList(db.Teachers, "PIN", "FirstName");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Payroll payroll)
        {
            ViewBag.PIN = new SelectList(db.Teachers, "PIN", "FirstName", payroll.Teacher.PIN);
            if (ModelState.IsValid)
            {
                db.Payrolls.Add(payroll);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payroll);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.PIN = new SelectList(db.Teachers, "PIN", "FirstName");
            Payroll payroll = db.Payrolls.Find(id);

            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        [HttpPost]
        public ActionResult Edit(Payroll payroll)
        {
            ViewBag.PIN = new SelectList(db.Teachers, "PIN", "FirstName", payroll.Teacher.PIN);
            if (ModelState.IsValid)
            {
                db.Entry(payroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payroll);
        }

        public ActionResult Delete(int id)
        {
            Payroll payroll = db.Payrolls.Find(id);

            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Payroll payroll = db.Payrolls.Find(id);
            db.Payrolls.Remove(payroll);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private ActionResult CheckUserRights()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            if (currUser == null || currUser.Role != "Secretary" && currUser.Role != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            return null;
        }
    }
}