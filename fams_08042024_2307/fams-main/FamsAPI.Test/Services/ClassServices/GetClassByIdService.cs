using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Services.ClassServices
{
    public class GetClassByIdService
    {
        [Fact]
        public void GetClassById_WithValidId_ReturnsOkObjectClassViewModel()
        {
            // Arrange
            var classId = "C00000001";
            var classEntity = new Class
            {
                ClassID = classId,
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
                TrainingCalendars = new List<TrainingCalendar>(),
                TrainingProgramCode = "T001"
            };

            var classRepository = A.Fake<ClassRepository>();
            A.CallTo(() => classRepository.Get(A<Expression<Func<Class, bool>>>._))
                .WithAnyArguments()
                .Returns(classEntity);

            var mapper = A.Fake<IMapper>();
            A.CallTo(() => mapper.Map<ClassViewModel>(classEntity)).Returns(new ClassViewModel
            {
                ClassID = classEntity.ClassID,
                ClassName = classEntity.ClassName
            });

            var trainingProgramRepository = A.Fake<TrainingProgramRepository>();

            var classUserRepository= A.Fake<ClassUserRepository>();
            var userRepository= A.Fake<UserRepository>();
            var trainingCalendarRepository= A.Fake<TrainingCalendarRepository>();

            var classService = new FamsAPI.Services.ClassServices(classRepository, classUserRepository, trainingProgramRepository, mapper, userRepository, trainingCalendarRepository);

            // Act
            var result = classService.GetClassById(classId);

            // Assert
            result.Should().NotBeNull();
            result.ClassID.Should().Be(classId);
        }

        [Fact]
        public void GetClassById_WithInvalidClassId_ReturnBadObjectResult()
        {
            // Arrange
            var classId = "NonExistingClassId";

            // Mock the dependencies
            var classRepository = A.Fake<ClassRepository>();
            A.CallTo(() => classRepository.Get(A<Expression<Func<Class, bool>>>._))
                .WithAnyArguments()
                .Returns(null); 

            var mapper = A.Fake<IMapper>();
            var trainingProgramRepository = A.Fake<TrainingProgramRepository>();
            var classUserRepository = A.Fake<ClassUserRepository>();
            var userRepository= A.Fake<UserRepository>();
            var trainingCalendarRepository = A.Fake<TrainingCalendarRepository>();
            var classService = new FamsAPI.Services.ClassServices(classRepository, classUserRepository, trainingProgramRepository, mapper, userRepository, trainingCalendarRepository);

            // Act and Assert
            Action act = () => classService.GetClassById(classId);
            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("An error occurred while getting the class by ID.", exception.Message);
        }
    }
}
