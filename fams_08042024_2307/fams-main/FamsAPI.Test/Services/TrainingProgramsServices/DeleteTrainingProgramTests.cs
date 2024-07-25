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

namespace FamsAPI.Test.Services.TrainingProgramServices
{
    public class DeleteTrainingProgramTests
    {
        [Fact]
        public async Task DeleteTrainingProgram_Returns_Success_When_TrainingProgram_Exists()
        {
            // Arrange
            var trainingProgramService = A.Fake<ITrainingProgram>();
            var trainingCode = "code1";
            A.CallTo(() => trainingProgramService.DeleteTrainingProgram(trainingCode)).Returns(true);

            // Act
            await trainingProgramService.DeleteTrainingProgram(trainingCode);

            // Assert
            A.CallTo(() => trainingProgramService.DeleteTrainingProgram(trainingCode)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void DeleteTrainingProgram_Throws_Exception_When_TrainingProgram_Does_Not_Exist()
        {
            // Arrange
            var trainingProgramService = A.Fake<ITrainingProgram>();
            var trainingCode = "code2";
            A.CallTo(() => trainingProgramService.DeleteTrainingProgram(trainingCode)).Returns(false);

            // Act
            Func<Task> act = async () => await trainingProgramService.DeleteTrainingProgram(trainingCode);

            // Assert
            act.Should().ThrowAsync<Exception>();
            A.CallTo(() => trainingProgramService.DeleteTrainingProgram(trainingCode)).MustHaveHappenedOnceExactly();
        }
    }
}
