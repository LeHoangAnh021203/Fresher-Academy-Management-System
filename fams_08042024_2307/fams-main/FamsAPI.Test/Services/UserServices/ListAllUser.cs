using DataLayer.Entities;
using FakeItEasy;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Services.UserService
{
    public class ListAllUser
    {
        [Fact]
        public void GetAllUser_Returns_ListUser_With_Users()
        {

            //Arrange - nơi bắt đầu khởi tạo userService và mình sử dụng FakeItEasy(Framework) để tạo ra một bản fake của IUserService
            var userService = A.Fake<IUser>();

            //Mock behavior of GetAllUsers method - Gọi hàm trong userService ra và tạo data giả 
            A.CallTo(() => userService.GetAllUsers()).Returns(new List<User>
                {
                    new User
                    {
                        UserId = Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"),
                        Name = "Super Admin",
                    },
                    new User
                    {
                        UserId = Guid.Parse("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"),
                        Name = "Trainer1",
                    },
                    new User
                    {
                        UserId = Guid.Parse("df1756fa-9f21-4a47-8ff8-7c7d9ed54553"),
                        Name = "Admin"
                    }
                });

            //Act -- Ở đây sẽ bắt đầu việc unit test
             var result = userService.GetAllUsers();

            //Assert
            //Các điều kiện kiểm tra ở đây - sử dụng framework FluentAssertion để cho thấy thông báo lỗi rõ ràng hơn 
            result.Should().BeOfType<List<User>>();

            var OkResult = (List<User>)result;
            OkResult.Should().BeAssignableTo<List<User>>();

            var users = (List<User>)OkResult.ToList();

            users.Should().HaveCount(3); // Giả sử có 3 user trong list mình vừa cung cấp;
            users.Should().Contain(user => user.UserId == Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"));
            users.Should().Contain(user => user.UserId == Guid.Parse("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"));
        }

        [Fact]
        public void GetAllUser_Returns_NullOrExceptions_With_Users()
        {
            //Arrange
            var userService = A.Fake<IUser>();

            //Mock behavior of GetAllUsers method - Gọi hàm trong userService ra và tạo data giả 
            A.CallTo(() => userService.GetAllUsers()).Returns(new List<User>
                {
                    
                });

            //Act -- Ở đây sẽ bắt đầu việc unit test
            var result = userService.GetAllUsers();

            //Assert
            
            result.Should().BeOfType<List<User>>();

            var OkResult = (List<User>)result;
            OkResult.Should().BeAssignableTo<List<User>>();

            var users = (List<User>)OkResult.ToList();

            users.Should().HaveCount(0); 
        }
    }
}
