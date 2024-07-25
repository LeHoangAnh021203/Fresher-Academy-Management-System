using FamsAPI.Controllers;
using FamsAPI.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;
using FakeItEasy;
using DataLayer.Entities;

namespace FamsAPI.Test.Controllers.TrainingControllers
{
    public class TrainingContentControllerTest
    {
        [Fact]
        public async Task EditContentByUnitCode_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var _fakeUnitService = A.Fake<ITrainingUnit>();
            var _fakeContentService = A.Fake<ITrainingContent>();
            var _controller = new TrainingController(_fakeUnitService, _fakeContentService);
            var unitCode = "U00000001";
            var trainingContent = new TrainingContent { ContentId = "C00000001", UnitCode = unitCode };

            A.CallTo(() => _fakeUnitService.GetTrainingUnitByUnitCode(unitCode))
                .Returns(new TrainingUnit());
            A.CallTo(() => _fakeContentService.GetTrainingContentByContentId(trainingContent.ContentId))
                .Returns(trainingContent);
            A.CallTo(() => _fakeContentService.EditContent(unitCode, trainingContent.ContentId, trainingContent))
                .Returns("Update success");

            // Act
            var result = await _controller.EditContentByUnitCode(unitCode, trainingContent);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Content updated successfully.", okResult.Value);
        }

        [Fact]
        public async Task EditContentByUnitCode_WhenUnitCodeDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            var _fakeUnitService = A.Fake<ITrainingUnit>();
            var _fakeContentService = A.Fake<ITrainingContent>();
            var _controller = new TrainingController(_fakeUnitService, _fakeContentService);
            var unitCode = "U00000002";
            var trainingContent = new TrainingContent { ContentId = "C00000002", UnitCode = unitCode };

            A.CallTo(() => _fakeUnitService.GetTrainingUnitByUnitCode(unitCode))
                .Returns(null);

            // Act
            var result = await _controller.EditContentByUnitCode(unitCode, trainingContent);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("UnitCode not found.", badRequestResult.Value);
        }

        [Fact]
        public async Task EditContentByUnitCode_WhenContentIdDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            var _fakeUnitService = A.Fake<ITrainingUnit>();
            var _fakeContentService = A.Fake<ITrainingContent>();
            var _controller = new TrainingController(_fakeUnitService, _fakeContentService);
            var unitCode = "U00000003";
            var trainingContent = new TrainingContent { ContentId = "C00000003", UnitCode = unitCode };

            A.CallTo(() => _fakeUnitService.GetTrainingUnitByUnitCode(unitCode))
                .Returns(new TrainingUnit());
            A.CallTo(() => _fakeContentService.GetTrainingContentByContentId(trainingContent.ContentId))
                .Returns(null);

            // Act
            var result = await _controller.EditContentByUnitCode(unitCode, trainingContent);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ContentId not found.", badRequestResult.Value);
        }
    }
}
