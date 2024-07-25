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
    public class TrainingProgram
    {
        [Key]
        [Required(ErrorMessage = "Not null")]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? TrainingProgramCode { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(100, ErrorMessage = "Not longer than 100")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public Guid UserId { get; set; }
        //public int Id {  get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Not null"), NotNull]
        public int Duration { get; set; }
        public string? CreateBy { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        [Column("TrainingProgramStatus")]
        public Statuses Status { get; set; }
        public virtual Class? Class { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<TrainingProgramSyllabus>? TrainingProgramSyllabuses
        {
            get;
            set;
        }
        public enum Statuses
        {
            Active,
            Inactive,
        }
    }
}
