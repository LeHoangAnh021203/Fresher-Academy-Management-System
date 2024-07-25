using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class TrainingUnit
    {
        [Key]
        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? UnitCode { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(100, ErrorMessage = "Not longer than 100")]
        public string? UnitName { get; set; }
        [Required(ErrorMessage = "Not null"), NotNull]
        public int DayNumber { get; set; }

        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? TopicCode { get; set; }
        public virtual Syllabus? Syllabus { get; set; }
        public virtual ICollection<TrainingContent>? TrainingContents { get; set; }
    }
}
