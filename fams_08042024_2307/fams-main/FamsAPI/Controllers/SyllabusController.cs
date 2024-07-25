using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
#pragma warning disable
namespace FamsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SyllabusController : ControllerBase
    {
        private readonly IAssessment _assessmentService;
        private readonly ISyllabus _syllabusService;
        
 

        public SyllabusController(IAssessment assessmentService, ISyllabus syllabusService)
        {
            _assessmentService = assessmentService;
            _syllabusService = syllabusService;
            
        }


        #region Get all syllabuses
        [HttpGet]
        [Route("GetAllSyllabuses")]
        [Authorize]
        public IActionResult GetAllSyllabuses()
        {
            if (_syllabusService == null)
            {
                return BadRequest("Syllabus services is not properly initialized.");
            }
            else if (_syllabusService.GetAllSyllabuses() == null)
            {
                return Ok(_syllabusService.GetAllSyllabuses().ToList());
            }
            else
            {
                return Ok(_syllabusService.GetAllSyllabuses().ToList());

            }
        }
        #endregion

        #region Get Assessment By AssessmentId
        [HttpGet]
        [Route("GetAssessmentById/{assessmentId}")]
        [Authorize]
        public IActionResult GetAssessmentById(string assessmentId)
        {
            var assessement = _assessmentService.GetAssessmentById(assessmentId);
            if (assessement == null)
            {
                return BadRequest("AssessmentId not found!");
            }
            return Ok(assessement);
        }
        #endregion

        #region Add New Assessment
        [HttpPost]
        [Route("AddNewAssessment")]
        [Authorize]
        public IActionResult AddNewAssessment(int quizCount, double quizPercent, int assignmentCount, double assignmentPercent, double fe, double pe)
        {
            _assessmentService.AddNew(quizCount, quizPercent, assignmentCount, assignmentPercent, fe, pe);
            return Ok("Add new assessment successfully.");
        }
        #endregion

        #region DuplicateSyllabus
        [HttpPost]
        [Route("DuplicateSyllabus")]
        [Authorize]
        public IActionResult DuplicateSyllabus(string keyword)
        {
            try
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    return BadRequest("Please input a TopicCode");
                }
                var data = _syllabusService.DuplicateSyllabus(keyword);
                if(data == null)
                {
                    return NotFound("A failure occurred while attempting to duplicate the syllabus as it was not found.");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Edit Assessment
        [HttpPut]
        [Route("EditAssessment")]
        [Authorize]
        public async Task<IActionResult> EditAssessment(AssessmentViewModel assessment)
        {
            var editAssessment = await _assessmentService.EditAssessment(assessment);

            if (editAssessment == null)
            {
                return BadRequest("Assessment updated failed");
            }
            else
            {
                return Ok("Update success.");
            }
        }

        #endregion

        #region Update Syllabus
        [HttpPut]
        [Route("UpdateSyllabus")]
        [Authorize]
        public async Task<IActionResult> UpdateSyllabus([FromBody] Syllabus syllabus)
        {
            var currentUser = HttpContext.User;
            var editSyllabus = await _syllabusService.UpdateSyllabus(syllabus, currentUser);
            if (editSyllabus != null)
            {                
                return Ok("Syllabus updated successfully");
            }
            else
            {
                return BadRequest("Syllabus updated failed");
            }
        }
        #endregion

        #region Search Syllabus by Keyword
        [HttpGet("SearchSyllabus/{keyword}")]
        [Authorize]
        public IActionResult SearchSyllabusbyKeyword(string keyword)
        {
            return Ok(_syllabusService.SearchSyllabusByKeyword(keyword));
        }
        #endregion

        #region Search Syllabus by Date
        [HttpGet("SearchSyllabuses/{CreatedDate}")]
        [Authorize]
        public IActionResult SearchSyllabusByDate(string CreatedDate)
        {
            return Ok(_syllabusService.SearchSyllabusByDate(CreatedDate));
        }
        #endregion

        #region Save Syllabus As Draft
        [NonAction]
        //[Authorize(Roles ="2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> SaveSyllabusAsDraft(Syllabus syllabus)
        {
            if (string.IsNullOrEmpty(syllabus.TopicName))
            {
                return BadRequest();
            }
            var currentUser = HttpContext.User;
            var ErrorMessage = await _syllabusService.SaveSyllabusAsDraftGeneral(syllabus, currentUser);
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessage);
            }

            return Ok("Created successfully");
        }
        #endregion

        #region Get All Assessment
        [HttpGet]
        [Route("GetAllAssessment")]
        [Authorize]
        public IActionResult GetAllAssessment()
        {
            return Ok(_assessmentService.GetAllAssessment());
        }
        #endregion

        #region View Syllabus Detail
        [HttpGet("ViewSyllabusDetail")]
        public IActionResult ViewSyllabusDetail(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            var result = _syllabusService.ViewSyllabusDetail(key);
            return Ok(result);
        }
        #endregion

        #region Import Syllabus 
        [HttpPost]
        [Route("Syllabus/Import")]
        [Authorize]
        public async Task<IActionResult> ImportSyllabus(IFormFile file)
        {
            var user = HttpContext.User;
            string uid = User.FindFirst("UserId")?.Value;
            var filePath = await SaveFile(file);
            // Check if file is null
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Check file extension
            if (Path.GetExtension(file.FileName).ToLower() != ".xlsx")
            {
                return BadRequest("Only .xlsx files are allowed.");
            }
            var errorMessage = await _syllabusService.GetDataFromExcelFile(filePath, user);
            if (errorMessage == null || errorMessage == "")
            {
                await DeleteRemoveFile(filePath);
                return Ok("The file convert and save successfully!");

            }
            else
            {
                return BadRequest(errorMessage);
            }
        }
        #endregion

        #region Add New Syllabus
        [HttpPost("AddNewSyllabus")]
        [Authorize]
        public async Task<IActionResult> AddNewSyllabusWithAssessmentAndUnit(string topicName, string technicalGroup, string trainingAudience, string topicOutline, string trainingMaterials, string trainingPrinciple, string courseObjective, string technicalRequirement, int quizCount, double quizPercent, int assignmentCount, double assignmentPercent, double fe, double pe, [FromBody] List<TrainingUnitViewModel> trainingUnits)
        {
            try
            {
                var currentUser = HttpContext.User;
                var result = await _syllabusService.AddNewSyllabusWithAssessmentAndUnit(topicName, technicalGroup, trainingAudience, topicOutline, trainingMaterials, trainingPrinciple, courseObjective, technicalRequirement, currentUser, quizCount, quizPercent, assignmentCount, assignmentPercent, fe, pe, trainingUnits);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Save Files
        /// <summary>
        /// Function will help the system save the file in temp folder and read it
        /// </summary>
        /// <param name="file">The file send from client to sever</param>
        /// <returns>Directory of the file after saving in temporary folder</returns>
        /// <exception cref="ArgumentException">In the case that file send is empty</exception>
        /// <exception cref="ApplicationException">In the case that while processing the name file got problem</exception>
        /// 
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<string> SaveFile(IFormFile file)
        {
            try
            {
                if (file == null) throw new ArgumentException("File is empty or null");
                var tempPath = Path.GetTempFileName();
                var tempDirectory = Path.GetDirectoryName(tempPath);
                var fileName = file.FileName;

                if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("File name is empty or null");

                var filePath = Path.Combine(tempDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("File processing failed", ex);
            }
        }

        #endregion


        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]

        private async Task<bool> DeleteRemoveFile(string filePath)
        {
            try
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
