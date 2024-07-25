using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Fsu
    {
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string FsuId { get; set; }
        [StringLength(6, ErrorMessage = "Not longer than 3")]
        public string FsuName { get; set;}
        public virtual ICollection<Class>? ClassFsuses { get; set;}
    }
}
