using Xunit;
using FluentAssertions;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using static DataLayer.Entities.User;
using static FamsAPI.IServices.IUser;
using FamsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FamsAPI.Test.Controllers.UsersControllers
{
    public class CreateAndDeleteUserServices
    {
        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var fakeUserRepository = A.Fake<UserRepository>();
            var expectedUsers = new List<User>
            {
                new User { UserId = Guid.NewGuid(), Name = "John Doe" },
                new User { UserId = Guid.NewGuid(), Name = "Jane Doe" }
            };
            A.CallTo(() => fakeUserRepository.GetAll()).Returns(expectedUsers);

            var userService = new UserServices(fakeUserRepository);

            // Act
            var result = userService.GetAllUsers();

            // Assert
            result.Should().BeEquivalentTo(expectedUsers);
        }

        [Fact]
        public void GetUserById_WithValidId_ShouldReturnUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = new User { UserId = userId, Name = "John Doe" };

            var fakeUserRepository = A.Fake<UserRepository>();
            A.CallTo(() => fakeUserRepository.Get(A<Expression<Func<User, bool>>>._)).Returns(expectedUser);

            var userService = new UserServices(fakeUserRepository);

            // Act
            var result = userService.GetUserById(userId);

            // Assert
            result.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public void UpdateUserStatus_WithValidRequest_ShouldUpdateUserStatus()
        {
            // Arrange
            var request = new User
            {
                UserId = Guid.NewGuid(),
                Status = UserStatus.Active
            };

            var fakeUserRepository = A.Fake<UserRepository>();
            var userToUpdate = new User { UserId = request.UserId, Status = UserStatus.Deactive };
            A.CallTo(() => fakeUserRepository.Get(A<Expression<Func<User, bool>>>._)).Returns(userToUpdate);

            var userService = new UserServices(fakeUserRepository);

            // Act
            var result = userService.UpdateUserStatus(request.UserId);

            // Assert
            result.Status.Should().Be(request.Status);
        }

        [Fact]
        public void GrantPermission_WithExistingUser_ShouldUpdatePermission()
        {
            // Arrange
            var user = new User { UserId = Guid.NewGuid(), PermissionId = 1 };
            var fakeUserRepository = A.Fake<UserRepository>();
            A.CallTo(() => fakeUserRepository.Get(A<Expression<Func<User, bool>>>._)).Returns(user);

            var userService = new UserServices(fakeUserRepository);

            // Act
            var result = userService.GrantPermission(user);

            // Assert
            result.PermissionId.Should().Be(user.PermissionId);
        }

        [Fact]
        public void GetUsersByKeyword_WithValidKeyword_ShouldReturnMatchingUsers()
        {
            // Arrange
            var keyword = "Doe";
            var filter = "NAME";
            var expectedUsers = new List<User>
            {
                new User { UserId = Guid.NewGuid(), Name = "John Doe" },
                new User { UserId = Guid.NewGuid(), Name = "Jane Doe" }
            };

            var fakeUserRepository = A.Fake<UserRepository>();
            A.CallTo(() => fakeUserRepository.GetAll()).Returns(expectedUsers);

            var userService = new UserServices(fakeUserRepository);

            // Act
            var result = userService.GetUsersByKeyword(keyword, filter);

            // Assert
            result.Should().BeEquivalentTo(expectedUsers);
        }

        //[Fact]
        //public void SortUsers_WithValidSortBy_ShouldReturnSortedUsers()
        //{
        //    // Arrange
        //    var sortBy = "name";
        //    var sortDir = "asc";
        //    var expectedUsers = new List<User>
        //    {
        //        new User { UserId = Guid.NewGuid(), Name = "Jane" },
        //        new User { UserId = Guid.NewGuid(), Name = "John" }
        //    };

        //    var fakeUserRepository = A.Fake<UserRepository>();
        //    A.CallTo(() => fakeUserRepository.GetAll()).Returns(expectedUsers);

        //    var userService = new UserServices(fakeUserRepository);

        //    // Act
        //    var result = userService.SortUsers(sortBy, sortDir);

        //    // Assert
        //    result.Should().BeInAscendingOrder(u => u.Name);
        //}

        [Fact]
        public void UserLogin_WithValidCredentials_ShouldReturnUser()
        {
            // Arrange
            var email = "john@example.com";
            var password = "password";
            var expectedUser = new User { UserId = Guid.NewGuid(), Email = email, Password = password };

            var fakeUserRepository = A.Fake<UserRepository>();
            A.CallTo(() => fakeUserRepository.Get(A<Expression<Func<User, bool>>>._)).Returns(expectedUser);

            var userService = new UserServices(fakeUserRepository);

            // Act
            var result = userService.UserLogin(email, password);

            // Assert
            result.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task AddUserBySuperAdmin_Returns_OkObjectResult_WhenUserIsAddedSuccessfully()
        {
            // Arrange
            var fakeUserService = A.Fake<IUser>();
            var controller = new UserController(fakeUserService);
            var fakeUser = A.Fake<ClaimsPrincipal>();

            var name = "John Doe";
            var email = "john.doe@example.com";
            var genders = Genders.Male;
            var phone = "1234567890";
            var dob = "2000-01-01";
            var permissionId = 1;
            var status = UserStatus.Active;

            A.CallTo(() => fakeUser.Identity.Name).Returns("Admin");
            A.CallTo(() => fakeUserService.AddNewUser(name, email, genders, phone, dob,status, permissionId, fakeUser)).Returns(Task.FromResult("Add User Success!!!"));

            // Set up the HttpContext.User to use the fake ClaimsPrincipal
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = fakeUser }
            };

            // Act
            var actionResult = await controller.AddUserBySuperAdmin(name, email, genders, phone, dob,status, permissionId);

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();

            var okResult = actionResult as OkObjectResult;
            okResult.Value.Should().Be("Add User Success!!!");
        }
        [Fact]
        public async Task AddUserBySuperAdmin_Returns_BadRequestResult_WhenUserIsNotAddedSuccessfully()
        {
            // Arrange
            var fakeUserService = A.Fake<IUser>();
            var controller = new UserController(fakeUserService);
            var fakeUser = A.Fake<ClaimsPrincipal>();

            var name = "John Doe";
            var email = "john.doe@example.com";
            var genders = Genders.Male;
            var phone = "1234567890";
            var dob = "2000-01-01";
            var status = UserStatus.Active;
            var permissionId = 0;

            A.CallTo(() => fakeUser.Identity.Name).Returns("Admin");
            A.CallTo(() => fakeUserService.AddNewUser(name, email, genders, phone, dob,status, permissionId, fakeUser)).Returns(Task.FromResult("Add User Failed!!!"));

            // Set up the HttpContext.User to use the fake ClaimsPrincipal
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = fakeUser }
            };

            // Act
            var actionResult = await controller.AddUserBySuperAdmin(name, email, genders, phone, dob,status, permissionId);

            // Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = actionResult as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Add User Failed!!!");
        }





        [Fact]
        public void UpdateUser_WithValidUser_ShouldUpdateUserSuccessfully()
        {
            // Arrange
            var existingUser = new User
            {
                UserId = Guid.NewGuid(),
                Name = "Jane",
                Email = "abc@gmail.com",
                Password = "avc",
                Phone = "8471733321",
                DOB = DateTime.Now,
                Gender = 0,
                ModifiedBy = "string"
            };

            var updatedUser = new UpdateUserViewModel
            {
                UserId = existingUser.UserId,
                Name = "Updated Name",
                Email = "updated@example.com",
                Password = "1212",
                Phone = "8471733322",
                DOB = DateTime.Now,
                Gender = 0,
                ModifiedBy = "string"
            };

            var fakeUserRepository = A.Fake<UserRepository>();
            A.CallTo(() => fakeUserRepository.Get(A<Expression<Func<User, bool>>>._)).Returns(existingUser);

            var userService = new UserServices(fakeUserRepository);

            // Act
            var result = userService.UpdateUser(updatedUser);

            // Assert
            result.Should().Be("Update successfully!!!");

            // Additional assertions (if required)
            A.CallTo(() => fakeUserRepository.Update(existingUser)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUserRepository.SaveChanges()).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void DeleteUser_WithValidUserId_ShouldDeleteUserSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var fakeUserRepository = A.Fake<UserRepository>();

            // Act
            var userService = new UserServices(fakeUserRepository);
            var result = userService.DeleteUser(userId);

            // Assert
            A.CallTo(() => fakeUserRepository.Get(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUserRepository.Delete(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUserRepository.SaveChanges()).MustHaveHappenedOnceExactly();
            Assert.True(result);
        }
    }
}
