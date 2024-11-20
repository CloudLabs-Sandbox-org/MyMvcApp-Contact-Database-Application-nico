
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Xunit;
using System.Linq;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfUsers()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<System.Collections.Generic.List<User>>(viewResult.ViewData.Model);
            Assert.Equal(UserController.userlist, model);
        }

        [Fact]
        public void Details_ReturnsAViewResult_WithAUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "Test User", Email = "test@example.com" };
            UserController.userlist.Add(user);

            // Act
            var result = controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(user, model);
        }

        [Fact]
        public void Create_Post_ReturnsARedirectAndAddsUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 2, Name = "New User", Email = "new@example.com" };

            // Act
            var result = controller.Create(user);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Contains(user, UserController.userlist);
        }

        [Fact]
        public void Edit_Post_ReturnsARedirectAndUpdatesUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 3, Name = "Edit User", Email = "edit@example.com" };
            UserController.userlist.Add(user);
            var updatedUser = new User { Id = 3, Name = "Updated User", Email = "updated@example.com" };

            // Act
            var result = controller.Edit(3, updatedUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            var editedUser = UserController.userlist.FirstOrDefault(u => u.Id == 3);
            Assert.Equal("Updated User", editedUser.Name);
            Assert.Equal("updated@example.com", editedUser.Email);
        }

        [Fact]
        public void Delete_Post_ReturnsARedirectAndRemovesUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 4, Name = "Delete User", Email = "delete@example.com" };
            UserController.userlist.Add(user);

            // Act
            var result = controller.DeleteConfirmed(4);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain(user, UserController.userlist);
        }
    }
}