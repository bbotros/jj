using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJTrailer.Library
{
    public class Department
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual DepartmentType departmentType { get; set; }
    }
    public enum DepartmentType
    {
        Trailers=0,
        TrailerParts=1,
        TrailerService=2,
        CustomMetal=3,
        Other=4
        
    }
}
