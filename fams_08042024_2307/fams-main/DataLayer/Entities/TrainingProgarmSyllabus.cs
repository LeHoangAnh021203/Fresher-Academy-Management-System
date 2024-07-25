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
    public class TrainingProgramSyllabus
    {
        [Key]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string TopicCode { get; set; }

        [Key]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string TrainingProgramCode { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public int Sequence { get; set; }
        public virtual TrainingProgram? TrainingProgram { get; set; }
        public virtual Syllabus? Syllabus { get; set; }
    }
}
