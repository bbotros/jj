using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JJTrailer.Library;
using JJTrailer.Models;

namespace JJTrailer.Controllers
{
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Search(string id)
        {
            ViewBag.searchstring = id;
            return View(db.Careers.ToList());

        }
        public ActionResult JobLocations()
        {
            return View(db.JobLocations.ToList());

        }
        public ActionResult JobCategories()
        {
            return View(db.JobCategories.ToList());

        }
        public ActionResult ContactUs()
        {
            return View();

        }
        public ActionResult WhoWeAre()
        {
            return View();

        }
        public ActionResult Resume()
        {
            return View();

        }
    }
}