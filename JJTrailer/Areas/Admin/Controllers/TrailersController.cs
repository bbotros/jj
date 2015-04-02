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
    public class TrailersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Trailers
        public async Task<ActionResult> Index()
        {
            return View(await db.Trailers.ToListAsync());
        }

        // GET: Admin/Trailers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer trailer = await db.Trailers.FindAsync(id);
            if (trailer == null)
            {
                return HttpNotFound();
            }
            return View(trailer);
        }

        // GET: Admin/Trailers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Trailers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TrailerName,CategoryID")] Trailer trailer)
        {
            if (ModelState.IsValid)
            {
                trailer.ID = Guid.NewGuid();
                db.Trailers.Add(trailer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(trailer);
        }

        // GET: Admin/Trailers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer trailer = await db.Trailers.FindAsync(id);
            if (trailer == null)
            {
                return HttpNotFound();
            }
            return View(trailer);
        }

        // POST: Admin/Trailers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,TrailerName,CategoryID")] Trailer trailer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trailer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trailer);
        }

        // GET: Admin/Trailers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trailer trailer = await db.Trailers.FindAsync(id);
            if (trailer == null)
            {
                return HttpNotFound();
            }
            return View(trailer);
        }

        // POST: Admin/Trailers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Trailer trailer = await db.Trailers.FindAsync(id);
            db.Trailers.Remove(trailer);
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
