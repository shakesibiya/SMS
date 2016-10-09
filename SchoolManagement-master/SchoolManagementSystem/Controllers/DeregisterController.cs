using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Deregister.Controllers
{
    public class DeregisterController : Controller
    {
        private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();
        public ActionResult Index()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var students = db.Deregister.ToList();
            List<StudentTrushView> model = new List<StudentTrushView>();

            foreach (var item in students)
            {
                //var studentClass = repository.Classes.FirstOrDefault(x => x.Id == item.Class_Id);

                model.Add(new StudentTrushView() { StudentTrush = item});
            }

            return View(model);
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
