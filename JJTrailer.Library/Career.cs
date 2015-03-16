using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace JJTrailer.Library
{
   public class Career
    {
       public Guid ID { get; set; }
       public string JobTitle { get; set; }
       public string JobDescription { get; set; }
       public Guid JobLocatoinID { get; set; }
       public Guid JobCategoryID { get; set; }
       public virtual JobLocation jobLocatoin { get; set; }
       public virtual JobCategory jobCategory { get; set; }
       [Display(Name = "Date Posted")]
       public DateTime DatePosted { get; set; }
       public bool StillAvailable { get; set; }
       [Display(Name = "Employment Type")]
       public EmploymentType employmentType { get; set; }
       public string JobQualification { get; set; }

   }
   public enum EmploymentType
   {
       PartTime=0,
       FullTime=1,
       Contract=2
   }
}
