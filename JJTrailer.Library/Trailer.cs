using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJTrailer.Library
{
    public class Trailer
    {
        public Guid ID { get; set; }
        public string TrailerName { get; set; }
        public virtual ICollection<Specifications> specifications { get; set; }
        public virtual ICollection<Options> options { get; set; }
        public Guid CategoryID { get; set; }
        public virtual ICollection<ImageLib> imaglib { get; set; }

    }
}
