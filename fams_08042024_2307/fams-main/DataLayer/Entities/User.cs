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
    public class User
    {
        //Define this is the primary key
        [Key]
        public Guid UserId { get; set; }

        //[Required(ErrorMessage = "Name is required"), NotNull]
        [StringLength(100, ErrorMessage = "User Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }
        //[Required(ErrorMessage = "Email is required")]
        [StringLength(256, ErrorMessage = "Email can not be longer than 256 characters.")]
        public string? Email { get; set; }
       
        [StringLength(16, ErrorMessage = "Not longer than 16")]
        public string? Password { get; set; }
        [RegularExpression(@"^(?:\+?84|0[3|5|7|8|9])\d{8}$", ErrorMessage = "Phone number must be (84)/(0)[3|5|7|8|9] and have 8 numbers after 84 or 0.")]
        //[Required(ErrorMessage = "Not null"), NotNull]
        public string? Phone { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? DOB { get; set; }

        public Genders Gender { get; set; }
        public UserStatus Status { get; set; }
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public int PermissionId { get; set; }
        public virtual UserPermission? UserPermission { get; set; }
        public virtual ICollection<ClassUser>? ClassUsers { get; set; }

        public virtual ICollection<TrainingProgram>? TrainingPrograms { get; set; }
        public virtual ICollection<Syllabus>? Syllabuses { get; set; }

        public enum Genders
        {
            Other,
            Male,
            Female
        }
        public enum UserStatus
        {
            Active,
            Deactive,
            Suspended
        }
    }
}
