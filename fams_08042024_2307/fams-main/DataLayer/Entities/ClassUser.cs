using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class ClassUser
    {
        public Guid UserId { get; set; }
        public string ClassId { get; set; }
        public string? UserType { get; set; }
        public virtual User User { get; set; }
        public virtual Class? Class { get; set; }
    }
}
