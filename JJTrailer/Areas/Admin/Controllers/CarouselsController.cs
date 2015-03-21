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
    public class CarouselsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Carousels
        public async Task<ActionResult> Index()
        {
            ViewBag.carouselview = new SelectList(db.Carousels, "ID", "Name");
          
            return View(await db.Carousels.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult> Index(string carouselview)
        {   List<ImageLib> tmp =null;
            Guid result=new Guid();
            if ( carouselview != null)
            {
                if(Guid.TryParse(carouselview,out result))
                tmp= db.Carousels.Find(Guid.Parse(carouselview)).CarouselCollection.ToList();
                if (tmp != null)
                    generatemenufn(tmp);
            }
            ViewBag.carouselview = new SelectList(db.Carousels, "ID", "Name");

            return View(await db.Carousels.ToListAsync());
        }
        private void generatemenufn(List<ImageLib> tmp)
        {
            //List<ImageLib> tmp=db.Carousels.Find(Carousels).CarouselCollection.ToList();

             string wholefile ="<div id='carousel-homepage' class='carousel slide' data-ride='carousel'>";
               wholefile += "<ol class='carousel-indicators'>";
               for (int i = 0; i < tmp.Count();i++ )
               {
                   if (i == 0)
                       wholefile += "<li data-target='#carousel-homepage' data-slide-to='" + i.ToString() + "' class='active'></li>";
                   else
                   wholefile += "<li data-target='#carousel-homepage' data-slide-to='"+i.ToString()+"'></li>";
               }
               wholefile += " </ol><div class='carousel-inner' role='listbox'>";
              for (int i = 0; i < tmp.Count();i++ )
               {
                  if(i==0)
                      wholefile += "<div class='item imgLiquidFill imgLiquid active' style='width:100%; height:400px;'>";
                  else
                      wholefile += "<div class='item imgLiquidFill imgLiquid' style='width:100%; height:400px;'>";

                      wholefile+="<img src='~"+tmp[i].FilePath+"'alt='...'  >";
                      wholefile+="<div class='carousel-caption'>";
                      wholefile += tmp[i].FileName;     
                       wholefile+="</div></div>";
            }

            wholefile += "</div><a class='left carousel-control' href='#carousel-homepage' role='button' data-slide='prev'>";
            wholefile+="<span class='glyphicon glyphicon-chevron-left' aria-hidden='true'></span>";
            wholefile+="<span class='sr-only'>Previous</span></a>";
            wholefile+="<a class='right carousel-control' href='#carousel-homepage' role='button' data-slide='next'>";
            wholefile+="<span class='glyphicon glyphicon-chevron-right' aria-hidden='true'></span>";
            wholefile += "<span class='sr-only'>Next</span></a></div>";
            System.IO.File.WriteAllText(HostingEnvironment.ApplicationPhysicalPath + "/Views/" + "Shared/" + "_carousel" + ".cshtml", wholefile);
        }

        // GET: Admin/Carousels/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carousel carousel = await db.Carousels.FindAsync(id);
            if (carousel == null)
            {
                return HttpNotFound();
            }
            return View(carousel);
        }

        // GET: Admin/Carousels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Carousels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,DateAdded")] Carousel carousel, HttpPostedFileBase[] image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    string physicalpath = HostingEnvironment.ApplicationPhysicalPath + "/Images/FrontPage/Carousel/";
                    string virtualpath = HostingEnvironment.ApplicationVirtualPath + "/Images/FrontPage/Carousel/";
                    
                        
                    carousel.CarouselCollection = uploadimage(image,physicalpath, virtualpath);

                }
                carousel.ID = Guid.NewGuid();
                db.Carousels.Add(carousel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(carousel);
        }
        private List<ImageLib> uploadimage(HttpPostedFileBase[] image, string physicalpath,string virtualpath)
        {
            List<ImageLib> carouselimages = new List<ImageLib>();
            ImageLib tmp = null;
            
            foreach (HttpPostedFileBase upload2 in image)
            {
                
                //if (!Request.Files[upload].HasFile()) continue;
                tmp = new ImageLib();
                tmp.FileName = Path.GetFileName(upload2.FileName);
                tmp.FilePath = Path.Combine(virtualpath, upload2.FileName);
                tmp.ID = Guid.NewGuid();
                carouselimages.Add(tmp);

                upload2.SaveAs(Path.Combine(physicalpath, tmp.FileName));
            }
            return carouselimages;
        }
        // GET: Admin/Carousels/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carousel carousel = await db.Carousels.FindAsync(id);
            if (carousel == null)
            {
                return HttpNotFound();
            }
            return View(carousel);
        }

        // POST: Admin/Carousels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,DateAdded")] Carousel carousel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carousel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(carousel);
        }

        // GET: Admin/Carousels/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carousel carousel = await db.Carousels.FindAsync(id);
            if (carousel == null)
            {
                return HttpNotFound();
            }
            return View(carousel);
        }

        // POST: Admin/Carousels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Carousel carousel = await db.Carousels.FindAsync(id);
            db.Carousels.Remove(carousel);
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
