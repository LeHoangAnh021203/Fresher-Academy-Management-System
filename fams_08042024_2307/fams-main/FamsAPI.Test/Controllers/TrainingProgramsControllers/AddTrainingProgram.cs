using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FamsAPI.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Controllers.TrainingProgramsControllers
{
    public class AddTrainingProgram
    {
        [Fact]
        public async Task AddNewTrainingProgram_Returns_OkObjectResult_WhenTrainingProgramIsAddedSuccessfully()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            var controller = new TrainingProgramController(fakeTrainingProgramService);
            var fakeUser = A.Fake<ClaimsPrincipal>();

            var name = "New Training Program";
            var duration = 5;
            var topics = new List<string> { "Topic 1", "Topic 2" };

            // Set up the HttpContext.User to use the fake ClaimsPrincipal
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = fakeUser }
            };

            // Act
            var actionResult = controller.AddNewTrainingProgram(name, duration, topics);

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)actionResult;
            okResult.Value.Should().Be("Add!");
        }

        [Fact]
        public async Task AddNewTrainingProgram_Returns_BadRequestObjectResult_WhenDurationIsZero()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            var controller = new TrainingProgramController(fakeTrainingProgramService);
            var fakeUser = A.Fake<ClaimsPrincipal>();

            var name = "New Training Program";
            var duration = 0; // Zero duration
            var topics = new List<string> { "Topic 1", "Topic 2" };

            // Set up the HttpContext.User to use the fake ClaimsPrincipal
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = fakeUser }
            };

            // Act
            var actionResult = controller.AddNewTrainingProgram(name, duration, topics);

            // Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = actionResult as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Add failed!");
        }

    }
}
