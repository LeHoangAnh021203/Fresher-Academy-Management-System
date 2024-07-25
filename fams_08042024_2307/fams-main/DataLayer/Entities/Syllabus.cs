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
    public class Syllabus
    {
        [Key]
        [Required(ErrorMessage = "Topic Code is not null"), NotNull]
        [StringLength(10, ErrorMessage = "Topic Code is not longer than 10")]
        public string? TopicCode { get; set; }

        [Required(ErrorMessage = "Topic Name is not null"), NotNull]
        [StringLength(100, ErrorMessage = "Topic Name is not longer than 100")]
        public string? TopicName { get; set; }

        [Required(ErrorMessage = "Technical Group is not null"), NotNull]
        [StringLength(100, ErrorMessage = "Technical Group is not longer than 100")]
        public string? TechnicalGroup { get; set; }

        [StringLength(int.MaxValue)]
        [Required(ErrorMessage = "Technical Requirement is not null"), NotNull]
        public string TechnicalRequirement { get; set; }

        [StringLength(int.MaxValue)]
        [Required(ErrorMessage = "Course Objective is not null"), NotNull]
        public string CourseObjective { get; set; }

        [Required(ErrorMessage = "Version is not null"),]
        public int Version { get; set; }

        [Required(ErrorMessage = "Training Audience is not null"), NotNull]
        [StringLength(100, ErrorMessage = "Training Audience is not longer than 100")]
        public string? TrainingAudience { get; set; }

        [Required(ErrorMessage = "Topic Outline is not null")]
        public string? TopicOutline { get; set; }

        public string? TrainingMaterials { get; set; }

        public string? TrainingPrinciple { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public Priorities Priority { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public PulishStatuses PulishStatus { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Guid UserId { get; set; }

        public string? AssessmentID { get; set; }
        public virtual User? User { get; set; }
        public virtual Assessment? Assessment { get; set; }
        public virtual ICollection<TrainingProgramSyllabus>? TrainingProgramSyllabuses { get; set; }
        public virtual ICollection<SyllabusObjective>? SyllabusObjectives { get; set; }
        public virtual ICollection<TrainingUnit>? TrainingUnits { get; set; }

        public enum Priorities
        {
            Low,
            Mid,
            High
        }

        public enum PulishStatuses
        {
            Denied,
            Editing,
            Pending,
            Published
        }

    }
}
