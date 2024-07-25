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
    public class LearningObjective
    {
        [Key]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(150, ErrorMessage = "Not longer than 150")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(100, ErrorMessage = "Not longer than 100")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public string? Description { get; set; }
        public virtual ICollection <SyllabusObjective>? SyllabusObjectives { get; set; }

        public virtual ICollection<TrainingContent>? TrainingContent { get; set; }
    }
}
