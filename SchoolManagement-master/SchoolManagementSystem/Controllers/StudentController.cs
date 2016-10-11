using System;
using System.Linq;
using System.Web.Mvc;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();
        public ActionResult Index()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            var thisMonthMarks = repository.Marks.Where(x => x.Date.Month == DateTime.Now.Month
                                                        && x.Student_PIN == currUser.Login).ToList();

            int totalMarks = thisMonthMarks.Count(x => x.Value != -1);
            int goodMarks = thisMonthMarks.Count(x => x.Value >= 6 && x.Value != -1);
            int badMarks = thisMonthMarks.Count(x => x.Value <= 5 && x.Value != -1);
            int absences = thisMonthMarks.Count(x => x.Value == -1);

            var model = new StudentIndexViewModelcs()
            {
                TotalMarks = totalMarks,
                GoodMarks = goodMarks,
                BadMarks = badMarks,
                Abscences = absences
            };

            return View(model);
        }
        public ActionResult Overview()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            var thisMonthMarks = repository.Marks.Where(x => x.Date.Month == DateTime.Now.Month
                                                        && x.Date.Year == DateTime.Now.Year
                                                        && x.Student_PIN == currUser.Login).ToList();

            StudentMonthOverviewModel model = new StudentMonthOverviewModel()
            {
                StudentMarks = thisMonthMarks,
                Subjects = repository.Disciplines.ToList(),
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year
            };
            return View(model);
        }

        public ActionResult SelectMonth()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            return View();
        }

        [HttpPost]
        public ActionResult MonthOverview(int year, int month)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];


            var thisMonthMarks = repository.Marks.Where(x => x.Date.Month == month
                                                        && x.Date.Year == year
                                                        && x.Student_PIN == currUser.Login).ToList();

            StudentMonthOverviewModel model = new StudentMonthOverviewModel()
            {
                StudentMarks = thisMonthMarks,
                Subjects = repository.Disciplines.ToList(),
                Year = year,
                Month = month
            };
            return View(model);
        }

        public ActionResult Details()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            Student details = repository.Students.FirstOrDefault(x => x.PIN == currUser.Login);

            var className = repository.Classes.FirstOrDefault(x => x.Id == details.Class_Id);

            details.Avats = repository.Avatars.Where(x => x.StudentID == details.StudentID).FirstOrDefault();

            if (className != null)
            {
                ViewBag.className = className.Name;
            }
            
            return View(details);
        }
        public ActionResult Materials()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;
            return View(db.Files.ToList());
        }
        public FileResult DownLoad(int? id)
        {
            // Get current File from database
            File file = db.Files.Find(id);
            string fileName = file.FileName;
            string fileType = file.FileType.ToString();
            byte[] contents = file.Content;
            return File(contents, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        
        private ActionResult CheckUserRights()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            if (currUser == null || currUser.Role != "Student")
            {
                return RedirectToAction("Login", "Account");
            }

            return null;
        }
        public ActionResult SignalRChat()
        {
            return View();
        }
    }
}
