using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host.Mef;
using System.Net.WebSockets;
#pragma warning disable
namespace FamsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainingController : Controller
    {
        private readonly ITrainingUnit _unitService;
        private readonly ITrainingContent _contentService;
        private readonly ITrainingProgram _programService;
        public TrainingController(ITrainingUnit unitService, ITrainingContent contentService)
        {
            _unitService = unitService;
            _contentService = contentService;
        }

        #region Get All Units By Day
        [HttpGet]
        [Route("GetAllUnitsByDay/{day}")]
        [Authorize]
        public IActionResult GetAllUnitsByDay(int day)
        {
            var dayExisted = _unitService.GetTrainingUnitsByDay(day);
            /*if (dayExisted == null)
            {
                return BadRequest();
            }*/
            return Ok(dayExisted);
        }
        #endregion

        #region Get All Units By Topic
        [HttpGet]
        [Route("GetAllUnitsByTopicCode/{topicCode}")]
        [Authorize]
        public IActionResult GetAllUnitsByTopic(string topicCode)
        {
            var dayExisted = _unitService.GetTrainingUnitListByTopicCode(topicCode);
            /*if (dayExisted == null)
            {
                return BadRequest();
            }*/
            return Ok(dayExisted);
        }
        #endregion

        #region Get All Contents By UnitCode
        [HttpGet]
        [Route("GetAllContentByUnitCode/{unitCode}")]
        [Authorize]
        public IActionResult GetAllContentByUnitCode(string unitCode)
        {
            var unitCodeExisted = _contentService.GetAllTrainingContentByUnitCode(unitCode);
            /*if (unitCodeExisted == null)
            {
                return BadRequest();
            }*/
            return Ok(unitCodeExisted);
        }
        #endregion


        #region Add New Unit
        [HttpPost]
        [Route("AddNewUnit")]
        [Authorize]
        public IActionResult AddNewUnit(string unitName, int day, string topicCode)
        {
            bool success;
            string message;
            _unitService.AddUnit(unitName, day, topicCode, out success, out message);

            if (success)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest(message);
            }
        }
        #endregion

        [HttpDelete]
        [Route("RemoveUnit/{unitCode}")]
        [Authorize]
        public IActionResult RemoveUnitByUnitCode(string unitCode)
        {
            var checkUnitExisted = _unitService.GetTrainingUnitByUnitCode(unitCode);
            if(checkUnitExisted == null)
            {
                return BadRequest("Unit not found!");
            }
            else
            {
                _unitService.RemoveUnit(unitCode);
                return Ok("Remove successfully");
            }
        }

        #region Edit Unit by UnitCode
        [HttpPut]
        [Route("EditUnit")]
        [Authorize]
        public IActionResult EditUnit(string unitCode, string unitName)
        {
            var checkUnitCode = _unitService.GetTrainingUnitByUnitCode(unitCode);
            if (checkUnitCode == null)
            {
                return BadRequest("UnitCode not found!");
            }
            _unitService.EditUnit(unitCode, unitName);
            return Ok("Unit updated successfully.");
        }
        #endregion

        #region Add New Content
        [HttpPost]
        [Route("AddNewContent")]
        [Authorize]
        public IActionResult AddNewContent(string unitCode, string content, string code, int duration, string deliveryType, string trainingFormat, string note)
        {
            var existingUnit = _unitService.GetTrainingUnitByUnitCode(unitCode);
            if (existingUnit == null)
            {
                return BadRequest("UnitCode not found!");
            }
            _contentService.AddContent(unitCode, content, code, duration, deliveryType, trainingFormat, note);
            return Ok("Content added successfully.");
        }
        #endregion

        #region Edit Content By UnitCode
        [HttpPut]
        [Route("EditContentByUnitCode/{unitCode}/Contents")]
        [Authorize]
        public async Task<IActionResult> EditContentByUnitCode(string unitCode, [FromBody] TrainingContent trainingContent)
        {
            var checkUnitCode = _unitService.GetTrainingUnitByUnitCode(unitCode);
            if (checkUnitCode == null)
            {
                return BadRequest("UnitCode not found.");
            }

          
            string contentId = trainingContent.ContentId;
            var checkContentId = _contentService.GetTrainingContentByContentId(contentId);
            if (checkContentId == null)
            {
                return BadRequest("ContentId not found.");
            }

            await _contentService.EditContent(unitCode, contentId, trainingContent);
            return Ok("Content updated successfully.");
        }
        #endregion

        #region Delete Content By Content Id
        [HttpDelete]
        [Route("DeleteContentByContentId/{contentId}")]
        [Authorize]
        public IActionResult DeleteContentByContentId(string contentId)
        {
            var checkContentExisted = _contentService.GetTrainingContentByContentId(contentId);
            if (checkContentExisted == null)
            {
                return BadRequest("Content not found. Please try again!");
            }
            _contentService.DeleteContent(contentId);
            return Ok("Content deleted succesfully.");
        }
        #endregion

        #region Remove All Units and Contents By DAY
        [HttpDelete]
        [Route("RemoveByDay/{dayNumber}")]
        [Authorize]
        public IActionResult RemoveDay(int dayNumber, string topicCode)
        {
            var units = _unitService.GetTrainingUnitListByTopicCode(topicCode);
            if (units == null || units.Count == 0)
            {
                return BadRequest("Invalid topicCode.");
            }

            _unitService.RemoveDay(dayNumber, topicCode);
            return Ok("Day and associated units and contents removed successfully.");
        }
        #endregion
    }
}
