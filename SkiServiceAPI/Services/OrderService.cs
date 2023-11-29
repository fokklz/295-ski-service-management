using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Common;
using SkiServiceAPI.Interfaces;
using SkiServiceModels;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;

namespace SkiServiceAPI.Services
{
    public class OrderService : GenericService<Order, OrderResponse, OrderResponseAdmin, UpdateOrderRequest, CreateOrderRequest>, IOrderService
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
        public override async Task<TaskResult<object>> CreateAsync(CreateOrderRequest entity)
        {
            var newOrder = await base.CreateAsync(entity);
            if (!newOrder.IsOk || newOrder.Response == null) return newOrder;

            var proxied = _context.Orders
                .Include(e => e.User)
                .Include(e => e.Service)
                .Include(e => e.Priority)
                .Include(e => e.State)
                .FirstOrDefault(e => e.Id == (newOrder.Response as OrderResponse).Id);

            // we know its not null since we just created it
            return Resolve(proxied);

        }

        /// <summary>
        /// Get all Orders by User
        /// </summary>
        /// <param name="userId">The target User</param>
        /// <returns>All Orders assigned to that user</returns>
        public async Task<TaskResult<IEnumerable<object>>> GetByUserAsync(int userId)
        {
            var query = _context.Orders.Where(e => e.UserId == userId);
            var filtered = ApplyFilter(query);
            var orders = await filtered.ToListAsync();

            return ResolveList(orders);
        }

        /// <summary>
        /// Get all Orders by Priority
        /// </summary>
        /// <param name="priorityId">The target Priority</param>
        /// <returns>All Orders assigned to that priority</returns>
        public async Task<TaskResult<IEnumerable<object>>> GetByPriorityAsync(int priorityId)
        {
            var query = _context.Orders.Where(e => e.PriorityId == priorityId);
            var filtered = ApplyFilter(query);
            var orders = await filtered.ToListAsync();

            return ResolveList(orders);
        }

        /// <summary>
        /// Get all Orders by State
        /// </summary>
        /// <param name="priorityId">The target State</param>
        /// <returns>All Orders assigned to that state</returns>
        public async Task<TaskResult<IEnumerable<object>>> GetByStateAsync(int stateId)
        {
            var query = _context.Orders.Where(e => e.StateId == stateId);
            var filtered = ApplyFilter(query);
            var orders = await filtered.ToListAsync();

            return ResolveList(orders);
        }

        /// <summary>
        /// Get all Orders by State
        /// </summary>
        /// <param name="priorityId">The target Service</param>
        /// <returns>All Orders assigned to that service</returns>
        public async Task<TaskResult<IEnumerable<object>>> GetByServiceAsync(int serviceId)
        {
            var query = _context.Orders.Where(e => e.ServiceId == serviceId);
            var filtered = ApplyFilter(query);
            var orders = await filtered.ToListAsync();

            return ResolveList(orders);
        }
    }
}