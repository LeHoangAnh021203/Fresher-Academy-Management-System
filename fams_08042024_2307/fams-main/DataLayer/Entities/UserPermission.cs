using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class UserPermission
    {
        [Key]
        public int PermissionId { get; set; }
        [Required(ErrorMessage = "Name of permission is required")]
        [StringLength(100, ErrorMessage = "Name of permission is not longer than 100 characters.")]
        public string PermissionName { get; set; }

        //public Guid UserId { get; set; }

        public Permissions Syllabus { get; set; }
        public Permissions LearningMaterial { get; set; }
        public Permissions TrainingProgram { get; set; }
        public Permissions Class { get; set; }
        public Permissions UserManagement { get; set; }
        public int Version { get; set; }

        public virtual ICollection<User>? Users { get; set; }
    }
    public enum Permissions
    {
        AccessDenied,
        View,
        Modify,
        Create,
        FullAccess
    }
}
