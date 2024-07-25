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
    public class Class
    {

        [Key]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? ClassID { get; set; }

        [ForeignKey("TrainingProgram")]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? TrainingProgramCode { get; set; }

        [StringLength(100, ErrorMessage = "Not longer than 100")]
        public string? ClassName { get; set; }
        
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? ClassCode { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public int Status { get; set; }
        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        [Column("Location")]
        public string LocationId { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        [Column("FSU")]
        public string FsuId { get; set; }
   
        [Required(ErrorMessage = "Not null"), NotNull]
        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public string? ModifiedBy { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public DateTime ModifiedDate { get; set; }

        public virtual TrainingProgram? TrainingProgram { get; set; }
        public virtual Location? GetLocation {  get; set; }
        public virtual Fsu? GetFsu { get; set; }
        public virtual ICollection<ClassUser>? ClassUsers { get; set; }

        public virtual ICollection<TrainingCalendar>? TrainingCalendars { get; set; }

        public enum ClassStatus
        {
            Planning,
            Scheduled,
            Opening,
            Completed
        }
    }
}
