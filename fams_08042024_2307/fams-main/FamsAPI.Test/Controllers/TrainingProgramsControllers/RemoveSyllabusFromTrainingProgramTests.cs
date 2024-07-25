using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace FamsAPI.Tests.TrainingProramControllers
{
    public class RemoveSyllabusFromTrainingProgramControllerTests
    {
        [Fact]
        public async Task RemoveSyllabusFromTrainingProgram_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            A.CallTo(() => fakeTrainingProgramService.RemoveSyllabusFromTrainingProgram(A<string>._, A<string>._)).Returns(true);
            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = await controller.RemoveSyllabusFromTrainingProgram("trainingCode", "topicCode") as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be("Syllabus removed from training program successfully");
        }

        [Fact]
        public async Task RemoveSyllabusFromTrainingProgram_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = await controller.RemoveSyllabusFromTrainingProgram(null, "topicCode") as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Invalid input parameters");
        }

        [Fact]
        public async Task RemoveSyllabusFromTrainingProgram_SyllabusOrTrainingProgramNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            A.CallTo(() => fakeTrainingProgramService.RemoveSyllabusFromTrainingProgram(A<string>._, A<string>._)).Returns(false);
            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = await controller.RemoveSyllabusFromTrainingProgram("trainingCode", "topicCode") as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
            result.Value.Should().Be("Syllabus or training program not found");
        }

        [Fact]
        public async Task RemoveSyllabusFromTrainingProgram_ExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            A.CallTo(() => fakeTrainingProgramService.RemoveSyllabusFromTrainingProgram(A<string>._, A<string>._)).Throws(new System.Exception("Something went wrong"));
            var controller = new TrainingProgramController(fakeTrainingProgramService);

            // Act
            var result = await controller.RemoveSyllabusFromTrainingProgram("trainingCode", "topicCode") as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("An error occurred: Something went wrong");
        }
    }
}
