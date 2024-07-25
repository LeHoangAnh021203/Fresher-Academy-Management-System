using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.IServices;
using FamsAPI.Services;
using FluentAssertions;
using System;
using System.Linq.Expressions;
using Xunit;

namespace FamsAPI.Test.Services.TrainingContentServicesTest
{
    public class TrainingContentServiceTest
    {
        private readonly TrainingContentServices _trainingContentServices;
        private readonly TrainingContentRepository _fakeTrainingContentRepository;

        public TrainingContentServiceTest()
        {
            _fakeTrainingContentRepository = A.Fake<TrainingContentRepository>();

            _trainingContentServices = new TrainingContentServices(
                _fakeTrainingContentRepository
            );
        }

        [Fact]
        public async Task EditContent_WhenCalled_UpdatesContent()
        {
            // Arrange
            var unitCode = "U00000001";
            var contentId = "C00000001";
            var trainingContent = new TrainingContent { ContentId = contentId, UnitCode = unitCode };

            A.CallTo(() => _fakeTrainingContentRepository.Get(A<Expression<Func<TrainingContent, bool>>>._))
                .Returns(trainingContent);

            // Act
            var result = await _trainingContentServices.EditContent(unitCode, contentId, trainingContent);

            // Assert
            result.Should().BeEquivalentTo("Update success");
            A.CallTo(() => _fakeTrainingContentRepository.Update(trainingContent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditContent_WhenContentDoesNotExist_ThrowsException()
        {
            // Arrange
            var unitCode = "U00000002";
            var contentId = "C00000002";
            var trainingContent = new TrainingContent { ContentId = contentId, UnitCode = unitCode };

            A.CallTo(() => _fakeTrainingContentRepository.Get(A<Expression<Func<TrainingContent, bool>>>._))
                .Returns(null);

            // Act
            var exception = await Record.ExceptionAsync(() => _trainingContentServices.EditContent(unitCode, contentId, trainingContent));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal("Content not found.", exception.InnerException.Message);
        }
    }
}
