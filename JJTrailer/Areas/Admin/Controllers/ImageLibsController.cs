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
    public class ImageLibsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ImageLibs
        public async Task<ActionResult> Index()
        {
          
           //ViewBag.subdir = subdir;
            return View(await db.ImageLibs.ToListAsync());
        }
        public ActionResult getimages(string path)
        {
            List<helperimages> items = new List<helperimages>();
            
            if(path==null)
             path = Directory.GetDirectories(HostingEnvironment.ApplicationPhysicalPath + "\\Images", "*").First();


            foreach (var imag2 in Directory.GetFiles(path))
            {
                var imag = imag2.Substring(imag2.IndexOf("\\Image"));
               items.Add(new helperimages()
                {
                    
                    src = imag,
                    srct=imag,
                    title=imag,
                    description=imag                  
                });
            }
            return Json(items);

        }

          //       src: 'demonstration/img_01.jpg',		// image url
          //srct: 'demonstration/img_01t.jpg',		// thumbnail url
          //title: 'image 1', 						// thumbnail title
          //description : 'image 1 description'		// thumbnail description
        public ActionResult gettree()
        {
            List<helperclass> imagedir = new List<helperclass>();
            int i = 0;
            //,SearchOption.AllDirectories
            foreach (var dir in Directory.GetDirectories(HostingEnvironment.ApplicationPhysicalPath + "\\Images", "*"))
            {
                var tmpdir = Directory.GetDirectories(dir);


                imagedir.Add(new helperclass()
                {
                    id = i++,
                    label = dir.Substring(dir.LastIndexOf('\\')),
                    inode = true ? tmpdir.Count() > 0 : false,
                    branch = getsubtree(tmpdir,ref i),
                    mainbranch = null,
                    open = false
                });
            }
            return Json(imagedir, JsonRequestBehavior.AllowGet);
        }
        public List<helperbranch> getsubtree(string[] tmpdir,ref int i)
        {
            List<helperbranch> tmpbranch=new List<helperbranch>();
            foreach (var item in tmpdir)
            {  
                tmpbranch.Add(new helperbranch()
                {
                    id = i++,
                    label = item.Substring(item.LastIndexOf('\\')),
                    inode = false,
                    myhash = item.Substring(item.LastIndexOf('\\')),
                    myurl=string.Empty
                });
                
            }
            return tmpbranch;
 
        }
        // GET: Admin/ImageLibs/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageLib imageLib = await db.ImageLibs.FindAsync(id);
            if (imageLib == null)
            {
                return HttpNotFound();
            }
            return View(imageLib);
        }

        // GET: Admin/ImageLibs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ImageLibs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FileName,FilePath")] ImageLib imageLib)
        {
            if (ModelState.IsValid)
            {
                imageLib.ID = Guid.NewGuid();
                db.ImageLibs.Add(imageLib);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(imageLib);
        }

        // GET: Admin/ImageLibs/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageLib imageLib = await db.ImageLibs.FindAsync(id);
            if (imageLib == null)
            {
                return HttpNotFound();
            }
            return View(imageLib);
        }

        // POST: Admin/ImageLibs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FileName,FilePath")] ImageLib imageLib)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imageLib).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(imageLib);
        }

        // GET: Admin/ImageLibs/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageLib imageLib = await db.ImageLibs.FindAsync(id);
            if (imageLib == null)
            {
                return HttpNotFound();
            }
            return View(imageLib);
        }

        // POST: Admin/ImageLibs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            ImageLib imageLib = await db.ImageLibs.FindAsync(id);
            db.ImageLibs.Remove(imageLib);
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
