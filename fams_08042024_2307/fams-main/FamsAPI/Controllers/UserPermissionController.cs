using FamsAPI.IServices;
using FamsAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermission _userPermission;

        public UserPermissionController(IUserPermission userPermission)
        {
            _userPermission = userPermission;
        }
        #region Get All Permission
        [HttpGet]
        [Route("GetAllPermission")]
        //[Authorize(Policy = "AdminAccessPolicy")]
        public IActionResult GetAllPermission()
        {
            return Ok(_userPermission.GetAll());
        }

        #endregion

        #region Update Permission By Id
        [HttpPut]
        [Route("UpdatePermission")]
        [Authorize(Policy = "SuperAdmin")]
        /// <summary>
		/// Updater permission by ID
		/// </summary>
		/// <returns></returns>
        public IActionResult PermissionMatrix(List<UserPermissionViewModel> permissionViewModels)
        {
            if(permissionViewModels.Count  < 1)
            {
                return BadRequest("Data is empty");
            }
            else
            {
                return Ok(_userPermission.PermissionMatrix(permissionViewModels));
            }
            
        }
        #endregion
    }
}
