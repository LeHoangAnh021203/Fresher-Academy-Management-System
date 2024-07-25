using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.IServices;
using FamsAPI.Services;
using System;
using System.Linq.Expressions;
using Xunit;

namespace FamsAPI.Test.Services.TrainingUnitService
{
    public class TrainingUnitServiceTest
    {
        private readonly LearningObjectiveRepository _fakeLearningObjectiveRepository;
        private readonly ITrainingContent _fakeTrainingContentService;
        private readonly SyllabusRepository _fakeSyllabusRepository;
        private readonly TrainingUnitServices _trainingUnitServices;
        private readonly TrainingUnitRepository _fakeTrainingUnitRepository;
        private readonly TrainingContentRepository _fakeTrainingContentRepository;

        public TrainingUnitServiceTest()
        {
            _fakeTrainingUnitRepository = A.Fake<TrainingUnitRepository>();
            _fakeTrainingContentRepository = A.Fake<TrainingContentRepository>();
            _fakeLearningObjectiveRepository = A.Fake<LearningObjectiveRepository>();
            _fakeTrainingContentService = A.Fake<ITrainingContent>();
            _fakeSyllabusRepository = A.Fake<SyllabusRepository>();

            _trainingUnitServices = new TrainingUnitServices(
                _fakeTrainingUnitRepository,
                _fakeTrainingContentRepository,
                _fakeLearningObjectiveRepository,
                _fakeTrainingContentService,
                _fakeSyllabusRepository
            );
        }


        [Fact]
        public async Task EditUnit_WhenCalled_UpdatesUnit()
        {
            // Arrange
            var unitCode = "U00000001";
            var newUnitName = "Test1";
            var unit = new TrainingUnit { UnitCode = unitCode };

            A.CallTo(() => _fakeTrainingUnitRepository.Get(A<Expression<Func<TrainingUnit, bool>>>._))
                .Returns(unit);

            // Act
            await _trainingUnitServices.EditUnit(unitCode, newUnitName);

            // Assert
            Assert.Equal(newUnitName, unit.UnitName);
            A.CallTo(() => _fakeTrainingUnitRepository.Update(unit)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditUnit_WhenUnitDoesNotExist_ThrowsException()
        {
            // Arrange
            var unitCode = "nonexistent code";
            var newUnitName = "Test2";

            A.CallTo(() => _fakeTrainingUnitRepository.Get(A<Expression<Func<TrainingUnit, bool>>>._))
                .Returns(null);

            // Act
            var exception = await Record.ExceptionAsync(() => _trainingUnitServices.EditUnit(unitCode, newUnitName));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal("Unit not found.", exception.InnerException.Message);
        }
    }
}
