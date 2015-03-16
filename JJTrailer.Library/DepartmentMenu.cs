using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace JJTrailer.Library
{
    public class DepartmentMenu
    {
        public Guid ID { get; set; }
        public Guid DepartmentID { get; set; }
        
        [Index("Order", IsUnique = true)]
        public int Order { get; set; }
        public string ImagePath { get; set; }
        public virtual Department department { get; set; }
        public bool show { get; set; }
    }
}
