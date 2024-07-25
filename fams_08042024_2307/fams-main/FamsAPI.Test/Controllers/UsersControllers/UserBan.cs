using Azure.Core;
using DataLayer.Entities;
using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable
namespace FamsAPI.Test.Controllers.UsersControllers
{
    public class UserBan
    {
        #region Fake data
        [Fact]

        public void UpdateUserStatus_ReturnsOK_WithDeactive()
        {
            var userService = A.Fake<IUser>();
            var user = Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72");
            A.CallTo(() => userService.GetAllUsers()).Returns(new List<User>
                {
                    new User
                    {
                        UserId = Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"),
                        Name = "Super Admin",
                        Status = User.UserStatus.Deactive
                    }
                }); ;

            var controller = new UserController(userService);

            // Act
            var result = controller.UpdateUserStatus(user);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<User>();
            
            var updatedUser = (User)okResult.Value;
            updatedUser.Status.Should().Be(User.UserStatus.Active);

        }

        public void UpdateUserStatus_ReturnsOK_WithActive()
        {
            var userService = A.Fake<IUser>();
            var user = Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72");
            A.CallTo(() => userService.GetAllUsers()).Returns(new List<User>
                {
                    new User
                    {
                        UserId = Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"),
                        Name = "Super Admin",
                        Status = User.UserStatus.Active
                    }
                }); ;

            var controller = new UserController(userService);

            // Act
            var result = controller.UpdateUserStatus(user);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<User>();

            var updatedUser = (User)okResult.Value;
            updatedUser.Status.Should().Be(User.UserStatus.Deactive);

        }
        #endregion
    }
}

