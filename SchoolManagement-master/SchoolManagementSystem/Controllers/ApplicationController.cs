using SchoolManagementSchool.Models;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ApplicationController : Controller
    {
        private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();


        // GET: ApplicationForm
        [HttpGet]
        public ActionResult Index()
        {
            var applications = repository.Applications.ToList();
            List<ApplicationViewModel> model = new List<ApplicationViewModel>();

            foreach (var item in applications)
            {
                var studentClass = db.Classes.FirstOrDefault(x => x.Id == item.Class_Id);
                model.Add(new ApplicationViewModel() { application = item, ClassName = studentClass.Name });
            }

            return View(model);
        }

        // GET: ApplicationForm/Details/5
        public ActionResult Details(int? id)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;


            ApplicationForm application = db.Applications.Find(id);

            if (application == null)
                return HttpNotFound();
            if (application.Read != "true")
            {
                application.Read = "True";
                application.Date = DateTime.Now;
                UpdateModel(application);
                db.SaveChanges();
            }

            return View(application);
        }

        // GET: ApplicationForm/Create
        public ActionResult Create()
        {
            List<Class> model = repository.Classes.ToList();
            return View(model);
        }

        // POST: ApplicationForm/Create
        [HttpPost]
        public ActionResult Create(ApplicationForm cr)
        {
            var curStud = repository.Applications.FirstOrDefault(x => x.StudentID == cr.StudentID);

            if (curStud != null)
            {                                
                ViewBag.message = "Student was not created! Such PIN already exists";
                List<Class> mod = repository.Classes.ToList();

                return View(mod);
            }

            repository.AddApplication(cr);

            ViewBag.message = "A new student was successfully added.";
            List<Class> model = repository.Classes.ToList();

            string mail = Request["Email"];
            EmailClass obj = new EmailClass();
            obj.From = new MailAddress("21449476@dut4life.ac.za");
            List<MailAddress> tolist = new List<MailAddress>();
            tolist.Add(new MailAddress(mail));
            obj.Body = "Thank you for your registration your user name is " + cr.FirstName;
            obj.to = tolist;
            obj.Method();



            return RedirectToAction("Info");

            //try
            //{
            //    // TODO: Add insert logic here
            //    if (ModelState.IsValid)
            //    {
            //        cr.Date = DateTime.Now;
            //        cr.ReadDate = DateTime.Now;
            //        cr.Read = "False";
            //        db.Applications.Add(cr);
            //        db.SaveChanges();
            //    }
            //    return RedirectToAction("Info");
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Message = ex.Message;
            //    return View();
            //}
        }

        // GET: ApplicationForm/Edit/5
        public ActionResult Edit(int id)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            ApplicationForm applications = db.Applications.Find(id);
            if (applications == null)
            {
                return HttpNotFound();
            }
            return View(applications);
        }

        // POST: ApplicationForm/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationForm/Delete/5
        public ActionResult Delete(int id)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            ApplicationForm applications = db.Applications.Find(id);
            if (applications == null)
            {
                return HttpNotFound();
            }
            return View(applications);

        }

        // POST: ApplicationForm/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Info()
        {
            return View();
        }

        private ActionResult CheckUserRights()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            if (currUser == null || currUser.Role != "Secretary")
            {
                return RedirectToAction("Login", "Account");
            }

            return null;
        }
    }
}
