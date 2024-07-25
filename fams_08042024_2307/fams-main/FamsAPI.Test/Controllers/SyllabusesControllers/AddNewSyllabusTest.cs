using FakeItEasy;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Controllers.SyllabusesControllers
{
    public class AddNewSyllabusTest
    {
        [Fact]
        public async Task AddNewSyllabusWithAssessmentAndUnit_ShouldReturnSuccessMessage()
        {
            // Arrange
            var fakeSyllabusServices = A.Fake<SyllabusServices>();
            var fakeUser = A.Fake<ClaimsPrincipal>();
            var trainingUnits = new List<TrainingUnitViewModel>();

            A.CallTo(() => fakeUser.Identity.Name).Returns("Admin");
            A.CallTo(() => fakeSyllabusServices.AddNewSyllabusWithAssessmentAndUnit(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, fakeUser, A<int>.Ignored, A<double>.Ignored, A<int>.Ignored, A<double>.Ignored, A<double>.Ignored, A<double>.Ignored, trainingUnits)).Returns("Add successfully!");

            // Act
            var result = await fakeSyllabusServices.AddNewSyllabusWithAssessmentAndUnit("topicName", "technicalGroup", "trainingAudience", "topicOutline", "trainingMatirials", "trainingPrinciple", "courseObjective", "technicalRequirement", fakeUser, 1, 0.5, 1, 0.5, 0.5, 0.5, trainingUnits);

            // Assert
            result.Should().Be("Add successfully!");
        }
        [Fact]
        public async Task AddNewSyllabusWithAssessmentAndUnit_ShouldReturnErrorMessage()
        {
            // Arrange
            var fakeSyllabusServices = A.Fake<SyllabusServices>();
            var fakeUser = A.Fake<ClaimsPrincipal>();
            var trainingUnits = new List<TrainingUnitViewModel>();

            A.CallTo(() => fakeUser.Identity.Name).Returns("Admin");
            A.CallTo(() => fakeSyllabusServices.AddNewSyllabusWithAssessmentAndUnit(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored, fakeUser, A<int>.Ignored, A<double>.Ignored, A<int>.Ignored, A<double>.Ignored, A<double>.Ignored, A<double>.Ignored, trainingUnits)).Throws(new Exception("Database error"));

            // Act
            Func<Task> act = async () => await fakeSyllabusServices.AddNewSyllabusWithAssessmentAndUnit("topicName", "technicalGroup", "trainingAudience", "topicOutline", "trainingMatirials", "trainingPrinciple", "courseObjective", "technicalRequirement", fakeUser, 1, 0.5, 1, 0.5, 0.5, 0.5, trainingUnits);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
        }

    }
}
