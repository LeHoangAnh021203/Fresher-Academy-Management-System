using System;
using FamsAPI.IServices;
using FakeItEasy;
using DataLayer.Entities;
using FamsAPI.Services;
using FamsAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Repositories;


namespace FamsAPI.Test.Controllers
{

    public class UserControllersTests
    {
        /*
        #region Fake Data Testing
        /// <summary>
        /// Fake-Data Testing By using FakeItEasy (Not-Recommended)
        /// </summary>
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

            var controller = new UserController(userService);

            //Act -- Ở đây sẽ bắt đầu việc unit test
            var result = controller.GetAllUser();

            //Các điều kiện kiểm tra ở đây - sử dụng framework FluentAssertion để cho thấy thông báo lỗi rõ ràng hơn 
            result.Should().BeOfType<OkObjectResult>();

            var OkResult = (OkObjectResult)result;
            OkResult.Value.Should().BeAssignableTo<List<User>>();

            var users = (List<User>)OkResult.Value;

            users.Should().HaveCount(3); // Giả sử có 3 user trong list mình vừa cung cấp;
            users.Should().Contain(user => user.UserId == Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"));
            users.Should().Contain(user => user.UserId == Guid.Parse("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"));
        }
        #endregion

        #region Real Data Testing
        [Fact]
        public void GetAllUser_Returns_OkObjectResult_With_Users_FromDatabase()
        {
            //
            //Arrange - nơi khai báo dữ liệu đầu vào;
            //Ở đây do mình sử dụng dữ liệu từ Database, để tránh mất dữ liệu thì mình sẽ sao chép sang một vùng
            //nhớ đệm để kiểm tra tránh gây thiệt hại đến dữ liệu trong database.
            var options = new DbContextOptionsBuilder<DataLayer.FAMSDBContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;

            //Khai báo dbContext Ảo
            using (var context = new DataLayer.FAMSDBContext(options))
            {
                //Thêm data ảo vào trong db
                context.Users.AddRange(new List<User>
                {
                    new User {UserId = Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"), Name = "SuperAdmin", },
                    new User {UserId = Guid.Parse("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"), Name = "Trainer1"},
                    new User {UserId = Guid.Parse("df1756fa-9f21-4a47-8ff8-7c7d9ed54553"), Name = "Admin"}
                });
                context.SaveChanges();
            }

            //Bây giờ sẽ sử dụng database ao để so sánh - lấy từ controller ra
            using (var context = new DataLayer.FAMSDBContext(options))
            {
                var userRepositories = new UserRepository(context);

                var userServices = new UserServices(userRepositories); 

                var controller = new UserController(userServices);

                // Act
                var result = controller.GetAllUser();

                // Assert
                result.Should().BeOfType<OkObjectResult>(); // Assert that the result is an OkObjectResult

                var okResult = (OkObjectResult)result;
                okResult.Value.Should().BeAssignableTo<List<User>>(); // Assert that the value is a List<User>

                var users = (List<User>)okResult.Value;
                users.Should().HaveCount(3); // Adjust the count based on your test data
            }




        }
        #endregion

        */


    }
}


