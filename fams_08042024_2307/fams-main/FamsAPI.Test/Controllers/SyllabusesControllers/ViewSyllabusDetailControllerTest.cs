using Xunit;
using FluentAssertions;
using FakeItEasy;
using FamsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using FamsAPI.IServices;

namespace FamsAPI.Test.ViewSyllabusDetail
{
    public class SyllabusControllerTests
    {
        [Fact]
        public void ViewSyllabusDetail_WhenKeyIsValid_ReturnsOk()
        {
            // Arrange
            var assessmentService = A.Fake<IAssessment>();
            var syllabusService = A.Fake<ISyllabus>();
            var syllabusRepository = A.Fake<ISyllabus>();
            var keyword = "1";

            var controller = new SyllabusController(assessmentService, syllabusService);

            // Act
            var result = controller.ViewSyllabusDetail(keyword);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ViewSyllabusDetail_WhenKeyIsValid_ReturnsBadRequest()
        {
            // Arrange
            var assessmentService = A.Fake<IAssessment>();
            var syllabusService = A.Fake<ISyllabus>();
            var syllabusRepository = A.Fake<ISyllabus>();

            var controller = new SyllabusController(assessmentService, syllabusService);
            string invalidKeyword = null;

            // Act
            var result = controller.ViewSyllabusDetail(invalidKeyword);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}