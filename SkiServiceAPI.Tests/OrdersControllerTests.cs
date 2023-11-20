using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using SkiServiceAPI.Common;
using SkiServiceAPI.Controllers;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;
using SkiServiceAPI.Services;
using SkiServiceAPI.Tests.Helper;
using SkiServiceAPI.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceAPI.Tests
{
    /// <summary>
    /// We only test the methods that are not tested in the GenericControllerTests
    /// since we have not the full middleware stack running, excluding auth
    /// </summary>
    public class OrdersControllerTests : ControllerTestsUtilty<OrdersController, OrderService>
    {

        public OrdersControllerTests() : base()
        {

        }

        [Fact]
        public async Task Create_ShouldCreateBasicOrderProxieAndMapItWithoutAuth()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var order = MockOrderData.NewItem();

            // Act
            var result = await controller.Create(order);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<OrderResponse>(res.Value);
            Assert.Equal(order.ServiceId, item.Service.Id);
            Assert.Equal(order.PriorityId, item.Priority.Id);
            Assert.Equal(order.StateId, item.State.Id);
            Assert.Equal(order.Email, item.Email);
            Assert.Equal(order.Name, item.Name);
            Assert.Equal(order.Phone, item.Phone);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Create_ShouldCreateBasicOrderProxieAndMapItForAdmin()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext(true);
            var order = MockOrderData.NewItem();

            // Act
            var result = await controller.Create(order);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<OrderResponseAdmin>(res.Value);
            Assert.Equal(order.ServiceId, item.Service.Id);
            Assert.Equal(order.PriorityId, item.Priority.Id);
            Assert.Equal(order.StateId, item.State.Id);
            Assert.Equal(order.Email, item.Email);
            Assert.Equal(order.Name, item.Name);
            Assert.Equal(order.Phone, item.Phone);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Create_ShouldCreateOrderWithUserAndNoteProxieAndMapItWithoutAuth()
        {

            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var order = MockOrderData.NewItem(1, "a note");

            // Act
            var result = await controller.Create(order);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<OrderResponse>(res.Value);
            Assert.Equal(order.UserId, item.User.Id);
            Assert.Equal(order.ServiceId, item.Service.Id);
            Assert.Equal(order.PriorityId, item.Priority.Id);
            Assert.Equal(order.StateId, item.State.Id);
            Assert.Equal(order.Email, item.Email);
            Assert.Equal(order.Name, item.Name);
            Assert.Equal(order.Phone, item.Phone);
            Assert.Equal(order.Note, item.Note);
            dbContext.Dispose();
        }

        [Fact]
        public async Task Create_ShouldCreateOrderWithUserAndNoteProxieAndMapItForAdmin()
        {

            // Arrange
            var (dbContext, controller) = CreateTestContext(true);
            var order = MockOrderData.NewItem(1, "a note");

            // Act
            var result = await controller.Create(order);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<OrderResponseAdmin>(res.Value);
            Assert.Equal(order.UserId, item.User.Id);
            Assert.Equal(order.ServiceId, item.Service.Id);
            Assert.Equal(order.PriorityId, item.Priority.Id);
            Assert.Equal(order.StateId, item.State.Id);
            Assert.Equal(order.Email, item.Email);
            Assert.Equal(order.Name, item.Name);
            Assert.Equal(order.Phone, item.Phone);
            Assert.Equal(order.Note, item.Note);
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetByUser_ShouldReturnOnlyOrdersWithThatUser()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var user = dbContext.Users.First();
            var orders = dbContext.Orders.Where(e => e.UserId == user.Id).ToList();

            // Act
            var result = await controller.GetByUser(user.Id);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<OrderResponse>>(res.Value);
            Assert.Equal(orders.Count, items.Count);
            foreach (var item in items)
            {
                Assert.Equal(user.Id, item.User.Id);
            }
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetByState_ShouldReturnOnlyOrdersWithThatState()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var state = dbContext.States.First();
            var orders = dbContext.Orders.Where(e => e.StateId == state.Id).ToList();

            // Act
            var result = await controller.GetByState(state.Id);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<OrderResponse>>(res.Value);
            Assert.Equal(orders.Count, items.Count);
            foreach (var item in items)
            {
                Assert.Equal(state.Id, item.State.Id);
            }
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetByService_ShouldReturnOnlyOrdersWithThatService()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var service = dbContext.Services.First();
            var orders = dbContext.Orders.Where(e => e.ServiceId == service.Id).ToList();

            // Act
            var result = await controller.GetByService(service.Id);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<OrderResponse>>(res.Value);
            Assert.Equal(orders.Count, items.Count);
            foreach (var item in items)
            {
                Assert.Equal(service.Id, item.Service.Id);
            }
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetByPriority_ShouldReturnOnlyOrdersWithThatPriority()
        {
            // Arrange
            var (dbContext, controller) = CreateTestContext();
            var priority = dbContext.Priorities.First();
            var orders = dbContext.Orders.Where(e => e.PriorityId == priority.Id).ToList();

            // Act
            var result = await controller.GetByPriority(priority.Id);

            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<OrderResponse>>(res.Value);
            Assert.Equal(orders.Count, items.Count);
            foreach (var item in items)
            {
                Assert.Equal(priority.Id, item.Priority.Id);
            }
            dbContext.Dispose();
        }

    }
}
