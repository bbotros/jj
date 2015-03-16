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
    public class CareersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Careers
        public async Task<ActionResult> Index()
        {
            var careers = db.Careers.Include(c => c.jobCategory).Include(c => c.jobLocatoin);
            return View(await careers.ToListAsync());
        }

        // GET: Admin/Careers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Career career = await db.Careers.FindAsync(id);
            if (career == null)
            {
                return HttpNotFound();
            }
            return View(career);
        }

        // GET: Admin/Careers/Create
        public ActionResult Create()
        {
            ViewBag.JobCategoryID = new SelectList(db.JobCategories, "ID", "JobCategoryName");
            ViewBag.JobLocatoinID = new SelectList(db.JobLocations, "ID", "ID");
            return View();
        }

        // POST: Admin/Careers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,JobTitle,JobDescription,JobLocatoinID,JobCategoryID,DatePosted,StillAvailable,employmentType,JobQualification")] Career career)
        {
            if (ModelState.IsValid)
            {
                career.ID = Guid.NewGuid();
                db.Careers.Add(career);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.JobCategoryID = new SelectList(db.JobCategories, "ID", "JobCategoryName", career.JobCategoryID);
            ViewBag.JobLocatoinID = new SelectList(db.JobLocations, "ID", "ID", career.JobLocatoinID);
            return View(career);
        }

        // GET: Admin/Careers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Career career = await db.Careers.FindAsync(id);
            if (career == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobCategoryID = new SelectList(db.JobCategories, "ID", "JobCategoryName", career.JobCategoryID);
            ViewBag.JobLocatoinID = new SelectList(db.JobLocations, "ID", "ID", career.JobLocatoinID);
            return View(career);
        }

        // POST: Admin/Careers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,JobTitle,JobDescription,JobLocatoinID,JobCategoryID,DatePosted,StillAvailable,employmentType,JobQualification")] Career career)
        {
            if (ModelState.IsValid)
            {
                db.Entry(career).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.JobCategoryID = new SelectList(db.JobCategories, "ID", "JobCategoryName", career.JobCategoryID);
            ViewBag.JobLocatoinID = new SelectList(db.JobLocations, "ID", "ID", career.JobLocatoinID);
            return View(career);
        }

        // GET: Admin/Careers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Career career = await db.Careers.FindAsync(id);
            if (career == null)
            {
                return HttpNotFound();
            }
            return View(career);
        }

        // POST: Admin/Careers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Career career = await db.Careers.FindAsync(id);
            db.Careers.Remove(career);
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
