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

namespace JJTrailer.Areas.Admin.Controllers
{
    public class CustomTrailersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CustomTrailers
        public async Task<ActionResult> Index()
        {
            var customTrailers = db.CustomTrailers.Include(c => c.trailer);
            return View(await customTrailers.ToListAsync());
        }

        // GET: Admin/CustomTrailers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomTrailer customTrailer = await db.CustomTrailers.FindAsync(id);
            if (customTrailer == null)
            {
                return HttpNotFound();
            }
            return View(customTrailer);
        }

        // GET: Admin/CustomTrailers/Create
        public ActionResult Create()
        {
            ViewBag.TrailerID = new SelectList(db.Trailers, "ID", "TrailerName");
            return View();
        }

        // POST: Admin/CustomTrailers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,TrailerID")] CustomTrailer customTrailer)
        {
            if (ModelState.IsValid)
            {
                customTrailer.ID = Guid.NewGuid();
                db.CustomTrailers.Add(customTrailer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TrailerID = new SelectList(db.Trailers, "ID", "TrailerName", customTrailer.TrailerID);
            return View(customTrailer);
        }

        // GET: Admin/CustomTrailers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomTrailer customTrailer = await db.CustomTrailers.FindAsync(id);
            if (customTrailer == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrailerID = new SelectList(db.Trailers, "ID", "TrailerName", customTrailer.TrailerID);
            return View(customTrailer);
        }

        // POST: Admin/CustomTrailers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,TrailerID")] CustomTrailer customTrailer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customTrailer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TrailerID = new SelectList(db.Trailers, "ID", "TrailerName", customTrailer.TrailerID);
            return View(customTrailer);
        }

        // GET: Admin/CustomTrailers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomTrailer customTrailer = await db.CustomTrailers.FindAsync(id);
            if (customTrailer == null)
            {
                return HttpNotFound();
            }
            return View(customTrailer);
        }

        // POST: Admin/CustomTrailers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            CustomTrailer customTrailer = await db.CustomTrailers.FindAsync(id);
            db.CustomTrailers.Remove(customTrailer);
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
