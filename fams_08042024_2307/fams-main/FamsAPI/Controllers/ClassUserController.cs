using FamsAPI.IServices;
using FamsAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassUserController : ControllerBase
    {
        private readonly IClassUser _classUserService;
        public ClassUserController(IClassUser classUserService)
        {
            _classUserService= classUserService;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAllClassUsers() 
        {
            var allCS= _classUserService.GetAllClassUser();
            return Ok(allCS);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddNewClassUser(List<ClassUserViewModel> classUsers)
        {
            _classUserService.AddClassUser(classUsers);
            return Ok(classUsers);
        }
    }
}
