using Xunit;
using FakeItEasy;
using FluentAssertions;
using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.Services;

namespace FamsAPI.Test.Services.UserServices
{
    public class SortUser
    {
        [Fact]
        public void SortUsers_WhenSortByNameAsc_ShouldReturnSortedUsers()
        {
            // Arrange
            var fakeRepo = A.Fake<UserRepository>();
            var service = new FamsAPI.Services.UserServices(fakeRepo);
            var users = new List<User>
        {
            new User { Name = "TestName1", Status = (User.UserStatus) 1 },
            new User { Name = "TestName2", Status = 0 },
            new User { Name = "TestName3", Status = (User.UserStatus) 1 },
        };
            A.CallTo(() => fakeRepo.GetAll()).Returns(users);

            // Act
            var result = service.SortUsers("name", "asc");

            // Assert
            result.Should().BeInAscendingOrder(u => u.Name);
        }

        [Fact]
        public void SortUsers_WhenSortByStatusDesc_ShouldReturnSortedUsers()
        {
            // Arrange
            var fakeRepo = A.Fake<UserRepository>();
            var service = new FamsAPI.Services.UserServices(fakeRepo);
            var users = new List<User>
        {
            new User { Name = "TestName1", Status = (User.UserStatus) 1 },
            new User { Name = "TestName2", Status = 0 },
            new User { Name = "TestName3", Status = (User.UserStatus) 1 },
        };
            A.CallTo(() => fakeRepo.GetAll()).Returns(users);

            // Act
            var result = service.SortUsers("status", "desc");

            // Assert
            result.Should().BeInDescendingOrder(u => u.Status);
        }

        [Fact]
        public void SortUsers_WhenInvalidSortBy_ShouldThrowArgumentException()
        {
            // Arrange
            var fakeRepo = A.Fake<UserRepository>();
            var service = new FamsAPI.Services.UserServices(fakeRepo);

            // Act
            Action act = () => service.SortUsers("invalid", "asc");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Invalid sort parameter");
        }
    }
}