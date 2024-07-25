using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class UpdateUserViewModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public DateTime? DOB { get; set; }

        public User.Genders Gender { get; set; }

        public string? ModifiedBy { get; set; }

        public User.UserStatus Status { get; set; }

        public int permissionId { get; set; }
    }
}
