using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FamsAPI.IServices;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FamsAPI.Test.Services.UserServices
{
    public class UserLogin
    {
        [Fact]
        public void UserLogin_ValidationData_Returns_Object_With_Data()
        {
            var userRepo = A.Fake<UserRepository>();
            string email = "locttse1@fpt.com";
            string password = FamsAPI.Services.UserServices.HashAndTruncatePassword("123@");

            A.CallTo(() => userRepo.Add(new User
            {
                Email = "locttse1@fpt.com",
                Password = password
            }));

            var expectedUser = new User
            {
                Email = "locttse1@fpt.com",
                Password = password
            };

            A.CallTo(() => userRepo.Get(A<Expression<Func<User, bool>>>.Ignored))
    .Returns(expectedUser);

            var userService = new FamsAPI.Services.UserServices(userRepo);

            // Act
            var result = userService.UserLogin(email, password);

            // Assert
            result.Should().BeEquivalentTo(expectedUser);


        }

        [Fact]
        public void UserLogin_InvalidData_Return_Object_With_Null()
        {
            var userRepo = A.Fake<UserRepository>();

            string email = "locttse1@fpt.com";
            string password = FamsAPI.Services.UserServices.HashAndTruncatePassword("123@");

            string passwordExpected = FamsAPI.Services.UserServices.HashAndTruncatePassword("12345");

            A.CallTo(() => userRepo.Get(A<Expression<Func<User, bool>>>.Ignored))
    .Returns(null);

            var userService = new FamsAPI.Services.UserServices(userRepo);

            // Act
            var result = userService.UserLogin(email, password);

            // Assert
            result.Should().BeNull();
        }
    }
}
