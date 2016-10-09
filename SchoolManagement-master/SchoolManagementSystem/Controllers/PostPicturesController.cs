using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class PostPicturesController : Controller
    {

        //private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        // GET: PostPictures
        public ActionResult Index(long Id)
        {
            ViewBag.PostId = Id;
            return View();
        }
        public ActionResult Upload(long Id)
        {
            return View(Id);
        }

        [HttpPost]
        public ActionResult Upload(long Id, HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/imgRepo/PostRepo/" + Id + file.FileName);

            PostPictures p = new PostPictures();

            p.PostId = Id;
            p.FileName = Id + file.FileName;

            db.PostPicture.Add(p);
            db.SaveChanges();
            file.SaveAs(path);

            return RedirectToAction("Index", "News");
        }
    }
}