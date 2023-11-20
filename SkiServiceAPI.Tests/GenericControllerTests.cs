using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Common;
using SkiServiceAPI.Controllers;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Models;
using SkiServiceAPI.Tests.Helper;
using SkiServiceAPI.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceAPI.Tests
{
    public class GenericControllerTests : ControllerTestsUtilty<StatesController, GenericService<State, StateResponse, StateResponseAdmin, UpdateStateRequest, CreateStateRequest>>
    {
        public GenericControllerTests() : base()
        {
        }

        #region STATUS_CODES
        // 401 Responses are not Testest since the middleware is not booted in tests

        [Fact]
        public async Task GetAll_ShouldReturn200Status()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 1;

            // Act
            var result = await controller.Get(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Get_ShouldReturn404Status()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 999;

            // Act
            var result = await controller.Get(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Create_ShouldReturn200Status()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var entity = MockStateData.NewItem();

            // Act
            var result = await controller.Create(entity);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            dbContext.Dispose();
        }

        /// <summary>
        /// Since middlewares are not booted in tests, we just expect a exception here
        /// </summary>
        [Fact]
        public async Task Create_ShouldReturn400Status()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(
                async () => await controller.Create(new CreateStateRequest())
            );

            dbContext.Dispose();
        }

        [Fact]
        public async Task Update_ShouldReturn200Status()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 1;
            var entity = MockStateData.NewItem() as object as UpdateStateRequest;

            // Act
            var result = await controller.Update(id, entity);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Update_ShouldReturn404Status()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 999;
            var entity = MockStateData.NewItem() as object as UpdateStateRequest;

            // Act
            var result = await controller.Update(id, entity);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Delete_ShouldReturn200Status()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 1;

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Delete_ShouldReturn404Status()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 999;

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            dbContext.Dispose();
        }
        #endregion

        #region MAPPINGS
        [Fact]
        public async Task GetAll_ShouldMapToResponseDTO()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();

            // Act
            var result = await controller.GetAll();

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var resValue = Assert.IsType<List<StateResponse>>(res.Value);
            Assert.Equal(MockStateData.Items().Count, resValue.Count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetAll_ShouldMapToAdminResponseDTO()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext(true);

            // Act
            var result = await controller.GetAll();

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var resValue = Assert.IsType<List<StateResponseAdmin>>(res.Value);
            Assert.Equal(MockStateData.Items().Count, resValue.Count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Get_ShouldMapToResposeDTO()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 1;

            // Act
            var result = await controller.Get(id);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var resValue = Assert.IsType<StateResponse>(res.Value);
            Assert.Equal(MockStateData.Items().First().Name, resValue.Name);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Get_ShouldMapToAdminResposeDTO()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext(true);
            var id = 1;

            // Act
            var result = await controller.Get(id);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var resValue = Assert.IsType<StateResponseAdmin>(res.Value);
            Assert.Equal(MockStateData.Items().First().Name, resValue.Name);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Update_ShouldMapToResposeDTO()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 1;
            var name = "New Name";

            // Act
            var result = await controller.Update(id, new UpdateStateRequest
            {
                Name = name
            });

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var resValue = Assert.IsType<StateResponse>(res.Value);
            Assert.Equal(name, resValue.Name);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Update_ShouldMapToAdminResposeDTO()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext(true);
            var id = 1;
            var name = "New Name";

            // Act
            var result = await controller.Update(id, new UpdateStateRequest
            {
                Name = name
            });

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var resValue = Assert.IsType<StateResponseAdmin>(res.Value);
            Assert.Equal(name, resValue.Name);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Create_ShouldMapToResposeDTO()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var entity = MockStateData.NewItem();

            // Act
            var result = await controller.Create(entity);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var resValue = Assert.IsType<StateResponse>(res.Value);
            Assert.Equal(entity.Name, resValue.Name);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Create_ShouldMapToAdminResposeDTO()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext(true);
            var entity = MockStateData.NewItem();

            // Act
            var result = await controller.Create(entity);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var resValue = Assert.IsType<StateResponseAdmin>(res.Value);
            Assert.Equal(entity.Name, resValue.Name);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Delete_ShouldBeDeleteResponse()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 1;

            // Act
            var result = await controller.Delete(id);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var resValue = Assert.IsType<DeleteResponse>(res.Value);
            dbContext.Dispose();
        }
        #endregion

        [Fact]
        public async Task GetAll_ShouldHideDeletedToUser()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 1;

            // Act
            var result = await controller.Delete(id);
            var result2 = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var res = Assert.IsType<OkObjectResult>(result2);
            var resValue = Assert.IsType<List<StateResponse>>(res.Value);
            Assert.Equal(MockStateData.Items().Count - 1, resValue.Count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetAll_ShouldShowDeletedToAdmin()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext(true);
            var id = 1;

            // Act
            var result = await controller.Delete(id);
            var result2 = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var res = Assert.IsType<OkObjectResult>(result2);
            var resValue = Assert.IsType<List<StateResponseAdmin>>(res.Value);
            Assert.Equal(MockStateData.Items().Count, resValue.Count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Get_ShouldHideDeletedToUser()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 1;

            // Act
            var result = await controller.Delete(id);
            var result2 = await controller.Get(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<NotFoundObjectResult>(result2);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Get_ShouldShowDeletedToAdmin()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext(true);
            var id = 1;

            // Act
            var result = await controller.Delete(id);
            var result2 = await controller.Get(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OkObjectResult>(result2);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Update_ShouldHideDeletedToUser()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var id = 1;
            var name = "New Name";

            // Act
            var result = await controller.Delete(id);
            var result2 = await controller.Update(id, new UpdateStateRequest
            {
                Name = name
            });

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<NotFoundObjectResult>(result2);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Update_ShouldShowDeletedToAdmin()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext(true);
            var id = 1;
            var name = "New Name";

            // Act
            var result = await controller.Delete(id);
            var result2 = await controller.Update(id, new UpdateStateRequest
            {
                Name = name
            });

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OkObjectResult>(result2);
            dbContext.Dispose();
        }    
    
    }
}
