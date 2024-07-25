using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Location
    {
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string LocationId { get; set; }
        [StringLength(7, ErrorMessage = "Not longer than 7")]
        public string LocationName { get; set; }
        public virtual ICollection<Class>? Classes { get; set; }
    }
}
