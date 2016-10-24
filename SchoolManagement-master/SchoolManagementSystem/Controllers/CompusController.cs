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
    public class CompusController : Controller
    {
        private DbSchoolContext db = new DbSchoolContext();

        // GET: /Compus/
        public ActionResult Index()
        {
            return View(db.Compus.ToList());
        }

        // GET: /Compus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compus compus = db.Compus.Find(id);
            if (compus == null)
            {
                return HttpNotFound();
            }
            return View(compus);
        }

        // GET: /Compus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Compus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="compus_id,compusnam")] Compus compus)
        {
            if (ModelState.IsValid)
            {
                db.Compus.Add(compus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(compus);
        }

        // GET: /Compus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compus compus = db.Compus.Find(id);
            if (compus == null)
            {
                return HttpNotFound();
            }
            return View(compus);
        }

        // POST: /Compus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="compus_id,compusnam")] Compus compus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compus);
        }

        // GET: /Compus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compus compus = db.Compus.Find(id);
            if (compus == null)
            {
                return HttpNotFound();
            }
            return View(compus);
        }

        // POST: /Compus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compus compus = db.Compus.Find(id);
            db.Compus.Remove(compus);
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
