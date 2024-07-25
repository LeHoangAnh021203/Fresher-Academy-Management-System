using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Azure.Messaging;
using Microsoft.IdentityModel.Tokens;
using static DataLayer.Entities.User;
#pragma warning disable
namespace FamsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUser _userServices;
        public UserController(IUser userServices)
        {
            _userServices = userServices;
        }

        #region Get All Users
        /// <summary>
		/// Get all users
		/// </summary>
		/// <returns></returns>
        [HttpGet]
        [Route("GetAllUser")]
        [Authorize]
        //[Authorize(Roles ="1")]
        public IActionResult GetAllUser()
        {
            if (_userServices == null)
            {
                return BadRequest("User services is not properly initialized.");
            }
            else
            {

                List<UserListModel> _usersList = new List<UserListModel>();
                var users = _userServices.GetAllUsers().ToList();
                foreach (var item in users)
                {
                    var _users = new UserListModel()
                    {
                        UserId = item.UserId,
                        Name = item.Name,
                        Email = item.Email,
                        PhoneNum = item.Phone,
                        DOB = item.DOB,
                        Gender = (UserListModel.Genders)item.Gender,
                        Status = (UserListModel.UserStatus)item.Status,
                        CreateBy = item.CreateBy,
                        CreateDate = item.CreateDate,
                        ModifiedBy = item.ModifiedBy,
                        ModifiedTime = item.ModifiedTime,
                        PermissionId = item.PermissionId
                    };
                    _usersList.Add(_users);
                }
                return Ok(_usersList);
            }
        }
        #endregion

        #region Get Users Filtered
        /// <summary>
		/// Get users by keyword and filter
		/// </summary>
		/// <returns></returns>
        [HttpGet]
        [Route("GetUserByKeyword")]
        [Authorize]
        public IActionResult GetUserByKey(string key, string filter)
        {
            var users = _userServices.GetUsersByKeyword(key, filter);
            if (users.IsNullOrEmpty())
            {
                return NotFound("Can not found any Users related with that");
            }
            List<UserListModel> _usersList = new List<UserListModel>();
            foreach (var item in users)
            {
                var _users = new UserListModel()
                {
                    UserId = item.UserId,
                    Name = item.Name,
                    Email = item.Email,
                    PhoneNum = item.Phone,
                    DOB = item.DOB,
                    Gender = (UserListModel.Genders)item.Gender,
                    Status = (UserListModel.UserStatus)item.Status,
                    CreateBy = item.CreateBy,
                    CreateDate = item.CreateDate,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedTime = item.ModifiedTime,
                    PermissionId = item.PermissionId
                };
                _usersList.Add(_users);
            }
            return Ok(_usersList);
        }
        #endregion

        #region Sort Users
        /// <summary>
		/// Sort users
		/// </summary>
		/// <returns></returns>
        [HttpGet]
        [Route("SortUsers")]
        [Authorize]
        public ActionResult<List<User>> GetUsers([Required] string sortBy = "name", [Required] string sortDir = "asc")
        {
            try
            {
                var users = _userServices.SortUsers(sortBy?.ToLower(), sortDir?.ToLower());
                return Ok(users);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        


        #region Update User Status
        [HttpPut("UpdateUserStatus/{UserId}")]
        [Authorize]
        public IActionResult UpdateUserStatus(Guid UserId)
        {
            try
            {
                var check = _userServices.GetUserById(UserId);
                if (check == null)
                {
                    return NotFound("User not found");
                }
                var user = _userServices.UpdateUserStatus(UserId);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Grant Permission
        [HttpPut]
        [Route("GrantPermission")]
        [Authorize]
        public IActionResult GrantPermission(UserViewModel model)
        {
            var User = new User
            {
                UserId = model.UserId,
                PermissionId = model.PermissionId
            };

            User = _userServices.GrantPermission(User);
            return Ok(User);
        }
        #endregion

        #region Create a new User
        [HttpPost]
        [Route("AddNewUser")]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult> AddUserBySuperAdmin(string name, string email, Genders genders, string phone, string dob, UserStatus status, int permissionId)
        {
            var currentUser = HttpContext.User;
            var addUser = await _userServices.AddNewUser(name, email, genders, phone, dob, status, permissionId, currentUser);
            if (addUser == "Add User Success!!!")
            {
                return Ok(addUser);
            }
            return BadRequest(addUser);
        }
        #endregion

        #region Update user
        [HttpPut]
        [Route("UpdateUser")]
        [Authorize]
        public IActionResult UpdateUser(UpdateUserViewModel user)
        {
            var updateUser = _userServices.UpdateUser(user);
            if (updateUser == null)
            {
                return BadRequest();
            }
            return Ok(updateUser);
        }
        #endregion


        #region Delete User
        [HttpPut]
        [Route("DeleteUser/{id}")]
        [Authorize]
        public IActionResult DeleteUserBySuperAdmin(Guid id)
        {
            var checkUser = _userServices.DeleteUser(id);
            if (!checkUser)              
            {
                return BadRequest();
            }
            return Ok();

        }
        #endregion


    }
}
