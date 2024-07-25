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
    public class SyllabusObjective
    {
        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? TopicCode { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? ObjectiveCode { get; set; }

        public virtual Syllabus? Syllabus { get; set; }
        public virtual LearningObjective? LearningObjective { get; set; }
        //public ICollection<Syllabus> Sylalabuses { get; set; }
        //public ICollection<LearningObjective> LearningObjectives { get; set; }
    }
}
