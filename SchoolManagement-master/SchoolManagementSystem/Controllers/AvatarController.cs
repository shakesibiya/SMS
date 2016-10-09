using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class AvatarController : Controller
    {
        private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        public ActionResult UploadAvatar(long Id)
        {
            return View(Id);
        }

        [HttpPost]
        public ActionResult UploadAvatar(long Id, HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/imgRepo/profilePic/" + Id + file.FileName);

            Avatar p = new Avatar();

            p.StudentID = (int)Id;
            p.FileName = Id + file.FileName;

            db.Avatars.Add(p);
            db.SaveChanges();
            file.SaveAs(path);

            return RedirectToAction("Index", "Secretary");
        }
    }
}
