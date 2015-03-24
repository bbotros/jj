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
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Categories
        public async Task<ActionResult> Index(int numberofpages=0,int currentpage=1,int pagesize=10)
        {
            ViewBag.DepartmentType =typeof( DepartmentType);
            ViewBag.Department = new List<Department> { };
            ViewBag.ParentCategories = new List<Category> { };
            ViewBag.categories = db.Categories.ToList();
            var categories = db.Categories.Include(c => c.department);

            if (categories.Count() > pagesize)
            {
                numberofpages = categories.Count() / pagesize;
                if (categories.Count() % pagesize > 0) pagesize++;
                categories = categories.OrderBy(s=>s.Name)
                    .Skip((currentpage-1)*pagesize).Take(pagesize);
            }

            ViewBag.currentpage = currentpage;
            ViewBag.numberofpages = numberofpages;
            ViewBag.pagesize = pagesize;
            return View(await categories.ToListAsync());
        }
        [HttpPost]
        public ActionResult Index(string DepartmentType, string Department, string ParentCategories, string searchterm, string filtered)
        {
            ViewBag.categories = db.Categories.ToList();
            ViewBag.DepartmentType = typeof(DepartmentType);
            var categories = db.Categories.Include(c => c.department);
            if (searchterm != null)
            {
                categories = categories.Where(s => s.Name.Contains(searchterm));
                ViewBag.Department = new List<Department> { };
                ViewBag.ParentCategories = new List<Category> { };
                if (filtered==null)
                    return View(categories.ToList());
            }

            if (DepartmentType != null && Department==null)
            {
                DepartmentType tmp;
                Enum.TryParse<DepartmentType>(DepartmentType, out tmp);
                
                    ViewBag.Department = db.Departments.Where(s => s.departmentType == tmp);
                    ViewBag.ParentCategories = new List<Category> { };
                    categories = categories.Where(s => s.department.departmentType == tmp);

              
                ViewBag.select1 = DepartmentType;
                ViewBag.select2 = null;
                ViewBag.select3 = null;


            }
             else if (Department != null && ParentCategories==null)
            {

                Guid tmp;
                if (Guid.TryParse(Department, out tmp))
                {


                    var tmp2 = db.Categories.Where(s => s.CategoryID == null && s.DepartmentID == tmp).ToList();
                    ViewBag.ParentCategories = tmp2;
                    try
                    {
                        DepartmentType tmp3 = db.Departments.Find(tmp).departmentType;
                        ViewBag.Department = db.Departments.Where(s => s.departmentType == tmp3);
                    }
                    catch
                    {
                        ViewBag.Department = new List<Department> { };
                    }
                    categories = categories.Where(s => s.DepartmentID == tmp);

                }
                else {

                    ViewBag.Department = new List<Department> { };
                    ViewBag.ParentCategories = new List<Category> { };
                    ViewBag.select1 = DepartmentType;
                    ViewBag.select2 = null;
                    ViewBag.select3 = null;
                }

                    ViewBag.select1 = DepartmentType;
                    ViewBag.select2 = Department;
                    ViewBag.select3 = null;
                    
            }
            else if (ParentCategories != null)
            {
                Guid tmp;
                if (Guid.TryParse(ParentCategories, out tmp)) 
                {

                try
                {
                    DepartmentType tmp3 = db.Categories.Find(tmp).department.departmentType;
                    var tmp4 = db.Categories.Find(tmp).DepartmentID;
                    var tmp2 = db.Categories.Where(s => s.CategoryID == null && s.DepartmentID == tmp4).ToList();

                    ViewBag.Department = db.Departments.Where(s => s.departmentType == tmp3);
                    ViewBag.ParentCategories = tmp2;
                    
                    DepartmentType = tmp3.ToString();
                    Department = tmp4.ToString();


                }
                catch
                {
                    ViewBag.Department = new List<Department> { };
                }
                categories = categories.Where(s => s.CategoryID == tmp);

                }
                else
                {

                    ViewBag.Department = new List<Department> { };
                    ViewBag.ParentCategories = new List<Category> { };

                }




                ViewBag.select1 = DepartmentType;
                ViewBag.select2 = Department;
                ViewBag.select3 = ParentCategories;

            }
            else
            {
                ViewBag.Department = new List<Department> { };
                ViewBag.ParentCategories = new List<Category> { };
                ViewBag.select1 = DepartmentType;
                ViewBag.select2 = Department;
                ViewBag.select3 = null;
            }

       
            return View(categories.ToList());
 
        }
        // GET: Admin/Categories/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");

            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,CategoryID,DepartmentID")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.ID = Guid.NewGuid();
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", category.DepartmentID);
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", category.DepartmentID);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,CategoryID,DepartmentID,Order,show")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", category.DepartmentID);
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Category category = await db.Categories.FindAsync(id);
            db.Categories.Remove(category);
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
