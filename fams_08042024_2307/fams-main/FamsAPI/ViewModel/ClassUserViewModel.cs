using DataLayer.Entities;

namespace FamsAPI.ViewModel
{
    public class ClassUserViewModel
    {
        public Guid UserId { get; set; }
        public string ClassId { get; set; }
        public string? UserType { get; set; }
    }
}
