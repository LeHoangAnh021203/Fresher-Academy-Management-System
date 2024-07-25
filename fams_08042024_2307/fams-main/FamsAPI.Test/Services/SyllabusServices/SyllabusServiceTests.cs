using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FakeItEasy;
using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.Services;
using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using DataLayer;
using FamsAPI.IServices;

namespace FamsAPI.Test.Services.SyllabusServices
{
    public class SyllabusServicesTests
    {
        [Fact]
        public async Task UpdateSyllabus_WhenCalled_UpdatesSyllabus()
        {
            // Arrange
            var key = "TP001";
            var fakeSyllabusRepository = A.Fake<SyllabusRepository>();
            var fakeTrainingUnitRepository = A.Fake<TrainingUnitRepository>();
            var fakeAssessmentRepository = A.Fake<AssessmentRepository>();

            var expectedSyllabus = new Syllabus { TopicCode = key };
            A.CallTo(() => fakeSyllabusRepository.Get(A<Expression<Func<Syllabus, bool>>>._))
                .Returns(expectedSyllabus);

            var syllabusService = new FamsAPI.Services.SyllabusServices(
                fakeSyllabusRepository,
                fakeTrainingUnitRepository,
                A.Fake<LearningObjectiveRepository>(),
                A.Fake<TrainingContentRepository>(),
                A.Fake<ITrainingUnit>(),
                A.Fake<ISyllabusObjective>(),
                A.Fake<IAssessment>(),
                fakeAssessmentRepository,
                A.Fake<FAMSDBContext>(),
                A.Fake<SyllabusObjectiveRepository>(),
                A.Fake<IMapper>()
            );

            // Act
            var result = await syllabusService.UpdateSyllabus(expectedSyllabus, A.Fake<ClaimsPrincipal>());

            // Assert
            result.Should().BeEquivalentTo(expectedSyllabus);
            A.CallTo(() => fakeSyllabusRepository.Update(expectedSyllabus)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateSyllabus_WhenSyllabusDoesNotExist_ThrowsException()
        {
            // Arrange
            var key = "TP002";
            var fakeSyllabusRepository = A.Fake<SyllabusRepository>();
            var fakeTrainingUnitRepository = A.Fake<TrainingUnitRepository>();
            var fakeAssessmentRepository = A.Fake<AssessmentRepository>();

            var expectedSyllabus = new Syllabus { TopicCode = key };
            A.CallTo(() => fakeSyllabusRepository.Get(A<Expression<Func<Syllabus, bool>>>._))
                .Returns(null);

            var syllabusService = new FamsAPI.Services.SyllabusServices(
                fakeSyllabusRepository,
                fakeTrainingUnitRepository,
                A.Fake<LearningObjectiveRepository>(),
                A.Fake<TrainingContentRepository>(),
                A.Fake<ITrainingUnit>(),
                A.Fake<ISyllabusObjective>(),
                A.Fake<IAssessment>(),
                fakeAssessmentRepository,
                A.Fake<FAMSDBContext>(),
                A.Fake<SyllabusObjectiveRepository>(),
                A.Fake<IMapper>()
            );

            // Act
            var exception = await Record.ExceptionAsync(() => syllabusService.UpdateSyllabus(expectedSyllabus, A.Fake<ClaimsPrincipal>()));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal("Syllabus not found", exception.Message);
        }
    }
}