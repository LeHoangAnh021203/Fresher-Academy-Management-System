using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FamsAPI.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Controllers.ClassControllers
{
    public class EditClass
    {
      /*  [Fact]
        public void EditClass_WithValidClass_ShouldUpdateClassSuccessfully()
        {
            // Arrange
            var classService = A.Fake<IClass>();
            var controller = new ClassController(classService);
            var updatedClassViewModel = new InputClassViewModel
            {
                ClassID = "C00000001",
                ClassName = "Java Introduction Updated",
                ClassCode = "J001",
                Duration = 120,
                Status = 0,
                LocationId = "L001",
                FsuId = "F001",
                StartDate = DateTime.ParseExact("03-25-2024", "MM-dd-yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("04-25-2024", "MM-dd-yyyy", CultureInfo.InvariantCulture),
                CreatedBy = "Super Admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "Super Admin",
                ModifiedDate = DateTime.Now,
                TrainingProgramCode = "T001"
            };

            var requestMock = new Mock<HttpRequest>();
            var accessToken = "sampleaccesstoken";
            requestMock.SetupGet(r => r.Headers["Authorization"]).Returns($"Bearer {accessToken}");
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.Request).Returns(requestMock.Object);
            controller.ControllerContext = new ControllerContext { HttpContext = httpContextMock.Object };

           
            A.CallTo(() => classService.UpdateClass(updatedClassViewModel, accessToken)).Returns(updatedClassViewModel);

            // Act
            var result = controller.EditClass(updatedClassViewModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>(); 

            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<ClassViewModel>(); 

            var editedClass = (ClassViewModel)okResult.Value;
            editedClass.Should().NotBeNull();
            editedClass.ClassName.Should().Be(updatedClassViewModel.ClassName); 
        }

        [Fact]
        public void EditClass_WithValidClass_ShouldUpdateClassFailed()
        {
            // Arrange
            var classService = A.Fake<IClass>();
            var controller = new ClassController(classService);
            var updatedClassViewModel = new InputClassViewModel
            {
                ClassID = "C00000001",
                ClassName = "Java Introduction Updated",
                ClassCode = "J001",
                Duration = 120,
                Status = 0,
                LocationId = "L001",
                FsuId = "F001",
                StartDate = DateTime.ParseExact("03-25-2024", "MM-dd-yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("04-25-2024", "MM-dd-yyyy", CultureInfo.InvariantCulture),
                CreatedBy = "Super Admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "Super Admin",
                ModifiedDate = DateTime.Now,
                TrainingProgramCode = "T001"
            };

            // Mocking the HttpRequest and ControllerContext
            var requestMock = new Mock<HttpRequest>();
            var accessToken = "sampleaccesstoken";
            requestMock.SetupGet(r => r.Headers["Authorization"]).Returns($"Bearer {accessToken}");
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.Request).Returns(requestMock.Object);
            controller.ControllerContext = new ControllerContext { HttpContext = httpContextMock.Object };

            A.CallTo(() => classService.UpdateClass(updatedClassViewModel, accessToken)).Returns(null);

            // Act
            var result = controller.EditClass(updatedClassViewModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }*/
    }
}
