using Xunit;
using FluentAssertions;
using FakeItEasy;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.Services;

namespace FamsAPI.Tests.TrainingProramService
{
    public class AddSyllabusToTrainingProgramServiceTests
    {
        [Fact]
        public async Task AddSyllabusToTrainingProgram_Should_Return_True_When_Successful()
        {
            // Arrange
            var trainingProgramRepository = A.Fake<TrainingProgramRepository>();
            var syllabusRepository = A.Fake<SyllabusRepository>();
            var trainingProgramSyllabusRepository = A.Fake<TrainingProgramSyllabusRepository>();
            var classRepository = A.Fake<ClassRepository>();
            var trainingContentRepository = A.Fake<TrainingContentRepository>();

            var trainingProgramService = new TrainingProgramService(
                trainingProgramRepository,
                syllabusRepository,
                trainingProgramSyllabusRepository,
                classRepository, 
                trainingContentRepository 
            );
            var trainingProgramCode = "TP001";
            var topicCode = "TS001";

            A.CallTo(() => trainingProgramRepository.GetTrainingProgrambyTrainingCode(trainingProgramCode)).Returns(new TrainingProgram());
            A.CallTo(() => syllabusRepository.GetSyllabusByTopicCode(topicCode)).Returns(new Syllabus());
            A.CallTo(() => trainingProgramSyllabusRepository.Get(A<System.Linq.Expressions.Expression<System.Func<TrainingProgramSyllabus, bool>>>.Ignored)).Returns(null);
            A.CallTo(() => trainingProgramSyllabusRepository.SaveChangesAsync()).DoesNothing();

            // Act
            var result = await trainingProgramService.AddSyllabusToTrainingProgram(trainingProgramCode, topicCode);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task AddSyllabusToTrainingProgram_Should_Return_False_When_TrainingProgram_Not_Found()
        {
            // Arrange
            var trainingProgramRepository = A.Fake<TrainingProgramRepository>();
            var syllabusRepository = A.Fake<SyllabusRepository>();
            var trainingProgramSyllabusRepository = A.Fake<TrainingProgramSyllabusRepository>();
            var classRepository = A.Fake<ClassRepository>();
            var trainingContentRepository = A.Fake<TrainingContentRepository>();

            var trainingProgramService = new TrainingProgramService(trainingProgramRepository, syllabusRepository, trainingProgramSyllabusRepository, classRepository, trainingContentRepository);

            var trainingProgramCode = "TP001";
            var topicCode = "TS001";

            A.CallTo(() => trainingProgramRepository.GetTrainingProgrambyTrainingCode(trainingProgramCode)).Returns(null);

            // Act
            var result = await trainingProgramService.AddSyllabusToTrainingProgram(trainingProgramCode, topicCode);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task AddSyllabusToTrainingProgram_Should_Return_False_When_Syllabus_Not_Found()
        {
            // Arrange
            var trainingProgramRepository = A.Fake<TrainingProgramRepository>();
            var syllabusRepository = A.Fake<SyllabusRepository>();
            var trainingProgramSyllabusRepository = A.Fake<TrainingProgramSyllabusRepository>();
            var classRepository = A.Fake<ClassRepository>();
            var trainingContentRepository = A.Fake<TrainingContentRepository>();

            var trainingProgramService = new TrainingProgramService(trainingProgramRepository, syllabusRepository, trainingProgramSyllabusRepository, classRepository, trainingContentRepository);

            var trainingProgramCode = "TP001";
            var topicCode = "TS001";

            A.CallTo(() => trainingProgramRepository.GetTrainingProgrambyTrainingCode(trainingProgramCode)).Returns(new TrainingProgram());
            A.CallTo(() => syllabusRepository.GetSyllabusByTopicCode(topicCode)).Returns(null);

            // Act
            var result = await trainingProgramService.AddSyllabusToTrainingProgram(trainingProgramCode, topicCode);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task AddSyllabusToTrainingProgram_ValidInput_ReturnsTrue()
        {
            // Arrange
            var trainingProgramRepository = A.Fake<TrainingProgramRepository>();
            var syllabusRepository = A.Fake<SyllabusRepository>();
            var trainingProgramSyllabusRepository = A.Fake<TrainingProgramSyllabusRepository>();
            var classRepository = A.Fake<ClassRepository>();
            var service = new TrainingProgramService(trainingProgramRepository, syllabusRepository, trainingProgramSyllabusRepository, classRepository, null);

            // Mock input parameters
            string trainingProgramCode = "TP001";
            string topicCode = "TS001";

            // Mock repository method calls
            A.CallTo(() => trainingProgramRepository.GetTrainingProgrambyTrainingCode(trainingProgramCode)).Returns(new TrainingProgram());
            A.CallTo(() => syllabusRepository.GetSyllabusByTopicCode(topicCode)).Returns(new Syllabus());
            A.CallTo(() => trainingProgramSyllabusRepository.Get(A<System.Linq.Expressions.Expression<System.Func<TrainingProgramSyllabus, bool>>>.Ignored)).Returns(null);
            A.CallTo(() => trainingProgramSyllabusRepository.SaveChangesAsync()).DoesNothing();

            // Act
            var result = await service.AddSyllabusToTrainingProgram(trainingProgramCode, topicCode);

            // Assert
            result.Should().BeTrue();
        }
    }
}
