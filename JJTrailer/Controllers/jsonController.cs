using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.Hosting;
namespace JJTrailer.Controllers
{
    public class jsonController : Controller
    {
        // GET: json
        [HttpGet]
        public ActionResult Index(string id,bool? isJson=true)
        {
            StreamReader fl = new StreamReader(HostingEnvironment.ApplicationPhysicalPath+"/json/"+id+".json");
            var o3=JToken.Parse(fl.ReadToEnd().Trim());
            // read JSON directly from a file
            //JsonTextReader reader = new JsonTextReader(fl);
           
            // JArray o2 = (JArray)JToken.ReadFrom(reader);
            
                ViewBag.json = o3.ToObject(typeof(helperclass[]));
            if (isJson.Value)
            {
                var v = Json(ViewBag.json, JsonRequestBehavior.AllowGet);              
                 return v;
            }
            else
                return View();
        }
    }
}
public class helperimages
{

    public string src { get; set; }
    public string srct { get; set; }

    public string title { get; set; }

    public string description { get; set; }

}
public class helperclass
{

    public int id { get; set; }
    public string label { get; set; }
    public bool inode { get; set; }
    public bool open { get; set; }
    public List<helperbranch> branch { get; set; }
    public List<helperclass> mainbranch { get; set; }

}
public class helperbranch {
    public int id { get; set; }
    public string label { get; set; }
    public bool inode { get; set; }
    public string myhash { get; set; }

    public string myurl { get; set; }

}