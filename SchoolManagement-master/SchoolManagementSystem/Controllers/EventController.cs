using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class EventController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        // GET: Event
        public ActionResult Index()
        {

            var posts = repository.Events.ToList();

            return View(posts);
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {

            Event pv = new Event();

            pv = db.Event.Where(x => x.Id == id).Single();


            return View(pv);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            Event model = new Event();

            return View(model);
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(Event e)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            db.Event.Add(e);
            db.SaveChanges();

            ViewBag.message = "Your Event was successfully added.";
            return RedirectToAction("Info");
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;
            return View();
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
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

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
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

        public ActionResult Comment(long Id,string comment)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            try
            {
                #region
                var query = db.Students.SingleOrDefault(x => x.PIN == ""); //Getting UserInfo
                var query1 = db.Teachers.SingleOrDefault(x => x.PIN == ""); //Getting UserInfo
                var query2 = db.Administrators.SingleOrDefault(x => x.Login == ""); //Getting UserInfo
                ViewBag.Fullname = query.FirstName + " " + query.LastName;  //Setting fullname for user
                #endregion

                EventComment c = new EventComment();
                c.EventId = Id;
                c.Date = DateTime.Now;
                c.UserId = query.StudentID;
                c.Text = comment;

                db.EventComment.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public bool removeComment(long Id)
        {
            try
            {
                var comment = db.EventComment.Find(Id);
                db.EventComment.Remove(comment);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public JsonResult GetComments(long Id)
        {
            //var comments = db.EventComment.Where(e => e.EventId == Id).Join(ac.Users, e => e.UserId, u => u.Id, (e, u) => new
            //{
            //    Username = u.Name + " " + u.Surname,
            //    E = e.Text,
            //    Date = e.Date
            //}).AsEnumerable().Select(x => new {
            //    // wanna create a dummy table
            //    //problem is EventComment 
            //    //do not want to use it like that "new EventComment"
            //    Username  = x.Username,
            //    E = x.E,
            //    Date = x.Date
            //    //Ok, give me 15 minutes I will try it and see if I can redo what I did yesterday
            //    //ok run it
            //    //Let me try it one more time 
            //});

            ////var users = (from u in ac.Users select new { Id = u.Id,
            ////    Name = u.Name + " " + u.Surname }).ToArray();
            var comments = from e in db.EventComment.AsEnumerable()
                           orderby e.Date descending
                           where e.EventId == Id
                    select new
                    {
                        Id = e.Id,
                        Username = "To Change",
                        UserId = e.UserId,
                        Text = e.Text,
                        Day = e.Date.Day,
                        Month = e.Date.ToString("MMMM"),
                        Year = e.Date.Year
                    };

            return Json(comments, JsonRequestBehavior.AllowGet);
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
