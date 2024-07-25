using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace FamsAPI.Tests.TrainingProramControllers
{
    public class AddSyllabusToTrainingProgramControllerTests
    {
        [Fact]
        public async Task AddSyllabusToTrainingProgram_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            A.CallTo(() => fakeTrainingProgramService.AddSyllabusToTrainingProgram(A<string>._, A<string>._)).Returns(true);
            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = await controller.AddSyllabusToTrainingProgram("trainingCode", "topicCode") as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be("Syllabus added to training program successfully");
        }

        [Fact]
        public async Task AddSyllabusToTrainingProgram_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = await controller.AddSyllabusToTrainingProgram(null, "topicCode") as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Invalid input parameters");
        }

        [Fact]
        public async Task AddSyllabusToTrainingProgram_SyllabusOrTrainingProgramNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            A.CallTo(() => fakeTrainingProgramService.AddSyllabusToTrainingProgram(A<string>._, A<string>._)).Returns(false);
            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = await controller.AddSyllabusToTrainingProgram("trainingCode", "topicCode") as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
            result.Value.Should().Be("Syllabus or training program not found");
        }

        [Fact]
        public async Task AddSyllabusToTrainingProgram_ExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            A.CallTo(() => fakeTrainingProgramService.AddSyllabusToTrainingProgram(A<string>._, A<string>._)).Throws(new System.Exception("Something went wrong"));
            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = await controller.AddSyllabusToTrainingProgram("trainingCode", "topicCode") as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("An error occurred: Something went wrong");
        }
    }
}
