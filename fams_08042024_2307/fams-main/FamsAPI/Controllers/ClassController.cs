using DataLayer.Entities;
using FamsAPI.IServices;
using FamsAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FamsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IClass _classService;
     
        public ClassController(IClass classService)
        {
            _classService = classService;
        }

        #region Get All Classes
        [HttpGet]
        [Route("GetAllClasses")]
        [Authorize]
        public IActionResult GetAllClasses()
        {
            var allClass = _classService.GetAllClasses();
            /*if (allClass == null || !allClass.Any())
            {
                return Ok("Empty.");
            }*/

            return Ok(allClass);
        }
        #endregion

        #region Get Class By Id
        [HttpGet]
        [Route("GetClassById/{id}")]
        [Authorize]
        public IActionResult GetClassById(string id)
        {
            var existedClass = _classService.GetClassById(id);
            if(existedClass.ClassID == null)
            {
                return BadRequest("Empty.");
            }
            return Ok(existedClass);
        }
        #endregion

        #region Add New Class
        [HttpPost]
        [Authorize]
        [Route("AddNewClass")]
        public IActionResult AddNewClass(NewClassViewModel newClass)
        {
            var user = HttpContext.User;

            var addClass = _classService.AddNewClass(newClass.InputClass, newClass.ClassUser, user);
            if (addClass != null)
            {
                return Ok(addClass);
            }
            else
            {
                return BadRequest("Failed to add a new class. Please try again later.");
            }
        }
        #endregion

        #region Edit Class
        [HttpPut]
        [Authorize]
        [Route("EditClass")]
        public IActionResult EditClass(InputClassViewModel updatedClass)
        {
            var user = HttpContext.User; 
            var editClass = _classService.UpdateClass(updatedClass, user);
            if (editClass != null)
            {
                return Ok(editClass);
            }
            else
            {
                return BadRequest("Failed to edit a class. Please try again.");
            }
        }
        #endregion
    }
}
