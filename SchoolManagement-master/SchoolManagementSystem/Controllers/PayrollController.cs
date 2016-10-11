using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class PayrollController : Controller

    {


        private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();


        public ActionResult Index()
        {
            var salaries = repository.Payrolls.ToList();
            // List<PayrollViewModel> model = new List<PayrollViewModel>();

            return View();
        }



        public ActionResult Details(int id)
        {
            return View();
        }



        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Payroll p )
        {
            var pay = repository.Payrolls.FirstOrDefault(x => x.Teacher.FirstName == p.Teacher.FirstName);
            

            return View();

        }



        public ActionResult Edit(int id)
        {
            return View();
        }



        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Delete(int id)
        {
            return View();
        }



        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
