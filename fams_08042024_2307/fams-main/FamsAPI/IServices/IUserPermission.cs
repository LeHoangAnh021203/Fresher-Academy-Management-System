using DataLayer.Entities;
using FamsAPI.ViewModel;

namespace FamsAPI.IServices
{
    public interface IUserPermission
    {
        public ICollection<UserPermission> GetAll();
        Task<string> PermissionMatrix(List<UserPermissionViewModel> permissionViewModels);
    }
}
