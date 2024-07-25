using DataLayer.Entities;
using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FamsAPI.Services;
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
    public class AddNewClass
    {/*
        [Fact]
        public void AddNewClass_WithValidClass_ShouldReturnOkRequest()
        {
            // Arrange
            var classService = A.Fake<IClass>();
            var controller = new ClassController(classService);
            var newClassViewModel = new InputClassViewModel
            {
                ClassID = "C00000001",
                ClassName = "Java Introduction",
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
            var classUsers = new List<ClassUserViewModel>
            {
                new ClassUserViewModel
                {
                    UserId= new Guid(),
                    ClassId= "C00000001",
                    UserType = "Trainer"
                },
                 new ClassUserViewModel
                {
                    UserId= new Guid(),
                    ClassId= "C00000001",
                    UserType = "Admin"
                },
            };

            var combineVariables = new NewClassViewModel
            {
                InputClass = newClassViewModel,
                ClassUser = classUsers
            };

            // Mocking the HttpRequest and ControllerContext
            var requestMock = new Mock<HttpRequest>();
            var accessToken = "sampleaccesstoken";
            requestMock.SetupGet(r => r.Headers["Authorization"]).Returns($"Bearer {accessToken}");
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.Request).Returns(requestMock.Object);
            controller.ControllerContext = new ControllerContext { HttpContext = httpContextMock.Object };

            A.CallTo(() => classService.AddNewClass(newClassViewModel,classUsers, accessToken)).Returns(newClassViewModel); 

            // Act
            var result = controller.AddNewClass(combineVariables);

            // Assert
            result.Should().BeOfType<OkObjectResult>(); 

            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<InputClassViewModel>(); 

            var addedClass = (InputClassViewModel)okResult.Value;
            addedClass.Should().NotBeNull();
            addedClass.ClassName.Should().Be(newClassViewModel.ClassName);

        }

        [Fact]
        public void AddNewClass_WithInvalidClass_ShouldReturnBadRequest()
        {
            // Arrange
            var classService = A.Fake<IClass>();
            var controller = new ClassController(classService);
            var newClassViewModel = new InputClassViewModel
            {
                ClassID = "C00000001",
                ClassName = "Java Introduction",
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

            var classUsers = new List<ClassUserViewModel>
            {
                new ClassUserViewModel
                {
                    UserId= new Guid(),
                    ClassId= "C00000001",
                    UserType = "Trainer"
                },
                 new ClassUserViewModel
                {
                    UserId= new Guid(),
                    ClassId= "C00000001",
                    UserType = "Admin"
                },
            };

            var requestMock = new Mock<HttpRequest>();
            var accessToken = "sampleaccesstoken";
            requestMock.SetupGet(r => r.Headers["Authorization"]).Returns($"Bearer {accessToken}");
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.Request).Returns(requestMock.Object);
            controller.ControllerContext = new ControllerContext { HttpContext = httpContextMock.Object };

            var combineVariables = new NewClassViewModel
            {
                InputClass = newClassViewModel,
                ClassUser = classUsers
            };

            A.CallTo(() => classService.AddNewClass(newClassViewModel, classUsers, accessToken)).Returns(null);

            // Act
            var result = controller.AddNewClass(combineVariables);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = (BadRequestObjectResult)result;
            badRequestResult.Value.Should().Be("Failed to add a new class. Please try again later."); // Verify error message
        }
*/

    }
}
