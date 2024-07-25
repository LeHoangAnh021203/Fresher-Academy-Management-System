using DataLayer.Entities;
using FamsAPI.ViewModel;
using System.Security.Claims;

namespace FamsAPI.IServices
{
    public interface IClass
    {
        public List<ClassViewModel> GetAllClasses();
        public ClassViewModel GetClassById(string classId);
        public InputClassViewModel AddNewClass(InputClassViewModel newClassData, List<ClassUserViewModel> classUsers, ClaimsPrincipal user);
        public InputClassViewModel UpdateClass(InputClassViewModel updateClass, ClaimsPrincipal user);

    }
}
