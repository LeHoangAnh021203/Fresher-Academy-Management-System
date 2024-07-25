
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FamsAPI.ViewModel
{
    public class UserCreate
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string Phone { get; set; }
        public string DOB { get; set; }
        public Genders Gender { get; set; }
        public UserStatus Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public int PermissionId { get; set; }

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
