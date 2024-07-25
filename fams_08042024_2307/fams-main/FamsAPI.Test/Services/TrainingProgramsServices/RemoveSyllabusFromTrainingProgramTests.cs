using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer;
using FakeItEasy;
using FamsAPI.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace FamsAPI.Tests.TrainingProramService
{
    public class RemoveSyllabusFromTrainingProgramServiceTests
    {
        [Fact]
        public async Task RemoveSyllabusFromTrainingProgram_Should_Return_True_When_Syllabus_Removed_Successfully()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FAMSDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new FAMSDBContext(options))
            {
                // Seed some data for testing
                var trainingProgram = new TrainingProgram { TrainingProgramCode = "TP001", Name = "Sample Training Program" };

                // Initialize Syllabus with all required properties
                var syllabus = new Syllabus
                {
                    TopicCode = "TS001",
                    CourseObjective = "Sample course objective",
                    TechnicalGroup = "Sample technical group",
                    TechnicalRequirement = "Sample technical requirement",
                    TopicName = "Sample topic name",
                    TopicOutline = "Sample topic outline",
                    TrainingAudience = "Sample training audience"
                };

                var trainingProgramSyllabus = new TrainingProgramSyllabus { TrainingProgramCode = "TP001", TopicCode = "TS001" };

                context.TrainingPrograms.Add(trainingProgram);
                context.Syllabuses.Add(syllabus);
                context.TrainingProgramSyllabuses.Add(trainingProgramSyllabus);
                context.SaveChanges();

                var trainingProgramSyllabusRepository = new TrainingProgramSyllabusRepository(context);

                var trainingProgramRepository = new TrainingProgramRepository(context);
                var syllabusRepository = new SyllabusRepository(context);
                var classRepository = new ClassRepository(context);
                var trainingContentRepository = new TrainingContentRepository(context); 

                var trainingProgramService = new TrainingProgramService(trainingProgramRepository, syllabusRepository, trainingProgramSyllabusRepository, classRepository, trainingContentRepository);

                var trainingProgramCode = "TP001";
                var topicCode = "TS001";

                // Act
                var result = await trainingProgramService.RemoveSyllabusFromTrainingProgram(trainingProgramCode, topicCode);

                // Assert
                result.Should().BeTrue();
                // Assert that the mapping is removed from the database
                context.TrainingProgramSyllabuses.Should().NotContain(tps => tps.TrainingProgramCode == trainingProgramCode && tps.TopicCode == topicCode);
            }
        }

        [Fact]
        public async Task RemoveSyllabusFromTrainingProgram_Should_Return_False_When_Syllabus_Not_Found()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FAMSDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new FAMSDBContext(options))
            {
                var trainingProgramSyllabusRepository = new TrainingProgramSyllabusRepository(context); // Use real repository

                var trainingProgramRepository = A.Fake<TrainingProgramRepository>();
                var syllabusRepository = A.Fake<SyllabusRepository>();
                var classRepository = A.Fake<ClassRepository>();
                var trainingContentRepository = A.Fake<TrainingContentRepository>();

                var trainingProgramService = new TrainingProgramService(trainingProgramRepository, syllabusRepository, trainingProgramSyllabusRepository, classRepository, trainingContentRepository);

                var trainingProgramCode = "TP001";
                var topicCode = "TS001";

                // Act
                var result = await trainingProgramService.RemoveSyllabusFromTrainingProgram(trainingProgramCode, topicCode);

                // Assert
                result.Should().BeFalse();
            }
        }
    }
}
