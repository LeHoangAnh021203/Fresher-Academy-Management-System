using DataLayer.Entities;
using FamsAPI.ViewModel;
using System.Security.Claims;
using static DataLayer.Entities.User;

namespace FamsAPI.IServices
{
    public interface IUser
    {
        public List<User> GetAllUsers();

        public User GetUserById(Guid id);
        public User UpdateUserStatus(Guid userId);
        public List<User> SortUsers(string? sortBy, string? sortDir);
        public User GetUserByEmail(string email);

        public User GrantPermission(User user);
        public List<User> GetUsersByKeyword(String key, string filter);

        public User UserLogin(string email, string password);

        Task<string> AddNewUser(string name, string mail, Genders genders, string phone, string Dob, UserStatus status, int permissionId, ClaimsPrincipal user);

        public string UpdateUser(UpdateUserViewModel user);


        public Boolean DeleteUser(Guid key);

        public interface IUserRepository
        {
            void Add(User user);
            void SaveChanges();
        }

    }
}
