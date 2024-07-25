using DataLayer.Entities;
using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Controllers.UsersControllers
{
    public class SortUser
    {
        [Fact]
        public void SortUsers_WhenSortByNameAsc_ShouldReturnOkObjectResult()
        {
            var fakeService = A.Fake<IUser>();
            var users = new List<User>
        {
            new User { Name = "TestName1", Status = (User.UserStatus) 1 },
            new User { Name = "TestName2", Status = 0 },
            new User { Name = "TestName3", Status = (User.UserStatus) 1 },
        };
            A.CallTo(() => fakeService.SortUsers("name", "asc")).Returns(users.OrderBy(u => u.Name).ToList());
            var controller = new UserController(fakeService);

            // Act
            var result = controller.GetUsers("name", "asc");

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            var resultUsers = okResult.Value as List<User>;
            resultUsers.Should().BeInAscendingOrder(u => u.Name);
        }

        [Fact]
        public void SortUsers_WhenSortByStatusDesc_ShouldReturnOkObjectResult()
        {
            var fakeService = A.Fake<IUser>();
            var users = new List<User>
        {
            new User { Name = "TestName1", Status = (User.UserStatus) 1 },
            new User { Name = "TestName2", Status = 0 },
            new User { Name = "TestName3", Status = (User.UserStatus) 1 },
        };
            A.CallTo(() => fakeService.SortUsers("status", "desc")).Returns(users.OrderBy(u => u.Name).ToList());
            var controller = new UserController(fakeService);

            // Act
            var result = controller.GetUsers("status", "desc");

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            var resultUsers = okResult.Value as List<User>;
            resultUsers.Should().BeInAscendingOrder(u => u.Name);
        }

        [Fact]
        public void SortUsers_WhenSortInvalid_ShouldReturnBadRequestObjectResult()
        {
            var fakeService = A.Fake<IUser>();
            A.CallTo(() => fakeService.SortUsers("invalid", "asc")).Throws(new ArgumentException("Invalid sort parameter"));
            var controller = new UserController(fakeService);

            // Act
            var result = controller.GetUsers("invalid", "asc");

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().Be("Invalid sort parameter");
        }
    }
}
