using Xunit;
using FluentAssertions;
using FakeItEasy;
using FamsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;

namespace FamsAPI.Tests.DuplicatSyllabusControllers
{
    public class SyllabusControllerTests
    {
        [Fact]
        public void DuplicateSyllabus_WithValidKeyword_ReturnsOk()
        {
            // Arrange
            var assessmentService = A.Fake<IAssessment>();
            var syllabusService = A.Fake<ISyllabus>();
            var syllabusRepository = A.Fake<ISyllabus>();

            var controller = new SyllabusController(assessmentService, syllabusService);
            var keyword = "exampleKeyword";

            // Act
            var result = controller.DuplicateSyllabus(keyword);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void DuplicateSyllabus_WithInvalidKeyword_ReturnsBadRequest()
        {
            // Arrange
            var assessmentService = A.Fake<IAssessment>();
            var syllabusService = A.Fake<ISyllabus>();
            var syllabusRepository = A.Fake<ISyllabus>();

            var controller = new SyllabusController(assessmentService, syllabusService);
            string invalidKeyword = null;

            // Act
            var result = controller.DuplicateSyllabus(invalidKeyword);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void DuplicateSyllabus_WithExistTopicCode_ReturnOKAndData()
        {
            // Arrange
            var assessmentService = A.Fake<IAssessment>();
            var syllabusService = A.Fake<ISyllabus>();
            var syllabusRepository = A.Fake<ISyllabus>();
            var controller = new SyllabusController(assessmentService, syllabusService);
            string invalidKeyword = "1";

            var oldSyllabus = new SyllabusViewModel
            {
                TopicCode = "1",
                TopicName = "C#",
                CreatedBy = "NamPT",
                CreatedDate = DateTime.Now
            };
            A.CallTo(() => syllabusService.DuplicateSyllabus(invalidKeyword)).Returns(new SyllabusViewModel 
            {
                TopicCode = "2",
                TopicName = "C#",
                CreatedBy = "NamPT",
                CreatedDate = DateTime.Now
            });
            // Act
            var result = controller.DuplicateSyllabus(invalidKeyword);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<SyllabusViewModel>();

            var newSyllabus = (SyllabusViewModel)okResult.Value;
            newSyllabus.Should().NotBeNull();
            newSyllabus.TopicName.Should().BeEquivalentTo(oldSyllabus.TopicName);      
        }

        [Fact]
        public void DuplicateSyllabus_WithNotExistTopicCode_ReturnNotFound()
        {
            // Arrange
            var assessmentService = A.Fake<IAssessment>();
            var syllabusService = A.Fake<ISyllabus>();
            var syllabusRepository = A.Fake<ISyllabus>();
            var controller = new SyllabusController(assessmentService, syllabusService);
            string invalidKeyword = "abcxyz";
            A.CallTo(() => syllabusService.DuplicateSyllabus(invalidKeyword)).Returns(null);

            // Act
            var result = controller.DuplicateSyllabus(invalidKeyword);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}