using DataLayer.Entities;
using FamsAPI.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace FamsAPI.Test.Controllers.UsersControllers
{
    public class SearchByFilter
    {
        private TestsFixture tf = new TestsFixture();       
        
        #region [Test] SearchByFiller
        [Fact]
        public void GetUsersByKeyword_FilterUID_Returns_UserListWithMatchingUserId()
        {
            var result = tf._userController.GetUserByKey("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72", "UID");

            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<List<UserListModel>>(); // Assert that the value is a List<User>

            var users = (List<UserListModel>)okResult.Value;

            users.Should().HaveCount(1);
            users.Should().Contain(user => user.UserId.ToString().Equals("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"));
            tf.Dispose();
        }
        #endregion

        [Fact]
        public void GetUsersByKeyword_FilterName_Returns_UserListWithMatchingName()
        {
            var result = tf._userController.GetUserByKey("ad", "name");

            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<List<UserListModel>>(); // Assert that the value is a List<User>

            var users = (List<UserListModel>)okResult.Value;

            users.Should().HaveCount(2);
            users.Should().Contain(user => user.Name == "SuperAdmin");
            users.Should().Contain(user => user.Name == "Admin");
            tf.Dispose();
        }

        [Fact]
        public void GetUsersByKeyword_FilterPhone_Returns_UserListWithMatchingPhone()
        { 
            var result = tf._userController.GetUserByKey("34", "phone");

            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeAssignableTo<List<UserListModel>>(); // Assert that the value is a List<User>

            var users = (List<UserListModel>)okResult.Value;

            users.Should().HaveCount(2);
            users.Should().Contain(user => user.PhoneNum.Contains("34"));

            tf.Dispose();
        }

        [Fact]
        public void GetUsersByKeyword_FilterEmails_Returns_EmptyList()
        {
            var result = tf._userController.GetUserByKey("@outlook", "email");

            result.Should().BeOfType<NotFoundObjectResult>();

            tf.Dispose();
        }

    }


}


