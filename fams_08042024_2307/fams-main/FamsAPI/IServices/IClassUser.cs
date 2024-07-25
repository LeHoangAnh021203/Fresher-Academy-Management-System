using DataLayer.Entities;
using FamsAPI.ViewModel;

namespace FamsAPI.IServices
{
    public interface IClassUser
    {
        public List<ClassUserViewModel> GetAllClassUser();
        public void AddClassUser(List<ClassUserViewModel> classUsers);
    }
}
