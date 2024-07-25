using FakeItEasy;
using FluentAssertions;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using DataLayer.Entities;

namespace FamsAPI.Tests.GettAllTrainingProramController
{
    public class TrainingProgramControllerTests
    {
        [Fact]
        public void GetAllTrainingProgram_Returns_Ok_With_TrainingPrograms()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            var trainingPrograms = new List<TrainingProgram>
            {
                new TrainingProgram { Id = 1, Name = "Training 1" },
                new TrainingProgram { Id = 2, Name = "Training 2" }
            };
            A.CallTo(() => fakeTrainingProgramService.GetAllTrainingProgram()).Returns(trainingPrograms);

            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = controller.GetAllTrainingProgram();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedTrainingPrograms = okResult.Value.Should().BeAssignableTo<List<TrainingProgram>>().Subject;
            returnedTrainingPrograms.Should().HaveCount(2);
            returnedTrainingPrograms.Should().Contain(trainingPrograms);
        }

        [Fact]
        public void GetAllTrainingProgram_Returns_BadRequest_When_Service_Is_Null()
        {
            // Arrange
            ITrainingProgram fakeTrainingProgramService = null;
            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = controller.GetAllTrainingProgram();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                  .Which.Value.Should().Be("Syllabus services is not properly initialized.");
        }

        [Fact]
        public void GetAllTrainingProgram_Returns_BadRequest_When_No_TrainingPrograms()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            A.CallTo(() => fakeTrainingProgramService.GetAllTrainingProgram()).Returns(null);

            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = controller.GetAllTrainingProgram();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                  .Which.Value.Should().Be("No syllabus!");
        }
    }
}
