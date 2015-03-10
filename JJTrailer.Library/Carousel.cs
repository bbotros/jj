using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace JJTrailer.Library
{
   public class Carousel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
       [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateAdded { get; set; }
        public virtual ICollection<ImageLib> CarouselCollection { get; set; }
    }
}
