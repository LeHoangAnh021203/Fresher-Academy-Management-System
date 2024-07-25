using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Services.ClassServices
{
    public class AddClassService
    {
        [Fact]
        public void AddNewClass_WithValidClass_ReturnOkObjectResult()
        {
            // Arrange
            var classRepository = A.Fake<ClassRepository>();
            var mapper = A.Fake<IMapper>();
            var trainingProgramRepository = A.Fake<TrainingProgramRepository>();
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "Super Admin"),
                new Claim("AccessToken", "valid_access_token"),
                // Add any other claims you may need for the test
            });
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var newClass = new InputClassViewModel
            {
                ClassID = "C000001",
                ClassName = "Java Introduction",
                ClassCode = "J001",
                Duration = 120,
                Status = 0,
                LocationId = "L001",
                FsuId = "F001",
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

            var classService = A.Fake<FamsAPI.Services.ClassServices>();

            A.CallTo(() => classService.AddNewClass(A<InputClassViewModel>.Ignored, A<List<ClassUserViewModel>>.Ignored, claimsPrincipal));
            var result = classService.AddNewClass(newClass, classUsers, claimsPrincipal);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void AddNewClass_WithInvalidClass_ReturnBadRequestResult()
        {
            // Arrange
            var classRepository = A.Fake<ClassRepository>();
            var mapper = A.Fake<IMapper>();
            var trainingProgramRepository = A.Fake<TrainingProgramRepository>();
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "Super Admin"),
                new Claim("AccessToken", "valid_access_token"),
                // Add any other claims you may need for the test
            });
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var newClass = new InputClassViewModel
            {
                ClassID = "C000001",
                ClassName = "Java Introduction",
                ClassCode = "J001",
                Duration = 120,
                Status = 0,
                LocationId = "L001",
                FsuId = "F001",
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

            var classService = A.Fake<FamsAPI.Services.ClassServices>();
            A.CallTo(() => classService.AddNewClass(A<InputClassViewModel>.Ignored, A<List<ClassUserViewModel>>.Ignored, claimsPrincipal))
                .Throws(new Exception("An error occurred while creating new class."));

            // Act and Assert
            var exception = Assert.Throws<Exception>(() => classService.AddNewClass(newClass, classUsers, claimsPrincipal));
            exception.Message.Should().Be("An error occurred while creating new class.");
        }
    }
}
