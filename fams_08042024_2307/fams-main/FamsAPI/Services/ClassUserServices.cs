using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.ViewModel;

namespace FamsAPI.Services
{
    public class ClassUserServices : IClassUser
    {
        private readonly ClassUserRepository _classUserRepository;
        public ClassUserServices(ClassUserRepository classUserRepository)
        {
            _classUserRepository = classUserRepository;
        }
        public List<ClassUserViewModel> GetAllClassUser()
        {
            try
            {
                var classUserEntities = _classUserRepository.GetAll().ToList();
                var classUserViewModels = classUserEntities.Select(c => new ClassUserViewModel
                {
                    UserId = c.UserId,
                    ClassId = c.ClassId,
                    UserType = c.UserType
                }).ToList();

                return classUserViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to Get All ClassUsers.", ex);
            }
        }
        public void AddClassUser(List<ClassUserViewModel> classUsers)
        {
            try
            {
                var classUserEntities = classUsers
                    .Select(c => new ClassUser
                    {
                        UserId = c.UserId,
                        ClassId = c.ClassId,
                        UserType = c.UserType
                    })
                    .ToList();
                _classUserRepository.AddRange(classUserEntities);
                _classUserRepository.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to add ClassUsers.", ex);
            }
        }

        
    }
}
