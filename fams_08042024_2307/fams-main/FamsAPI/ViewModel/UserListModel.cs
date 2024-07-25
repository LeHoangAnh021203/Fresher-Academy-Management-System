using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class UserListModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNum {get; set;}
        public DateTime? DOB { get; set;}
        public Genders Gender { get; set;}
        public UserStatus Status { get; set;}
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
