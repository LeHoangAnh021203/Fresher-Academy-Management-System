using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.IServices;
using FamsAPI.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace FamsAPI.Tests.GettAllTrainingProgramService
{
    public class TrainingProgramServiceTests
    {
        [Fact]
        public void GetAllTrainingProgram_Returns_List_Of_TrainingPrograms()
        {
            // Arrange
            var fakeTrainingProgramService = A.Fake<ITrainingProgram>();
            var trainingPrograms = new List<TrainingProgram>
            {
                new TrainingProgram { Id = 1, Name = "Training 1" },
                new TrainingProgram { Id = 2, Name = "Training 2" }
            };

            var mockTrainingProgramRepository = A.Fake<TrainingProgramRepository>();
            A.CallTo(() => mockTrainingProgramRepository.GetAll()).Returns(trainingPrograms);

            var service = new TrainingProgramService(mockTrainingProgramRepository, A.Dummy<TrainingProgramSyllabusRepository>());

            // Act
            var result = service.GetAllTrainingProgram();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(trainingPrograms);
        }

        [Fact]
        public void GetAllTrainingProgram_Throws_Exception_When_Repository_Fails()
        {
            // Arrange
            var mockTrainingProgramRepository = A.Fake<TrainingProgramRepository>();
            A.CallTo(() => mockTrainingProgramRepository.GetAll()).Throws<Exception>();

            var service = new TrainingProgramService(mockTrainingProgramRepository, A.Dummy<TrainingProgramSyllabusRepository>());

            // Act & Assert
            service.Invoking(s => s.GetAllTrainingProgram()).Should().Throw<Exception>();
        }

    }
}
