using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Assessment
    {
        [Key]
        [StringLength(5, ErrorMessage = "Not longer than 5")]
        public string AssessmentID { get; set; }
        public int QuizCount { get; set; }
        public double QuizPercent { get; set; }
        public int AssignmentCount { get; set; }
        public double AssignmentPercent { get; set; }
        public double FinalTheoryPercent { get; set; }
        public double FinalPracticePercent { get; set; }
        public virtual Syllabus? Syllabus { get; set; }
    }
}
