using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJTrailer.Library
{
    public class CustomTrailer
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid TrailerID { get; set; }
        public virtual Trailer trailer { get; set; }
        public virtual ICollection<Options> optionsSubset { get; set; }
        public virtual ICollection<ImageLib> imaglib { get; set; }
    }
}
