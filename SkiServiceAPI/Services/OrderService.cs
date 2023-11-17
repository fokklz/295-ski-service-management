using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Common;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Services
{
    public class OrderService : GenericService<Order, OrderResponse, UpdateOrderRequest, CreateOrderRequest>, IOrderService
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;

        public OrderService(IApplicationDBContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Wrapper to propperly proxy the response since this won't be done at creation automatically
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>OrderResponse as TaskResult</returns>
        public override async Task<TaskResult<OrderResponse>> CreateAsync(CreateOrderRequest entity)
        {
            var newOrder = await base.CreateAsync(entity);
            if (!newOrder.IsOk || newOrder.Response == null) return newOrder;

            var proxied = _context.Orders
                .Include(e => e.User)
                .Include(e => e.Service)
                .Include(e => e.Priority)
                .Include(e => e.State)
                .FirstOrDefault(e => e.Id == newOrder.Response.Id);

            return TaskResult<OrderResponse>.Success(_mapper.Map<OrderResponse>(proxied));
        }

    }
}