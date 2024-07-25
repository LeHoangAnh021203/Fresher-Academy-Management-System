using DataLayer.Entities;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet.DependencyResolver;
#pragma warning disable
namespace FamsAPI.Test.Controllers.UsersControllers
{
    public class GetAllUser
    {
        [Fact]
        public void GetAllUser_Returns_OkObjectResult_With_Users()
        {
            //Arrange - nơi bắt đầu khởi tạo userService và mình sử dụng FakeItEasy(Framework) để tạo ra một bản fake của IUserService

            var userService = A.Fake<IUser>();

            //Mock behavior of GetAllUsers method - Gọi hàm trong userService ra và tạo data giả 

            A.CallTo(() => userService.GetAllUsers()).Returns(new List<User>
                {
                    new User
                    {
                        UserId = Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"),
                    },
                    new User
                    {
                        UserId = Guid.Parse("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"),
                    },
                    new User
                    {
                        UserId = Guid.Parse("df1756fa-9f21-4a47-8ff8-7c7d9ed54553"),
                    }
                });

            var controller = new UserController(userService);

            //Act -- Ở đây sẽ bắt đầu việc unit test
            var result = controller.GetAllUser();

            //Các điều kiện kiểm tra ở đây - sử dụng framework FluentAssertion để cho thấy thông báo lỗi rõ ràng hơn 
            result.Should().BeOfType<OkObjectResult>();

            var OkResult = (OkObjectResult)result;
            OkResult.Value.Should().BeAssignableTo<List<ViewModel.UserListModel>>();

            var users = (List<ViewModel.UserListModel>)OkResult.Value;

            users.Should().HaveCount(3); // Giả sử có 3 user trong list mình vừa cung cấp;
            users.Should().Contain(user => user.UserId == Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"));
            users.Should().Contain(user => user.UserId == Guid.Parse("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"));
            users.Should().Contain(user => user.UserId == Guid.Parse("df1756fa-9f21-4a47-8ff8-7c7d9ed54553"));
        }

        [Fact]
        public void GetAllUser_Returns_NotFound_With_UsersList()
        {

            var userService = A.Fake<IUser>();

            //Mock behavior of GetAllUsers method - Gọi hàm trong userService ra và tạo data giả 

            A.CallTo(() => userService.GetAllUsers()).Returns(new List<User>
            {

            });

            var controller = new UserController(userService);

            var result = controller.GetAllUser();

            result.Should().BeOfType<NotFoundObjectResult>();

        }




    }
}
