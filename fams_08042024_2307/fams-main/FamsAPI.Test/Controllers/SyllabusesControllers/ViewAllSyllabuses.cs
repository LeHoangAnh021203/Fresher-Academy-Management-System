using DataLayer.Entities;
using DataLayer;
using FamsAPI.Controllers;
using FamsAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamsAPI.IServices;
using FakeItEasy;

namespace FamsAPI.Test.Controllers.SyllabusesControllersTests
{
    public class ViewAllSyllabuses
    {
        private readonly ISyllabus _fakeSyllabusService;
        private readonly SyllabusController _controller;
        private readonly IAssessment _fakeAssessmentService;


        public ViewAllSyllabuses()
        {
            _fakeSyllabusService = A.Fake<ISyllabus>();
            _fakeAssessmentService = A.Fake<IAssessment>();
            _controller = new SyllabusController( _fakeAssessmentService, _fakeSyllabusService);
        }

        [Fact]
        public void GetAllSyllabuses_WhenSyllabusesExist_ReturnsOk()
        {
            // Arrange
            var syllabuses = new List<object> {
                new Syllabus { TopicCode = "1", TopicName = "C#", CreatedBy = "NamPT", CreatedDate = DateTime.Now },
                new Syllabus { TopicCode = "2", TopicName = "Java", CreatedBy = "NgocTB", CreatedDate = DateTime.Now }

            };
            A.CallTo(() => _fakeSyllabusService.GetAllSyllabuses()).Returns(syllabuses);

            // Act
            var result = _controller.GetAllSyllabuses();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllSyllabuses_WhenServiceIsNull_ReturnsBadRequest()
        {
            // Arrange
            A.CallTo(() => _fakeSyllabusService.GetAllSyllabuses()).Returns(null);

            // Act
            var result = _controller.GetAllSyllabuses();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
