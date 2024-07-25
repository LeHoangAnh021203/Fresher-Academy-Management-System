using DataLayer.Entities;
using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FamsAPI.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Controllers.TrainingProgramControllers
{
    public class DeleteTrainingProgram
    {
        [Fact]
        public async Task DeleteTrainingProgram_Returns_Ok_When_TrainingProgram_Exists()
        {
            // Arrange
            var trainingProgramService = A.Fake<ITrainingProgram>();
            var trainingProgramController = new TrainingProgramController(trainingProgramService);
            var trainingCode = "code1";
            A.CallTo(() => trainingProgramService.DeleteTrainingProgram(trainingCode)).Returns(true);

            // Act
            var result = await trainingProgramController.DeleteTrainingProgram(trainingCode);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            A.CallTo(() => trainingProgramService.DeleteTrainingProgram(trainingCode)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteTrainingProgram_Returns_BadRequest_When_TrainingProgram_Does_Not_Exist()
        {
            // Arrange
            var trainingProgramService = A.Fake<ITrainingProgram>();
            var trainingProgramController = new TrainingProgramController(trainingProgramService);
            var trainingCode = "code2";
            A.CallTo(() => trainingProgramService.DeleteTrainingProgram(trainingCode)).Returns(false);

            // Act
            var result = await trainingProgramController.DeleteTrainingProgram(trainingCode);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            A.CallTo(() => trainingProgramService.DeleteTrainingProgram(trainingCode)).MustHaveHappenedOnceExactly();
        }
    }
}
