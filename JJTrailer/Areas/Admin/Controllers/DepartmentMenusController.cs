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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Web.Hosting;
using System.Text;
namespace JJTrailer.Areas.Admin.Controllers
{
    public class DepartmentMenusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public class shortDepartmentMenu
        {
            public string name { get; set; }
            public string path { get; set; }
            public string imagepath { get; set; }
            public shortDepartmentMenu[] children { get; set; }
            
        }
        // GET: Admin/DepartmentMenus
        public ActionResult Index(bool generatemenu=false)
        {
            var departmentMenus = db.DepartmentMenus.Include(d => d.department).Include(d=>d.department.Categories);
            var dep = departmentMenus.GroupBy(s => s.department.departmentType);
            List<shortDepartmentMenu> temp = null;
            List<shortDepartmentMenu> temp2 = null;
            shortDepartmentMenu temp3 = null;
            shortDepartmentMenu temp4 = null;
            List<shortDepartmentMenu> temp5 = null;
            foreach (var item in dep)
            {
                ViewData[item.Key.ToString()] = item.OrderBy(s=>s.Order).ToList();
            }

            if (generatemenu)
            {
                temp = new List<shortDepartmentMenu>();
                temp2 = new List<shortDepartmentMenu>();
                temp3 = new shortDepartmentMenu();
                temp4 = new shortDepartmentMenu();
                temp5 = new List<shortDepartmentMenu>();
                foreach (var item in ViewData)
                {
                    //add department types
                    temp3 = new shortDepartmentMenu()
                    {
                        name = item.Key,
                        path = null,
                        imagepath=null,
                        children = null
                    };
                    temp.Add(temp3);
                    //add departments
                    foreach (var item2 in ViewData[item.Key.ToString()] as IEnumerable<DepartmentMenu>)
                    {
                        if (item2.show)
                        {
                            temp3=new shortDepartmentMenu()
                            {
                                name = item2.department.Name,
                                path = item2.DepartmentID.ToString(),
                                imagepath = item2.ImagePath,
                                children = new shortDepartmentMenu[]{ }
                            };

                            temp2 = new List<shortDepartmentMenu>();
                            //add categories
                        foreach(var item3 in item2.department.Categories.OrderBy(s=>s.Order).Where(s=>s.show==true).Where(S=>S.CategoryID==null))
                        {
                            if (item3.SubCategories != null && item3.SubCategories.Count() > 0)
                            {

                                temp4 = new shortDepartmentMenu()
                                {
                                    name = item3.Name,
                                    path = item3.ID.ToString(),
                                    imagepath = null,
                                    children = new shortDepartmentMenu[] { }
                                };
                                temp5= new List<shortDepartmentMenu>();

                                foreach (var item4 in item3.SubCategories.OrderBy(s=>s.Order).Where(s=>s.show==true))
                                {
                                    if (item4.SubCategories != null && item4.SubCategories.Count() > 0)
                                    {
                                        List<shortDepartmentMenu> tmp9 = new List<shortDepartmentMenu>();
                                        foreach (var item5 in item4.SubCategories)
                                        {
                                            tmp9.Add(new shortDepartmentMenu { name = item5.Name, path = item5.ID.ToString(), imagepath = null, children = null });
                                            
                                        }
                                        temp5.Add(new shortDepartmentMenu()
                                        {
                                            name = item4.Name,
                                            path = item4.ID.ToString(),
                                            imagepath = null,
                                            children = tmp9.ToArray()
                                        });


                                    }
                                    else { 
                                      temp5.Add(new shortDepartmentMenu()
                                    {
                                        name = item4.Name,
                                        path = item4.ID.ToString(),
                                        imagepath = null,
                                        children = null
                                    });
                                    }
                                  


                                }
                                temp4.children = temp5.ToArray();
                                temp2.Add(temp4);
                            }
                            else
                            {
                                temp2.Add(new shortDepartmentMenu()
                                {
                                    name = item3.Name,
                                    path = item3.ID.ToString(),
                                    imagepath = null,
                                    children = null
                                });
                            }
 
                        }
                           

                            temp3.children = temp2.ToArray();
                         temp.Add(temp3);
                        }
                    }
                    temp3 = new shortDepartmentMenu()
                    {
                        name = "",
                        path = "",
                        imagepath = null,
                        children = null
                    };
                    temp.Add(temp3);

            } 
            
                System.IO.File.WriteAllText(HostingEnvironment.ApplicationPhysicalPath + "/json/" + "Departmentmenu" + ".json", JsonConvert.SerializeObject(temp,formatting:Formatting.Indented));
                buildDepartmentHtml();
            }
            return View();
        }

        private void buildDepartmentHtml()
        {
         string Wholefile= "<nav2 id='mysidebarmenu' class='amazonmenu'><ul>";
         
         shortDepartmentMenu[] file = JsonConvert.DeserializeObject<shortDepartmentMenu[]>(System.IO.File.ReadAllText(HostingEnvironment.ApplicationPhysicalPath + "/json/" + "Departmentmenu" + ".json"));
         int numberofrows = file.Count();
         int numberofcolumns = 2;
         int numberofAllChildren = 0;
            
            foreach (shortDepartmentMenu dep in file)
            {
                numberofAllChildren = 0;
                if (dep.children!=null)
                foreach (shortDepartmentMenu dep2 in dep.children)
                {
                    numberofAllChildren++;
                    if (dep2.children != null)
                    foreach (shortDepartmentMenu dep3 in dep2.children)
                    {
                        numberofAllChildren++;
                    }
                }


             //super department
                if (dep.path == null)
                {
                    Wholefile += "<li><strong style='color:Red;'>"+dep.name+"</strong></li>";

                }
                    //department
                else if (dep.children != null)
                {
                    Wholefile += "<li><a href='" + dep.path + "'>";
                    Wholefile += "<p>" + dep.name + "</p></a>";
                    if (dep.children.Count() > 0)
                    {
                        Wholefile += "<div><div style='background: url(.."+dep.imagepath+") no-repeat;     background-position:100% 100%; width:100%;height:100%'><table>";
                        if (numberofAllChildren > numberofrows )
                        {
                            Wholefile += "<tr>";
                            int x = (dep.children.Count()-1) / numberofcolumns;
                            int rem = (dep.children.Count() - 1) % numberofcolumns;
                            int start = 0;
                            int end = x;
                            for (int j = 0; j < numberofcolumns; j++)
                            {
                                Wholefile += "<th>";
                                Wholefile = GetChildren(Wholefile, dep, start, end);
                                Wholefile += "</th>";
                                start = end;
                                end += x;
                                if ((end + rem) == (dep.children.Count() - 1))
                                    end = end + rem;
                            }
                                                            Wholefile += "</tr>";

                        }
                        else
                        {
                            //categories    
                            Wholefile += "<tr><th>";

                            Wholefile = GetChildren(Wholefile, dep, 0, dep.children.Count());
                            Wholefile += "</th></tr>";
                        }
                       // Wholefile += "<th><img src='~"+dep.imagepath+"'> </th></tr>";
                        Wholefile += "</table></div></div>";

                    }
                    Wholefile += "</li>";
                }
                    //horizontal line
                else {
                    Wholefile += "<li><hr style='margin:0;' /></li>";

                }
            }
        
         Wholefile+="</ul></nav2>";
        System.IO.File.WriteAllText(HostingEnvironment.ApplicationPhysicalPath + "/Views/" + "Shared/" + "_departmentmenu" + ".cshtml", Wholefile);
        }

        private static string GetChildren(string Wholefile, shortDepartmentMenu dep,int start,int end)
        {
            for (int i = start; i < end;i++ )
            {

                shortDepartmentMenu dep2 = dep.children[i];
                Wholefile += "<span><a href='" + dep2.path + "'>";
                if (dep2.children != null)
                    Wholefile += "<strong style='color:Red;'>" + dep2.name + "</strong>";
                else
                    Wholefile += "<p><strong>" + dep2.name + "</strong></p>";
                Wholefile += "</a></span>";

                //Sub-categories
                if (dep2.children != null)
                    foreach (shortDepartmentMenu dep3 in dep2.children)
                    {
                        Wholefile += "<span><a style='font-weight:lighter;' href='" + dep3.path + "'>";
                        Wholefile += "" + dep3.name + "";
                        Wholefile += "</a></span>";
                        if (dep3.children != null)
                        {
                            Wholefile += "<ul>";
                            foreach (shortDepartmentMenu dep4 in dep3.children)
                            {
                                 Wholefile += "<li><a style='font-weight:lighter;' href='" + dep4.path + "'>";
                                 Wholefile += "[" + dep4.name + "]";
                                 Wholefile += "</a></li>";   
                            }
                            Wholefile += "</ul>";
                        }
                    }
                Wholefile += "<p></p>";

            }
            return Wholefile;
        }
        [HttpGet]
        public ActionResult GetDep()
        {
            shortDepartmentMenu[] dep = JsonConvert.DeserializeObject<shortDepartmentMenu[]>(System.IO.File.ReadAllText(HostingEnvironment.ApplicationPhysicalPath + "/json/" + "Departmentmenu" + ".json"));

            return Json(dep,JsonRequestBehavior.AllowGet);
        }
   
  

        // GET: Admin/DepartmentMenus/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentMenu departmentMenu = await db.DepartmentMenus.FindAsync(id);
            if (departmentMenu == null)
            {
                return HttpNotFound();
            }
            return View(departmentMenu);
        }

        // GET: Admin/DepartmentMenus/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name");
            return View();
        }

        // POST: Admin/DepartmentMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DepartmentID,Order,show")] DepartmentMenu departmentMenu)
        {
            if (ModelState.IsValid)
            {
                departmentMenu.ID = Guid.NewGuid();
                db.DepartmentMenus.Add(departmentMenu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", departmentMenu.DepartmentID);
            return View(departmentMenu);
        }

        // GET: Admin/DepartmentMenus/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentMenu departmentMenu = await db.DepartmentMenus.FindAsync(id);
            if (departmentMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", departmentMenu.DepartmentID);
            return View(departmentMenu);
        }

        // POST: Admin/DepartmentMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DepartmentID,Order,show")] DepartmentMenu departmentMenu,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if(image!=null)
                {
                string physicalpath = HostingEnvironment.ApplicationPhysicalPath + "/Images/FrontPage/DepartmentMenu/";
                string virtualpath = HostingEnvironment.ApplicationVirtualPath + "/Images/FrontPage/DepartmentMenu/";
                string filename=Path.GetFileName(Request.Files[0].FileName);
                departmentMenu.ImagePath = Path.Combine(virtualpath, filename);

                uploadimage(image, Path.Combine(physicalpath, filename));

                }
                db.Entry(departmentMenu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", departmentMenu.DepartmentID);
            return View(departmentMenu);
        }

        private void uploadimage(HttpPostedFileBase image,string path)
        {
            foreach (string upload in Request.Files)
            {
                //if (!Request.Files[upload].HasFile()) continue;
                
                
                Request.Files[upload].SaveAs(path);
            }
        }

        // GET: Admin/DepartmentMenus/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentMenu departmentMenu = await db.DepartmentMenus.FindAsync(id);
            if (departmentMenu == null)
            {
                return HttpNotFound();
            }
            return View(departmentMenu);
        }

        // POST: Admin/DepartmentMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            DepartmentMenu departmentMenu = await db.DepartmentMenus.FindAsync(id);
            db.DepartmentMenus.Remove(departmentMenu);
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
