using System;
using System.Collections.Generic;
using FamsAPI.Controllers;
using FamsAPI.IServices;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using AutoMapper;
using FamsAPI.ViewModel;
using System.Linq.Expressions;
using DataLayer.Repositories;
#pragma warning disable
namespace FamsAPI.Test.Controllers.UserPermissionControllers
{
    public class UserPermissionControllerTests
    {
        [Fact]
        public void GetAllPermission_Returns_OkResult_With_All_Permissions()
        {
            // Arrange
            var permissions = new List<UserPermission>
            {
                new UserPermission { PermissionId = 1, PermissionName = "Permission1" },
                new UserPermission { PermissionId = 2, PermissionName = "Permission2" },
                new UserPermission { PermissionId = 3, PermissionName = "Permission3" }
            };

            var mockUserService = new Mock<IUserPermission>();
            mockUserService.Setup(service => service.GetAll()).Returns(permissions);

            var controller = new UserPermissionController(mockUserService.Object);

            // Act
            var result = controller.GetAllPermission();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPermissions = Assert.IsAssignableFrom<IEnumerable<UserPermission>>(okResult.Value);
            Assert.Equal(3, returnedPermissions.Count());
        }

        [Fact]
        public void PermissionMatrix_ReturnsBadRequest_WhenPermissionIdNotFound()
        {
            // Arrange
            var permissionId = 1;
            var mockUserPermission = new Mock<IUserPermission>();
            mockUserPermission.Setup(repo => repo.GetAll()).Returns(new List<UserPermission>());
            var controller = new UserPermissionController(mockUserPermission.Object);

            // Act
            var result = controller.PermissionMatrix(permissionId, new UserPermissionViewModel());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Id not Found!!!", badRequestResult.Value);
        }

        [Fact]
        public void PermissionMatrix_ReturnsOk_WhenPermissionIdFound()
        {
            // Arrange
            var permissionId = 1;
            var permissions = new List<UserPermission> { new UserPermission { PermissionId = permissionId } };
            var mockUserPermission = new Mock<IUserPermission>();
            mockUserPermission.Setup(repo => repo.GetAll()).Returns(permissions.ToList()); // Convert IQueryable to List
            var controller = new UserPermissionController(mockUserPermission.Object);

            // Act
            var result = controller.PermissionMatrix(permissionId, new UserPermissionViewModel());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }





    }
}
