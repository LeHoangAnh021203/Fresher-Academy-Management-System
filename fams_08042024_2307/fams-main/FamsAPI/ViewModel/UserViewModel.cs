using System.Transactions;

namespace FamsAPI.ViewModel
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public int PermissionId { get; set; }
    }
}
