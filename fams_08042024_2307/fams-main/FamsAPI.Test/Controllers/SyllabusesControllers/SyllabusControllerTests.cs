using System;
using System.Collections.Generic;
using Xunit;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using FakeItEasy;
using FluentAssertions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FamsAPI.Test.Controllers.SyllabusesControllersTests
{
    public class SyllabusControllerTests
    {
        [Fact]
        public async Task UpdateSyllabus_With_Valid_Syllabus_Returns_OkResult()
        {
            // Arrange
            var fakeAssessmentService = A.Fake<IAssessment>();
            var fakeSyllabusService = A.Fake<ISyllabus>();
            var fakeSyllabusRepository = A.Fake<ISyllabus>();
            var fakeContext = A.Fake<HttpContext>();

            var syllabus = new Syllabus { TopicCode = "TP001", TopicName = "Test1" };
            var controller = new SyllabusController(fakeAssessmentService, fakeSyllabusService);

            fakeContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.Name, "username")
            }));
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = fakeContext
            };

            A.CallTo(() => fakeSyllabusService.UpdateSyllabus(syllabus, A<ClaimsPrincipal>.Ignored))
                .Returns(syllabus);

            // Act
            var result = await controller.UpdateSyllabus(syllabus);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Syllabus updated successfully", okResult.Value);
        }

        [Fact]
        public async Task UpdateSyllabus_With_Invalid_Syllabus_Returns_BadRequestResult()
        {
            // Arrange
            var fakeAssessmentService = A.Fake<IAssessment>();
            var fakeSyllabusService = A.Fake<ISyllabus>();
            var fakeSyllabusRepository = A.Fake<ISyllabus>();
            var fakeContext = A.Fake<HttpContext>();

            Syllabus syllabus = null; // invalid syllabus
            var controller = new SyllabusController(fakeAssessmentService, fakeSyllabusService);

            fakeContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.Name, "username")
            }));
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = fakeContext
            };

            A.CallTo(() => fakeSyllabusService.UpdateSyllabus(syllabus, A<ClaimsPrincipal>.Ignored))
                .Returns((Syllabus)null);
            // Act
            var result = await controller.UpdateSyllabus(syllabus);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Syllabus updated failed", badRequestResult.Value);
        }
    }
}
