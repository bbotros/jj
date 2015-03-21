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
using System.Web.Hosting;
using System.IO;

namespace JJTrailer.Areas.Admin.Controllers
{
    public class STLfilemanagersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/STLfilemanagers
        public async Task<ActionResult> Index()
        {
            ViewBag.STLfile = new SelectList(db.STLfilemanagers, "filepath", "filepath");
            ViewBag.url = "";
            
            return View(await db.STLfilemanagers.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult> Index(string STLfile)
        {
            ViewBag.STLfile = new SelectList(db.STLfilemanagers, "filepath", "filepath");
            ViewBag.url = STLfile;

            return View(await db.STLfilemanagers.ToListAsync());
        }
        public ActionResult threejCanvas(string url)
        {
            //  ViewBag.url = "\\Threejs\\STL\\"+url+".stl";
            ViewBag.url = url;
            return View();
        }
        // GET: Admin/STLfilemanagers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STLfilemanager sTLfilemanager = await db.STLfilemanagers.FindAsync(id);
            if (sTLfilemanager == null)
            {
                return HttpNotFound();
            }
            return View(sTLfilemanager);
        }

        // GET: Admin/STLfilemanagers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/STLfilemanagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,filepath")] STLfilemanager sTLfilemanager,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {        if(image!=null)
                {
                string physicalpath = HostingEnvironment.ApplicationPhysicalPath + "/ThreeJs/STL/";
                string virtualpath = HostingEnvironment.ApplicationVirtualPath + "/ThreeJs/STL/";
                string filename=Path.GetFileName(image.FileName);
                sTLfilemanager.filepath = Path.Combine(virtualpath, filename);
                image.SaveAs(Path.Combine(physicalpath, filename));
            }
                sTLfilemanager.ID = Guid.NewGuid();
                db.STLfilemanagers.Add(sTLfilemanager);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sTLfilemanager);
        }

        // GET: Admin/STLfilemanagers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STLfilemanager sTLfilemanager = await db.STLfilemanagers.FindAsync(id);
            if (sTLfilemanager == null)
            {
                return HttpNotFound();
            }
            return View(sTLfilemanager);
        }

        // POST: Admin/STLfilemanagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,filepath")] STLfilemanager sTLfilemanager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTLfilemanager).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sTLfilemanager);
        }

        // GET: Admin/STLfilemanagers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STLfilemanager sTLfilemanager = await db.STLfilemanagers.FindAsync(id);
            if (sTLfilemanager == null)
            {
                return HttpNotFound();
            }
            return View(sTLfilemanager);
        }

        // POST: Admin/STLfilemanagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            STLfilemanager sTLfilemanager = await db.STLfilemanagers.FindAsync(id);
            db.STLfilemanagers.Remove(sTLfilemanager);
            await db.SaveChangesAsync();
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
