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
    public class JobLocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/JobLocations
        public async Task<ActionResult> Index()
        {
            var jobLocations = db.JobLocations.Include(j => j.StoreAddress);
            return View(await jobLocations.ToListAsync());
        }

        // GET: Admin/JobLocations/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobLocation jobLocation = await db.JobLocations.FindAsync(id);
            if (jobLocation == null)
            {
                return HttpNotFound();
            }
            return View(jobLocation);
        }

        // GET: Admin/JobLocations/Create
        public ActionResult Create()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetName");
            return View();
        }

        // POST: Admin/JobLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,AddressID")] JobLocation jobLocation)
        {
            if (ModelState.IsValid)
            {
                jobLocation.ID = Guid.NewGuid();
                db.JobLocations.Add(jobLocation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetName", jobLocation.AddressID);
            return View(jobLocation);
        }

        // GET: Admin/JobLocations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobLocation jobLocation = await db.JobLocations.FindAsync(id);
            if (jobLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetName", jobLocation.AddressID);
            return View(jobLocation);
        }

        // POST: Admin/JobLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,AddressID")] JobLocation jobLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobLocation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetName", jobLocation.AddressID);
            return View(jobLocation);
        }

        // GET: Admin/JobLocations/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobLocation jobLocation = await db.JobLocations.FindAsync(id);
            if (jobLocation == null)
            {
                return HttpNotFound();
            }
            return View(jobLocation);
        }

        // POST: Admin/JobLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            JobLocation jobLocation = await db.JobLocations.FindAsync(id);
            db.JobLocations.Remove(jobLocation);
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
