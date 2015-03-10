using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJTrailer.Library
{
   public class Product
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryID { get; set; }
        public virtual Category ProductCategory { get; set; }
        public ICollection<ImageLib> ProductImage { get; set; }
    }
}
