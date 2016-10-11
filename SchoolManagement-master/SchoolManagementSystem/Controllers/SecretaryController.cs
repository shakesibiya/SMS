using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;
using System;
using System.Data.Entity;
using System.Web;
using System.IO;

namespace SchoolManagementSystem.Controllers
{
    public class SecretaryController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        public ActionResult Index()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            return View();
        }


        public ActionResult Students()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var students = repository.Students.ToList();
            List<StudentViewModel> model = new List<StudentViewModel>();

            foreach (var item in students)
            {
                var studentClass = repository.Classes.FirstOrDefault(x => x.Id == item.Class_Id);

                model.Add(new StudentViewModel() { Student = item/*, ClassName = studentClass.Name*/ });
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Deregister(int id)
        {

            if (db.Students.Find(id) == null)
                return HttpNotFound();
            return View();

        }

        public ActionResult Deregister(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    var c = db.Students.Find(id);
                    var ct = new StudentTrush();
                    ct.StudentID = c.StudentID;
                    ct.PIN = c.PIN;
                    ct.Password = c.Password;
                    ct.PhoneNumber = c.PhoneNumber;
                    ct.MobilePhoneNumber = c.MobilePhoneNumber;
                    ct.FirstName = c.FirstName;                    
                    ct.Email = c.Email;
                    ct.StreetAddress = c.StreetAddress;
                    ct.PostalAddress = c.PostalAddress;
                    ct.Province = c.Province;
                    ct.Religion = c.Religion;
                    ct.Surbs = c.Surbs;
                    ct.PostalCode = c.PostalCode;
                    ct.DateDeleted = DateTime.Now;
                    db.Deregister.Add(ct);
                    db.Students.Remove(c);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Teachers()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var teachers = repository.Teachers.ToList();
            List<TeacherViewModel> model = new List<TeacherViewModel>();

            foreach (var item in teachers)
            {
                var teacherDiscipline = repository.Disciplines.FirstOrDefault(x => x.Id == item.Discipline_Id);

                model.Add(new TeacherViewModel() { Teacher = item, Discipline = teacherDiscipline.Subject });
            }

            return View(model);
        }
        private void PopulateCountriesDropDownList(object selectedCountry = null)
        {
            var list = db.Countries.OrderBy(r => r.CountryCode).ToList().Select(rr =>
            new SelectListItem { Value = rr.Countryid.ToString(), Text = rr.CountryCode }).ToList();

            ViewBag.Countries = list;
        }
        public ActionResult AddStudent()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            PopulateCountriesDropDownList();
            List<Class> model = repository.Classes.ToList();

            return View(model);
        }

        public ActionResult AddTeacher()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            SecretaryAddTeacherViewModel model = new SecretaryAddTeacherViewModel();

            model.Disciplines = repository.Disciplines.ToList();
            model.Classes = repository.Classes.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var curStud = repository.Students.FirstOrDefault(x => x.PIN == student.PIN);

            if (curStud != null)
            {
                ViewBag.message = "Student was not created! Such PIN already exists";
                List<Class> mod = repository.Classes.ToList();
                return View(mod);
            }
            repository.AddStudent(student);

            ViewBag.message = "A new student was successfully added.";
            List<Class> model = repository.Classes.ToList();
            PopulateCountriesDropDownList(student.Countryid);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTeacher(Teacher teacher, int[] classes)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            SecretaryAddTeacherViewModel model = new SecretaryAddTeacherViewModel();

            model.Disciplines = repository.Disciplines.ToList();
            model.Classes = repository.Classes.ToList();


            var curTeach = repository.Teachers.FirstOrDefault(x => x.PIN == teacher.PIN);

            if (curTeach != null)
            {
                ViewBag.message = "Teacher was not created! Such PIN already exists";
                List<Class> mod = repository.Classes.ToList();
                return View(model);
            }

            List<Class> classteToStudy = new List<Class>();
            foreach (var classItem in classes)
            {
                var classToStudy = repository.Classes.FirstOrDefault(x => x.Id == classItem);
                classteToStudy.Add(classToStudy);
            }

            teacher.ClassesToStudy = classteToStudy;
            repository.AddTeacher(teacher);

            ViewBag.message = "A new teacher was successfully added.";

            return View(model);
        }

        public ActionResult Search(string search)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var searchLowCase = search.ToLower();

            var students = repository.Students.Where(
                x => x.FirstName.ToLower().Contains(searchLowCase) ||
                x.LastName.ToLower().Contains(searchLowCase) ||
                x.PIN == searchLowCase).ToList();

            List<StudentViewModel> modelStud = new List<StudentViewModel>();

            foreach (var item in students)
            {
                var studentClass = repository.Classes.FirstOrDefault(x => x.Id == item.Class_Id);

                modelStud.Add(new StudentViewModel() { Student = item, ClassName = studentClass.Name });
            }

            var teachers = repository.Teachers.Where(
                x => x.FirstName.ToLower().Contains(searchLowCase) ||
                x.LastName.ToLower().Contains(searchLowCase) ||
                x.PIN == searchLowCase).ToList();

            List<TeacherViewModel> modelTeach = new List<TeacherViewModel>();

            foreach (var item in teachers)
            {
                var teacherDiscipline = repository.Disciplines.FirstOrDefault(x => x.Id == item.Discipline_Id);

                modelTeach.Add(new TeacherViewModel() { Teacher = item, Discipline = teacherDiscipline.Subject });
            }

            return View(new SecretarySearchModel() { Students = modelStud, Teachers = modelTeach });
        }
        public ActionResult AddNews()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            Post model = new Post();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddNews(Post P)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            repository.AddNews(P);

            ViewBag.message = "Your Post was successfully added.";
            return RedirectToAction("Index", "PostPictures", new { Id = P.Id });

        }

        // GET: News/Delete/5
        public ActionResult Delete(int id)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;
            return View();
        }

        // POST: News/Delete/5
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
        public ActionResult UpdateStudent(int id)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            ViewBag.Apply = new SelectList(db.Classes, "Id", "Name");

            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }
        [HttpPost]
        public ActionResult UpdateStudent(Student student)
        {
            ViewBag.Apply = new SelectList(db.Classes, "Id", "Name", student.Class_Id);

            if(ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        [HttpGet]
        public ActionResult ApproveStudent(int id)
        {

            if (db.Applications.Find(id) == null)
                return HttpNotFound();
            return View();

        }
        public ActionResult ApproveStudent(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    var c = db.Applications.Find(id);
                    var ct = new Student();
                    ct.StudentID = c.StudentID;
                    ct.Email = c.Email;
                    ct.PhoneNumber = c.PhoneNumber;
                    ct.MobilePhoneNumber = c.MobilePhoneNumber;
                    ct.Class_Id = c.Class_Id;
                    ct.FirstName = c.FirstName;
                    ct.StreetAddress = c.StreetAddress;
                    ct.PostalAddress = c.PostalAddress;
                    ct.Province = c.Province;
                    ct.Religion = c.Religion;
                    ct.Surbs = c.Surbs;
                    ct.PostalCode = c.PostalCode;
                    db.Students.Add(ct);
                    db.Applications.Remove(c);
                    db.SaveChanges();

                    Sms sms = new Sms();
                    Student stud = new Student();
                    // var sms = new Smscan();
                    HttpCookie myCookie = new HttpCookie("MyCookie");
                    myCookie = Request.Cookies["MyCookie"];
                    
                    int Id = Convert.ToInt16(myCookie);
                    string feedback = "";
                    stud.PIN = "2017".Count().ToString();
                    try
                    {
                        sms.Send_SMS(stud.PhoneNumber.ToString(), "Hi " + stud.FirstName + " Welcome to Exemption Matric Centre, you registered as a " + stud.FirstName +"."+ " Use " + stud.PIN + " to login and password " + "2017.");
                    }
                    catch(Exception e)
                    {
                        feedback += e.Message;
                    }

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
