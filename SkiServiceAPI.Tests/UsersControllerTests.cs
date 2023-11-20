using Microsoft.AspNetCore.Mvc;
using SkiServiceAPI.Controllers;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Models;
using SkiServiceAPI.Services;
using SkiServiceAPI.Tests.Helper;
using SkiServiceAPI.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceAPI.Tests
{
    public class UsersControllerTests : ControllerTestsUtilty<UsersController, UserService>
    {
        public UsersControllerTests() : base()
        {

        }

        [Fact]
        public async Task Login_ShouldLoginUser()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var user = new LoginRequest()
            {
                Username = "Mitarbeiter 1",
                Password = "m1"
            };

            // Act
            var result = await controller.Login(user);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<LoginResponse>(res.Value);
            Assert.Equal(user.Username, item.Username);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Login_ShouldNotLoginUserWithWrongPassword()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var user = new LoginRequest()
            {
                Username = "Mitarbeiter 1",
                Password = "m2"
            };

            // Act
            var result = await controller.Login(user);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Login_ThreeFailsShouldLock()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var user = new LoginRequest()
            {
                Username = "Mitarbeiter 1", // has id 2
                Password = "m2"
            };

            // Act
            for (int i = 0; i < 3; i++)
            {
                var result = await controller.Login(user);
            }
     
            // Assert
            Assert.True(dbContext.Find<User>(2).Locked);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Login_ShouldNotLoginLockedUser()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var user = new LoginRequest()
            {
                Username = "Mitarbeiter 1", // has id 2
                Password = "m1"
            };
            dbContext.Find<User>(2).Locked = true;
            dbContext.SaveChanges();


            // Act
            var result2 = await controller.Login(user);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result2);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Login_ShouldNotLoginNonExistingUser()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var user = new LoginRequest()
            {
                Username = "Mitarbeiter 11",
                Password = "m11"
            };

            // Act
            var result = await controller.Login(user);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            dbContext.Dispose();
        }
    }
}
