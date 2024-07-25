using Xunit;
using System;
using System.Linq.Expressions;
using FakeItEasy;
using FluentAssertions;
using FamsAPI.Services;
using FamsAPI.IServices;
using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer;
using AutoMapper;

namespace FamsAPI.Test.Services.SyllabusServices
{
    public class ViewSyllabusDetailTest
    {
        [Fact]
        public async Task ViewSyllabusDetail_Returns_Valid_SyllabusDetailViewModel()
        {
            // Arrange
            var key = "1";
            var fakeSyllabusRepository = A.Fake<SyllabusRepository>();
            var fakeTrainingUnitRepository = A.Fake<TrainingUnitRepository>();
            var fakeAssessmentRepository = A.Fake<AssessmentRepository>();
            var fakeTrainingContentRepository = A.Fake<TrainingContentRepository>();

            var expectedSyllabus = new Syllabus
            {
                TechnicalRequirement = "hihi",
                CourseObjective = "hoho",
                TrainingMaterials = "22",
                TrainingPrinciple = "77",
                TrainingUnits = new List<TrainingUnit>
        {
            new TrainingUnit
            {
                UnitCode = "2",
                UnitName = "gg",
                DayNumber = 5,
                TopicCode = "1",
                TrainingContents = new List<TrainingContent>
                {
                    new TrainingContent
                    {
                        ContentId = "1",
                        Content = "2",
                        Code = "3",
                        DeliveryType = "ready",
                        Duration = 5,
                    }
                }
            }
        },
            };

            var expectedAssessment = new Assessment
            {
                AssessmentID = "1",
                QuizCount = 4,
                QuizPercent = 5,
                AssignmentCount = 6,
                AssignmentPercent = 7,
                FinalPracticePercent = 8,
                FinalTheoryPercent = 9
            };

            A.CallTo(() => fakeSyllabusRepository.GetByKeyword(key)).Returns(expectedSyllabus);
            A.CallTo(() => fakeTrainingUnitRepository.GetByKeyword(key)).Returns(expectedSyllabus.TrainingUnits.First());
            A.CallTo(() => fakeAssessmentRepository.Get(A<Expression<Func<Assessment, bool>>>._)).Returns(expectedAssessment);
            A.CallTo(() => fakeTrainingContentRepository.GetByUnitCode(A<string>._))
    .Returns(expectedSyllabus.TrainingUnits.First().TrainingContents.ToList());

            var syllabusService = new FamsAPI.Services.SyllabusServices(
                fakeSyllabusRepository,
                fakeTrainingUnitRepository,
                A.Fake<LearningObjectiveRepository>(),
                fakeTrainingContentRepository,
                A.Fake<ITrainingUnit>(),
                A.Fake<ISyllabusObjective>(),
                A.Fake<IAssessment>(),
                fakeAssessmentRepository,
                A.Fake<FAMSDBContext>(),
                A.Fake<SyllabusObjectiveRepository>(),
                A.Fake<IMapper>()
            );

            // Act
            var result = await syllabusService.ViewSyllabusDetail(key);

            // Assert
            result.Should().NotBeNull();
            result.TechnicalRequirement.Should().Be(expectedSyllabus.TechnicalRequirement);
            result.CourseObjective.Should().Be(expectedSyllabus.CourseObjective);
            result.Assessment.Should().NotBeNull();
            result.Assessment.QuizCount.Should().Be(expectedAssessment.QuizCount);
            result.TrainingUnits.Should().NotBeNull();
            result.TrainingUnits.Should().HaveCount(1);
            result.TrainingUnits.First().TrainingContents.Should().NotBeNull();
            result.TrainingUnits.First().TrainingContents.Should().HaveCount(expectedSyllabus.TrainingUnits.First().TrainingContents.Count);
        }


        [Fact]
        public async Task ViewSyllabusDetail_Returns_Null_When_Syllabus_Not_Found()
        {
            // Arrange

            var key = "1";
            var fakeSyllabusRepository = A.Fake<SyllabusRepository>();
            var fakeTrainingUnitRepository = A.Fake<TrainingUnitRepository>();
            var fakeAssessmentRepository = A.Fake<AssessmentRepository>();

            // Setup fake repository to return null, simulating scenario when syllabus is not found
            A.CallTo(() => fakeSyllabusRepository.Get(A<Expression<Func<Syllabus, bool>>>._)).Returns(null);

            var syllabusService = new FamsAPI.Services.SyllabusServices(
                fakeSyllabusRepository,
                fakeTrainingUnitRepository,
                A.Fake<LearningObjectiveRepository>(),
                A.Fake<TrainingContentRepository>(),
                A.Fake<ITrainingUnit>(), // Pass the fake ITrainingUnit to the constructor
                A.Fake<ISyllabusObjective>(),
                A.Fake<IAssessment>(),
                fakeAssessmentRepository,
                A.Fake<FAMSDBContext>(),
                A.Fake<SyllabusObjectiveRepository>(),
                A.Fake<IMapper>()
            );

            // Act
            var result = await syllabusService.ViewSyllabusDetail(key);

            // Assert
            result.Assessment.AssessmentID.Should().BeNull();
        }

    }
}
