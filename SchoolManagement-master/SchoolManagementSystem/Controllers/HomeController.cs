using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Subjects()
        {
            return View();
        }

    }
}
