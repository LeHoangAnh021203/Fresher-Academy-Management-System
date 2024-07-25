using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using FluentAssertions;
using System;
using System.Linq.Expressions;
using Xunit;

namespace FamsAPI.Test.Services.AssessmentServices
{
    public class AssessmentServiceTest
    {
        private readonly AssessmentService _assessmentService;
        private readonly AssessmentRepository _fakeAssessmentRepository;
        private readonly SyllabusRepository _fakeSyllabusRepository;

        public AssessmentServiceTest()
        {
            _fakeAssessmentRepository = A.Fake<AssessmentRepository>();

            _assessmentService = new AssessmentService(
                _fakeAssessmentRepository,
                _fakeSyllabusRepository
            );
        }

        [Fact]
        public async Task EditAssessment_WhenCalled_UpdatesAssessment()
        {
            // Arrange
            var assessmentId = "A00000001";
            var assessment = new AssessmentViewModel { AssessmentID = assessmentId };

            A.CallTo(() => _fakeAssessmentRepository.Get(A<Expression<Func<Assessment, bool>>>._))
                .Returns(new Assessment());

            // Act
            var result = await _assessmentService.EditAssessment(assessment);

            // Assert
            result.Should().BeEquivalentTo(assessment);
        }

        [Fact]
        public async Task EditAssessment_WhenAssessmentDoesNotExist_ThrowsException()
        {
            // Arrange
            var assessmentId = "nonexistent id";
            var assessment = new AssessmentViewModel { AssessmentID = assessmentId };

            A.CallTo(() => _fakeAssessmentRepository.Get(A<Expression<Func<Assessment, bool>>>._))
                .Returns(null);

            // Act
            var exception = await Record.ExceptionAsync(() => _assessmentService.EditAssessment(assessment));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal("Assessment not found", exception.InnerException.Message);
        }
    }
}
