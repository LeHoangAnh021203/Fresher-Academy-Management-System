using FamsAPI.IServices;
using FamsAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FamsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainingCalendarController : Controller
    {
        private readonly ITrainingCalendar _trainingCalendarService;

        public TrainingCalendarController(ITrainingCalendar trainingCalendarService)
        {
            _trainingCalendarService = trainingCalendarService;
        }

        #region Get All Training Calendars
        [HttpGet]
        [Route("GetAllCalendars")]
        [Authorize]
        public IActionResult GetAllCalendars()
        {
            var allCalendars = _trainingCalendarService.GetAllTrainingCalendars();
            return Ok(allCalendars);
        }
        #endregion

        #region Add New Training Calendar
        [HttpPost]
        [Route("AddNewTrainingCalendar")]
        [Authorize]
        public async Task<IActionResult> AddNewTrainingCalendar(List<InputTrainingCalendarViewModel> inputTrainingCalendarViewModels)
        {
            var newTrainingCalendars = await _trainingCalendarService.AddNewTrainingCalendar(inputTrainingCalendarViewModels);

            if (newTrainingCalendars.Count > 0)
            {
                return Ok(newTrainingCalendars); 
            }
            else
            {
                return BadRequest("No training calendars were added!"); 
            }
        }
        #endregion

        #region Get Training Calendar By ClassId
        [HttpGet]
        [Route("GetTrainingCalendarByClassId")]
        [Authorize]
        public IActionResult GetTrainingCalendarByClassId(string classId)
        {
            var existedCalendar = _trainingCalendarService.GetTrainingCalendarByClassId(classId);
            if (existedCalendar == null)
            {
                return BadRequest("ClassId is not exist. Try again");
            }
            return Ok(existedCalendar);
 ;        }
        #endregion
    }
}
