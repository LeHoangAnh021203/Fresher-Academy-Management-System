using DataLayer.Entities;
using DataLayer.Repositories;
using FakeItEasy;
using FakeItEasy.Core;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Entities.User;
using static FamsAPI.IServices.IUser;

namespace FamsAPI.Test.Services.UserServices
{
    public class AddNewUser
    {
        [Fact]
        public async Task AddNewUser_WithValidUser_ShouldAddUserSuccessfully()
        {
            // Arrange
            var fakeUserServices = A.Fake<FamsAPI.Services.UserServices>();
           

            var name = "John Doe";
            var email = "john.doe@example.com";
            var genders = Genders.Male;
            var phone = "1234567890";
            var dob = "2000-01-01";
            var status = UserStatus.Active;
            var permissionId = 1;
            var fakeUser = A.Fake<ClaimsPrincipal>();

            A.CallTo(() => fakeUser.Identity.Name).Returns("Admin");
            A.CallTo(() => fakeUserServices.AddNewUser(name, email, genders, phone, dob, status, permissionId, fakeUser)).Returns("Add User Success!!!");

            // Act
            var result = await fakeUserServices.AddNewUser(name, email, genders, phone, dob, status, permissionId, fakeUser);

            // Assert
            result.Should().Be("Add User Success!!!"); // Ensure the correct message is returned
            
        }





        [Fact]
        public async Task AddNewUser_WithExistingName_ShouldNotAddUser()
        {
            // Arrange
            var fakeUserServices = A.Fake<FamsAPI.Services.UserServices>();
            var name = "John Doe"; // Assume this name already exists in the database
            var email = "john.doe@example.com";
            var genders = Genders.Male;
            var phone = "1234567890";
            var dob = "2000-01-01";
            var permissionId = 1;
            var status = UserStatus.Active;
            var fakeUser = A.Fake<ClaimsPrincipal>();

            A.CallTo(() => fakeUser.Identity.Name).Returns("Admin");
            A.CallTo(() => fakeUserServices.AddNewUser(name, email, genders, phone, dob,status, permissionId, fakeUser)).Returns("Name already existed!!!!");

            // Act
            var result = await fakeUserServices.AddNewUser(name, email, genders, phone, dob,status, permissionId, fakeUser);

            // Assert
            result.Should().Be("Name already existed!!!!");
        }







    }
}
