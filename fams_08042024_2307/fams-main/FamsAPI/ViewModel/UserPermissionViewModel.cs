using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class UserPermissionViewModel
    {
       
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public Permissions Syllabus { get; set; }
        public Permissions LearningMaterial { get; set; }
        public Permissions TrainingProgram { get; set; }
        public Permissions Class { get; set; }
        public Permissions UserManagement { get; set; }
    }
}
