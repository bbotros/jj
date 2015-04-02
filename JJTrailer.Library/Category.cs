using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace JJTrailer.Library
{
    public class Category
    {
        public Guid ID { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }
         [Display(Name = "Parent Category")]
        public Guid? CategoryID { get; set; }
         public int Order { get; set; }
        public bool show { get; set; }
        [Display(Name = "Department Name")]
        public Guid DepartmentID { get; set; }
        public virtual Department department { get; set; }
        public virtual ICollection<Category> SubCategories{get;set;}
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Trailer> Trailers { get; set; }

        public  ICollection<ImageLib> CategoryImage { get; set; }
    }
}
