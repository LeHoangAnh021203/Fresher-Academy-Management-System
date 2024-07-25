using DataLayer.Entities;
using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Controllers.TrainingControllers
{
    public class TrainingUnitControllerTest
    {
        [Fact]
        public void EditUnit_UnitCodeFound_ReturnsSuccess()
        {
            // Arrange
            var fakeUnitService = A.Fake<ITrainingUnit>();
            var fakeContentService = A.Fake<ITrainingContent>();
            var controller = new TrainingController(fakeUnitService, fakeContentService);
            string unitCode = "U00000001";
            string unitName = "Test1";

            A.CallTo(() => fakeUnitService.GetTrainingUnitByUnitCode(unitCode)).Returns(new TrainingUnit());

            // Act
            var result = controller.EditUnit(unitCode, unitName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Unit updated successfully.", okResult.Value);
        }

        [Fact]
        public void EditUnit_UnitCodeNotFound_ReturnsBadRequest()
        {
            // Arrange
            var fakeUnitService = A.Fake<ITrainingUnit>();
            var fakeContentService = A.Fake<ITrainingContent>();
            var controller = new TrainingController(fakeUnitService, fakeContentService);
            string unitCode = "U00000002";
            string unitName = "Test2";

            A.CallTo(() => fakeUnitService.GetTrainingUnitByUnitCode(unitCode)).Returns(null);

            // Act
            var result = controller.EditUnit(unitCode, unitName);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("UnitCode not found!", badRequestResult.Value);
        }
    }
}
