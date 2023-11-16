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

        [HttpPost]
        [AllowAnonymous]
        public override async Task<IActionResult> Create([FromBody] CreateOrderRequest entity)
        {
            var result = await _service.CreateAsync(entity);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }
    }
}
