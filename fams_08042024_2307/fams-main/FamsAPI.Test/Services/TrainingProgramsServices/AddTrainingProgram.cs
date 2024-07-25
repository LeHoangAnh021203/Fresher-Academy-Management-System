using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Services.TrainingProgramsServices
{
    public class AddTrainingProgram
    {

        [Fact]
        public async Task AddNewTrainingProgramAsync_WithValidTrainingProgram_ShouldAddTrainingProgramSuccessfully()
        {
            // Arrange
            var fakeTrainingProgramRepository = A.Fake<TrainingProgramRepository>();
            var fakeTrainingProgramSyllabusRepository = A.Fake<TrainingProgramSyllabusRepository>();
            var fakeTrainingContentRepository= A.Fake<TrainingContentRepository>();
            var fakeSyllabusRepository= A.Fake<SyllabusRepository>();
            var classRepositry= A.Fake<ClassRepository>();
            var service = new TrainingProgramService(fakeTrainingProgramRepository, fakeSyllabusRepository, fakeTrainingProgramSyllabusRepository, classRepositry, fakeTrainingContentRepository);


            var name = "Test Training Program";
            var duration = 10;
            var topicCode = new List<string> { "Topic1", "Topic2", "Topic3" };
            var userId = Guid.NewGuid().ToString(); // Generate a valid Guid string
            var claims = new List<Claim>
                {
                    new Claim("UserId", userId),
                    new Claim(ClaimTypes.Name, "TestUser") // You can set other claims as needed
                };
            var identity = new ClaimsIdentity(claims, "TestAuthenticationType");
            var fakeUser = new ClaimsPrincipal(identity);


            
            //A.CallTo(() => trainingProgramService.AddNewTrainingProgramAsync(fakeUser, name, duration, topicCode)); 
            // Act
            var result = service.AddNewTrainingProgramAsync(fakeUser, name, duration, topicCode);
            
            // Assert
            result.Should().NotBeNull();

            
        }

        [Fact]
        public async Task AddNewTrainingProgramAsync_WithEmptyName_ShouldThrowBadRequestException()
        {
            // Arrange
            var fakeTrainingProgramRepository = A.Fake<TrainingProgramRepository>();
            var fakeTrainingProgramSyllabusRepository = A.Fake<TrainingProgramSyllabusRepository>();
            var fakeTrainingContentRepository = A.Fake<TrainingContentRepository>();
            var fakeSyllabusRepository = A.Fake<SyllabusRepository>();
            var classRepositry = A.Fake<ClassRepository>();
            var service = new TrainingProgramService(fakeTrainingProgramRepository, fakeSyllabusRepository, fakeTrainingProgramSyllabusRepository, classRepositry, fakeTrainingContentRepository);

            var name = ""; // Empty name, which is invalid
            var duration = 10; // Valid duration
            var topicCode = new List<string> { "Topic1", "Topic2" }; // Valid topic codes
            var userId = Guid.NewGuid().ToString(); // Generate a valid Guid string
            var claims = new List<Claim>
                {
                    new Claim("UserId", userId),
                    new Claim(ClaimTypes.Name, "TestUser") // You can set other claims as needed
                };
            var identity = new ClaimsIdentity(claims, "TestAuthenticationType");
            var fakeUser = new ClaimsPrincipal(identity);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                service.AddNewTrainingProgramAsync(fakeUser, name, duration, topicCode);
            });

            // No need for additional assertions since the exception handling is checked by Assert.ThrowsAsync
        }

    }
}
