using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Domain;

namespace SchoolManagementSystem.Controllers
{
    public class timetableController : Controller
    {
        private DbSchoolContext db = new DbSchoolContext();

        // GET: /timetable/
        public ActionResult Index()
        {
            var timetables = db.timetables.Include(t => t.Compus);
            return View(timetables.ToList());
        }

        // GET: /timetable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable timetable = db.timetables.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // GET: /timetable/Create
        public ActionResult Create()
        {
            ViewBag.compus_id = new SelectList(db.Compus, "compus_id", "compusnam");
            return View();
        }

        // POST: /timetable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="timetable_id,compus_id")] timetable timetable)
        {
            if (ModelState.IsValid)
            {
                db.timetables.Add(timetable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.compus_id = new SelectList(db.Compus, "compus_id", "compusnam", timetable.compus_id);
            return View(timetable);
        }

        // GET: /timetable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable timetable = db.timetables.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            ViewBag.compus_id = new SelectList(db.Compus, "compus_id", "compusnam", timetable.compus_id);
            return View(timetable);
        }

        // POST: /timetable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="timetable_id,compus_id")] timetable timetable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.compus_id = new SelectList(db.Compus, "compus_id", "compusnam", timetable.compus_id);
            return View(timetable);
        }

        // GET: /timetable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable timetable = db.timetables.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // POST: /timetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            timetable timetable = db.timetables.Find(id);
            db.timetables.Remove(timetable);
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
