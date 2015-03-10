using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJTrailer.Library
{
    public class Category
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid? CategoryID { get; set; }
        public virtual ICollection<Category> SubCategories{get;set;}
        public virtual ICollection<Product> Products { get; set; }
        public  ICollection<ImageLib> CategoryImage { get; set; }
    }
}
