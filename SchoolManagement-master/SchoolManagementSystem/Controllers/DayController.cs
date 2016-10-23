using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class DayController : Controller
    {
        private DbSchoolContext db = new DbSchoolContext();

        // GET: /Day/
        public ActionResult Index()
        {
            var days = db.Days.Include(d => d.Copmus).Include(d => d.rooms).Include(d => d.Subject);
            return View(days.ToList());
        }

        // GET: /Day/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // GET: /Day/Create
        public ActionResult Create()
        {
            ViewBag.compus_id = new SelectList(db.Compus, "compus_id", "compusnam");
            ViewBag.room_id = new SelectList(db.rooms, "room_id", "roomname");
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subjects");
            return View();
        }

        // POST: /Day/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="day_id,days,subject_id,room_id,compus_id,time")] Day day)
        {
            if (ModelState.IsValid)
            {
                db.Days.Add(day);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.compus_id = new SelectList(db.Compus, "compus_id", "compusnam", day.compus_id);
            ViewBag.room_id = new SelectList(db.rooms, "room_id", "roomname", day.room_id);
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subjects", day.subject_id);
            return View(day);
        }

        // GET: /Day/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            ViewBag.compus_id = new SelectList(db.Compus, "compus_id", "compusnam", day.compus_id);
            ViewBag.room_id = new SelectList(db.rooms, "room_id", "roomname", day.room_id);
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subjects", day.subject_id);
            return View(day);
        }

        // POST: /Day/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="day_id,days,subject_id,room_id,compus_id,time")] Day day)
        {
            if (ModelState.IsValid)
            {
                db.Entry(day).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.compus_id = new SelectList(db.Compus, "compus_id", "compusnam", day.compus_id);
            ViewBag.room_id = new SelectList(db.rooms, "room_id", "roomname", day.room_id);
            ViewBag.subject_id = new SelectList(db.Subjects, "subject_id", "subjects", day.subject_id);
            return View(day);
        }
        [HttpGet]
        public ActionResult search()
        {
            var all = from g in db.Compus.ToList()
                      select g;
            return View();
        }
        public ActionResult search(string compus)
        {
            var all = from g in db.Compus.ToList()
                      select g;

            List<Compus> obj = new List<Compus>();
            foreach(var p in db.Compus)
            {
                if(p.compusnam.Equals(compus))
                {
                    obj.Add(p);
                }
            }
            if(obj.Count()==0)
            {
                return View("search");
            }
            else
            {
                if (!String.IsNullOrEmpty(compus))
                {
                    var result = db.Compus.ToList().Find(x => x.compusnam.Equals(compus));
                    TempData["compus"] = result;
                    return RedirectToAction("recod");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult recod()
        {
            string compus =  TempData["compus"] as string; ;
          ///  string applicant = TempData["applicantno"] as string;
            if(compus!=null)
            {
                var days = db.Days.Include(d => d.Copmus).Include(d => d.rooms).Include(d => d.Subject);
                return View(days.ToList().Where(b => b.Copmus.compusnam.Equals(compus)));

                //var all = db.Days.ToList().Where(b => b.Copmus.compusnam.Equals(compus));
                //return View(all);
            }
            return View();
        }



        // GET: /Day/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // POST: /Day/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Day day = db.Days.Find(id);
            db.Days.Remove(day);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
