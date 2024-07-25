using FamsAPI.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using FamsAPI.Services;
using static DataLayer.Entities.TrainingProgram;

namespace FamsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainingProgramController : Controller
    {
        private readonly ITrainingProgram _trainingProgram;
        public TrainingProgramController(ITrainingProgram trainingProgram)
        {
            _trainingProgram = trainingProgram;
        }

        //UserId will be get from ClaimsPrincipal.Identity
        [HttpPost]
        [Route("AddNewTrainingProgram")]
        [Authorize]
        public IActionResult AddNewTrainingProgram(string name, int duration, List<string> topic)
        {
            
                var user = HttpContext.User;

                var trainingProgram = _trainingProgram.AddNewTrainingProgramAsync(user, name, duration, topic);

                if (duration == 0) return BadRequest("Add failed!");
                return Ok("Add!");
            
        }

        #region Search TrainingProgram by Keyword
        [HttpGet("GetTrainingProgramByKeyword")]
        [Authorize]
        public IActionResult SearchTrainingProgram(string? keyword, string? createBy, string? createDate, int? duration, Statuses? status)
        {
            var searchResults = _trainingProgram.SearchTrainingPrograms(keyword, createBy, createDate, duration, status);

            // AC2: When the keyword is not matching any record, show announcement
            if (searchResults == null)
            {
                return NotFound("There's no record matching with your keyword.");
            }

            // AC1 and AC3: Return search results with applied filters
            return Ok(searchResults);
        }
        #endregion

        #region Delete Training Program
        [HttpDelete]
        [Route("Delete-training-program")]
        [Authorize]
        public async Task<IActionResult> DeleteTrainingProgram(string trainingCode)
        {
            var result = await _trainingProgram.DeleteTrainingProgram(trainingCode);
            if (result)
            {
                return Ok("Training program delete successfully");
            }
            else
            {
                return BadRequest("Failed to delete training program");
            }
        }
        #endregion

        #region Update Training Program
        [HttpPut]
        [Route("UpdateTrainingProgram")]
        [Authorize]
        public async Task<IActionResult> UpdateTrainingProgram(string trainingCode, string name)
        {
            var user = HttpContext.User;
            if (string.IsNullOrEmpty(trainingCode) || string.IsNullOrEmpty(name))
            {
                return BadRequest("Invalid input parameters");
            }

            await _trainingProgram.UpdateTrainingProgram(trainingCode, name, user);
            return Ok("Training program updated successfully");
        }
        #endregion

        #region Get All TrainingProgram
        [HttpGet]
        [Route("GetAllTrainingProgram")]
        [Authorize]
        public IActionResult GetAllTrainingProgram()
        {
            if (_trainingProgram == null)
            {
                return BadRequest("Training Program services is not properly initialized.");
            }
            else if (_trainingProgram.GetAllTrainingProgram() == null)
            {
                return Ok("Training Program is null");
            }
            else
            {
                return Ok(_trainingProgram.GetAllTrainingProgram().ToList());

            }
        }
        #endregion

        #region ViewTrainingProgramForTrainer
        [HttpGet("Trainer")]
        [Authorize]
        public IActionResult ViewTrainingProgramDetailTrainer(string programCode)
        {
            var trainingProgram = _trainingProgram.ViewDetailTrainingProgramTrainer(programCode);
            if (trainingProgram == null)
            {
                return BadRequest();
            }
            return Ok(trainingProgram);
        }
        #endregion

        #region ViewTrainingProgramForAdmin
        [HttpGet("Admin")]
        [Authorize]
        public IActionResult ViewTrainingProgramDetailAdmin(string programCode)
        {
            var trainingProgram = _trainingProgram.ViewDetailTrainingProgramAdmin(programCode);
            if (trainingProgram == null)
            {
                return BadRequest();
            }
            return Ok(trainingProgram);
        }
        #endregion

        #region RemoveSyllabusFromTrainingProgram
        [HttpDelete]
        [Route("RemoveSyllabusFromTrainingProgram")]
        [Authorize]
        public async Task<IActionResult> RemoveSyllabusFromTrainingProgram(string trainingProgramCode, string topicCode)
        {
            try
            {
                if (string.IsNullOrEmpty(trainingProgramCode) || string.IsNullOrEmpty(topicCode))
                {
                    return BadRequest("Invalid input parameters");
                }

                var result = await _trainingProgram.RemoveSyllabusFromTrainingProgram(trainingProgramCode, topicCode);
                if (result)
                {
                    return Ok("Syllabus removed from training program successfully");
                }
                else
                {
                    return NotFound("Syllabus or training program not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }
        #endregion

        #region AddSyllabusToTrainingProgram
        [HttpPost]
        [Route("AddSyllabusToTrainingProgram")]
        [Authorize]
        public async Task<IActionResult> AddSyllabusToTrainingProgram(string trainingProgramCode, string topicCode)
        {
            try
            {
                // Check the input parameters
                if (string.IsNullOrEmpty(trainingProgramCode) || string.IsNullOrEmpty(topicCode))
                {
                    return BadRequest("Invalid input parameters");
                }

                // Call the method in the service to add syllabus to the training program
                var result = await _trainingProgram.AddSyllabusToTrainingProgram(trainingProgramCode, topicCode);

                // Check the results and return the corresponding response
                if (result)
                {
                    return Ok("Syllabus added to training program successfully");
                }
                else
                {
                    return NotFound("Syllabus or training program not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        #endregion

        #region Update Status Training Program
        [HttpPut]
        [Route("UpdateStatusTrainingProgram")]
        [Authorize]
        public IActionResult UpdateStatus(string trainingProgramCode)
        {
            if(trainingProgramCode is null || trainingProgramCode == "")
            {
                return BadRequest("Parameter is null");
            }
            var checkTrainingProgramExisted = _trainingProgram.GetTrainingProgramByTrainingProgramCode(trainingProgramCode);
            if(checkTrainingProgramExisted == null)
            {
                return BadRequest("Training Program is not existed");
            }
            else
            {
                var user = HttpContext.User;
                _trainingProgram.UpdateTrainingProgramStatus(trainingProgramCode, user);
                return Ok("Update Successfully");
            }

        }

        #endregion

    }
}
