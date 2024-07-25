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
    public class TrainingContent
    {
        [Key]
        [StringLength(150, ErrorMessage = "Not longer than 150")]
        public string ContentId { get; set; }

        [Column("ContentName")]
        [StringLength(300, ErrorMessage = "Not longer than 300")]
        public string? Content { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(50, ErrorMessage = "Not longer than 50")]
        public string? DeliveryType { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        [StringLength(50, ErrorMessage = "Not longer than 50")]
        public string? TrainingFormat { get; set; }

        [Required(ErrorMessage = "Not null"), NotNull]
        public string? Note { get; set; }

        [StringLength(10, ErrorMessage = "Not longer than 10")]
        public string? UnitCode { get; set; }

        public virtual TrainingUnit? TrainingUnit { get; set; }

        public virtual LearningObjective? LearningObjectives { get; set; }
    }
}
