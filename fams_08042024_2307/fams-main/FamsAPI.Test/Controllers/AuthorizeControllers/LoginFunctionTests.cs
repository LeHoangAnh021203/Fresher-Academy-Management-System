using System;
using FakeItEasy;
using FamsAPI.IServices;
using DataLayer.Entities;
using FamsAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FamsAPI.Services;

namespace FamsAPI.Test.Controllers.AuthorizeControllers
{
	public class LoginFunctionTests
	{
		/*[Fact]
		public void Login_LoginWithExistingAccount_Returns_OkResultWithToken()
		{
			//Arrange
			var userService = A.Fake<IUser>();
			var refreshHanderService = A.Fake<IRefreshHandler>();

			string password = FamsAPI.Services.UserServices.HashAndTruncatePassword("123@");

			A.CallTo(() => userService.GetUserByEmail("locttse1@gmail.com")).Returns(
				new User
				{
					Email = "locttse1@gmail.com",
					Password = password,
					Name = "Loc"
				}
			);

			var testUser = new User
			{
				UserId = Guid.NewGuid(),
				PermissionId = 1,
				Password = "123@",
				Email = "locttse1@gmail.com",
				Name = "Loc"
			};
			var controller = new AuthorizeController(userService, refreshHanderService);
			//Act
			var result = controller.Login(testUser.Email, testUser.Password);
			//Assert
			var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
			okResult.Value.Should().NotBeNull();
		}*/

		[Fact]
		public void Login_LoginWithInvalidAccount_Returns_BadRequest()
		{
			var userService = A.Fake<IUser>();
            var refreshHanderService = A.Fake<IRefreshHandler>();
            string password = FamsAPI.Services.UserServices.HashAndTruncatePassword("123@");

			A.CallTo(() => userService.GetUserByEmail("locttse1@gmail.com")).Returns(

				new User
				{
					Email = "locttse1@gmail.com",
					Password = password,
					Name = "Loc"
				}
			);

			var testUser = new User
			{
				UserId = Guid.NewGuid(),
				PermissionId = 1,
				Password = "123@",
				Email = "loctt@gmail.com",
				Name = "Loc"
			};
			var controller = new AuthorizeController(userService, refreshHanderService);
			//Act
			var result = controller.Login(testUser.Email, testUser.Password);
			//Assert
			var okResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
			okResult.Value.Should().NotBeNull();
		}
	}
}

