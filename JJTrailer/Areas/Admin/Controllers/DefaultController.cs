using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JJTrailer.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult threejs() { return View(); }
        public ActionResult threejsCanvas(string url) {
          //  ViewBag.url = "\\Threejs\\STL\\"+url+".stl";
            ViewBag.url = url;
            return View(); }

    }
}