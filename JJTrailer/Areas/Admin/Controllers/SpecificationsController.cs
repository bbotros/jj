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
    public class SpecificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Specifications
        public async Task<ActionResult> Index()
        {
            return View(await db.Specifications.ToListAsync());
        }

        // GET: Admin/Specifications/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specifications specifications = await db.Specifications.FindAsync(id);
            if (specifications == null)
            {
                return HttpNotFound();
            }
            return View(specifications);
        }

        // GET: Admin/Specifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Specifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] Specifications specifications)
        {
            if (ModelState.IsValid)
            {
                specifications.ID = Guid.NewGuid();
                db.Specifications.Add(specifications);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(specifications);
        }

        // GET: Admin/Specifications/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specifications specifications = await db.Specifications.FindAsync(id);
            if (specifications == null)
            {
                return HttpNotFound();
            }
            return View(specifications);
        }

        // POST: Admin/Specifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Specifications specifications)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specifications).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specifications);
        }

        // GET: Admin/Specifications/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specifications specifications = await db.Specifications.FindAsync(id);
            if (specifications == null)
            {
                return HttpNotFound();
            }
            return View(specifications);
        }

        // POST: Admin/Specifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Specifications specifications = await db.Specifications.FindAsync(id);
            db.Specifications.Remove(specifications);
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
