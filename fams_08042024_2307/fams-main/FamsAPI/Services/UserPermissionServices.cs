using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer;
using FamsAPI.IServices;
using FamsAPI.ViewModel;
using AutoMapper;

namespace FamsAPI.Services
{
    public class UserPermissionServices : IUserPermission
    {
        //private readonly FAMSDBContext context;
        private readonly UserPermissionRepository _repository;
        private readonly IMapper _mapper;

        public UserPermissionServices(UserPermissionRepository repository, IMapper mapper)
        {
            //this.context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public ICollection<UserPermission> GetAll()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> PermissionMatrix(List<UserPermissionViewModel> permissionViewModels)
        {
            try
            {
                var permissionExistedList = _repository.GetAll().ToList();
                foreach (var permission in permissionViewModels)
                {
                    int role = permission.PermissionId;
                    var permissionExisted = permissionExistedList.FirstOrDefault(c => c.PermissionId == role);
                    var permissionUpdate = permission;
                    if (permissionExisted != null)
                    {
                        _mapper.Map(permissionUpdate, permissionExisted);
                        permissionExisted.Version++;
                        _repository.Update(permissionExisted);
                        _repository.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Fatal error! Role is null");
                    }

                }
                return "Update Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
