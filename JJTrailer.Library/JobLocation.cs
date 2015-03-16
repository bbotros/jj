using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJTrailer.Library
{
    public class JobLocation
    {
        public Guid ID { get; set; }
        public Guid AddressID { get; set; }
        public virtual Address StoreAddress{ get; set; }
    }
}
