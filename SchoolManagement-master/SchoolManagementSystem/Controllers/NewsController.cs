using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class NewsController : Controller
    {

        private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        // GET: News
        public ActionResult Index()
        {

            var posts = repository.News.ToList();
            return View(posts);

        }

        // GET: News/Details/5
        public ActionResult Details(int id)
        {
            PostView pv = new PostView();

            pv.Post = repository.News.Where(x => x.Id == id).Single();

            try
            {
                pv.Pics = repository.PostPictures.Where(x => x.PostId == pv.Post.Id).Single();
                //pv.Author = repository.Teachers.Where(x => x.)
            }catch (Exception)
            {
                pv.Pics = null;
            }
           

            return View(pv);
        }
    }
}
