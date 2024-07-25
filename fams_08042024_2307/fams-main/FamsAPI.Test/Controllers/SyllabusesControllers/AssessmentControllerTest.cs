using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FamsAPI.Test.Controllers.AssessmentControllers
{
    public class EditAssessmentTest
    {
        [Fact]
        public async Task EditAssessment_GoodCase()
        {
            // Arrange
            var assessmentService = A.Fake<IAssessment>();
            var syllabusService = A.Fake<ISyllabus>();
            var assessment = new AssessmentViewModel
            {
                AssessmentID = "AS001",
                AssignmentPercent = 10,
            };
            A.CallTo(() => assessmentService.EditAssessment(assessment)).Returns(Task.FromResult<AssessmentViewModel>(assessment));

            var controller = new SyllabusController(assessmentService,syllabusService);

            // Act
            var result = await controller.EditAssessment(assessment);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Update success.", okResult.Value);
        }

        [Fact]
        public async Task EditAssessment_BadCase()
        {
            // Arrange
            var assessmentService = A.Fake<IAssessment>();
            var syllabusService= A.Fake<ISyllabus>();
            var assessment = new AssessmentViewModel
            {
                AssessmentID = "AS002",
                AssignmentPercent = 20,
            };
            A.CallTo(() => assessmentService.EditAssessment(assessment)).Returns(Task.FromResult<AssessmentViewModel>(null));
            var controller = new SyllabusController(assessmentService,syllabusService);

            // Act
            var result = await controller.EditAssessment(assessment);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Assessment updated failed", badRequestResult.Value);
        }
    }
}
