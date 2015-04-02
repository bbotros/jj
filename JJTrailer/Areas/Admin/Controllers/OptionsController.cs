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
    public class OptionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Options
        public async Task<ActionResult> Index()
        {
            return View(await db.Options.ToListAsync());
        }

        // GET: Admin/Options/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Options options = await db.Options.FindAsync(id);
            if (options == null)
            {
                return HttpNotFound();
            }
            return View(options);
        }

        // GET: Admin/Options/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Options/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] Options options)
        {
            if (ModelState.IsValid)
            {
                options.ID = Guid.NewGuid();
                db.Options.Add(options);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(options);
        }

        // GET: Admin/Options/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Options options = await db.Options.FindAsync(id);
            if (options == null)
            {
                return HttpNotFound();
            }
            return View(options);
        }

        // POST: Admin/Options/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Options options)
        {
            if (ModelState.IsValid)
            {
                db.Entry(options).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(options);
        }

        // GET: Admin/Options/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Options options = await db.Options.FindAsync(id);
            if (options == null)
            {
                return HttpNotFound();
            }
            return View(options);
        }

        // POST: Admin/Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Options options = await db.Options.FindAsync(id);
            db.Options.Remove(options);
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
