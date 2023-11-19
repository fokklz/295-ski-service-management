using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Common;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SkiServiceAPI.Services
{
    public class OrderService : GenericService<Order, OrderResponse, UpdateOrderRequest, CreateOrderRequest>, IOrderService
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;

        public OrderService(IApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
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

        /// <summary>
        /// Get all Orders by User
        /// </summary>
        /// <param name="userId">The target User</param>
        /// <returns>All Orders assigned to that user</returns>
        public async Task<TaskResult<List<OrderResponse>>> GetByUserAsync(int userId)
        {
            var query = _context.Orders.Where(e => e.UserId == userId);
            var filtered = ApplyFilter(query);
            var orders = await filtered.ToListAsync();

            return TaskResult<List<OrderResponse>>.Success(_mapper.Map<List<OrderResponse>>(orders));
        }

        /// <summary>
        /// Get all Orders by Priority
        /// </summary>
        /// <param name="priorityId">The target Priority</param>
        /// <returns>All Orders assigned to that priority</returns>
        public async Task<TaskResult<List<OrderResponse>>> GetByPriorityAsync(int priorityId)
        {
            var query = _context.Orders.Where(e => e.PriorityId == priorityId);
            var filtered = ApplyFilter(query);
            var orders = await filtered.ToListAsync();

            return TaskResult<List<OrderResponse>>.Success(_mapper.Map<List<OrderResponse>>(orders));
        }

        /// <summary>
        /// Get all Orders by State
        /// </summary>
        /// <param name="priorityId">The target State</param>
        /// <returns>All Orders assigned to that state</returns>
        public async Task<TaskResult<List<OrderResponse>>> GetByStateAsync(int stateId)
        {
            var query = _context.Orders.Where(e => e.StateId == stateId);
            var filtered = ApplyFilter(query);
            var orders = await filtered.ToListAsync();

            return TaskResult<List<OrderResponse>>.Success(_mapper.Map<List<OrderResponse>>(orders));
        }

        /// <summary>
        /// Get all Orders by State
        /// </summary>
        /// <param name="priorityId">The target Service</param>
        /// <returns>All Orders assigned to that service</returns>
        public async Task<TaskResult<List<OrderResponse>>> GetByServiceAsync(int serviceId)
        {
            var query = _context.Orders.Where(e => e.ServiceId == serviceId);
            var filtered = ApplyFilter(query);
            var orders = await filtered.ToListAsync();

            return TaskResult<List<OrderResponse>>.Success(_mapper.Map<List<OrderResponse>>(orders));
        }
    }
}