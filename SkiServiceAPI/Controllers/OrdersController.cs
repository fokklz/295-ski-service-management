using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SkiServiceAPI.Common;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Controllers
{
    /// <summary>
    /// Basic CRUD Controller for Orders
    /// </summary>
    public class OrdersController : GenericController<Order, OrderResponse, UpdateOrderRequest, CreateOrderRequest>
    {

        private readonly IOrderService _service;

        public OrdersController(IOrderService service) : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Override the Create method to not require authentication
        /// </summary>
        /// <param name="entity">The Entity to create</param>
        /// <returns>The Created Entity</returns>
        [HttpPost]
        [AllowAnonymous]
        public override async Task<IActionResult> Create([FromBody] CreateOrderRequest entity)
        {
            var result = await _service.CreateAsync(entity);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }

        /// <summary>
        /// Get all Orders by User
        /// </summary>
        /// <param name="userId">The target User</param>
        /// <returns>all orders for that user</returns>
        [HttpGet("user/{userId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var result = await _service.GetByUserAsync(userId);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }

        /// <summary>
        /// Get all Orders by Priority Id
        /// </summary>
        /// <param name="priorityId">The Priority</param>
        /// <returns>all orders for that priority</returns>
        [HttpGet("priority/{priorityId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByPriority(int priorityId)
        {
            var result = await _service.GetByPriorityAsync(priorityId);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }

        /// <summary>
        /// Get all Orders by State Id
        /// </summary>
        /// <param name="stateId">The Sate</param>
        /// <returns>all orders for that priority</returns>
        [HttpGet("state/{stateId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByState(int stateId)
        {
            var result = await _service.GetByStateAsync(stateId);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }

        /// <summary>
        /// Get all Orders by Service Id
        /// </summary>
        /// <param name="serviceId">The Service</param>
        /// <returns>all orders for that service</returns>
        [HttpGet("service/{serviceId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByService(int serviceId) 
        {
            var result = await _service.GetByServiceAsync(serviceId);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }

    }
}
