using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Common;
using SkiServiceAPI.Interfaces;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Models.Base;
using SkiServiceModels.Models.EF;

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

        /// <summary>
        /// Good for now. allows to create default data
        /// </summary>
        public async Task CreateSeed()
        {
            // Extended lists of names, emails, and phones
            string[] names = {
                "Alex", "Jamie", "Jordan", "Sam", "Pat",
                "Taylor", "Casey", "Morgan", "Terry", "Cameron",
                "Reese", "Quinn", "Drew", "Skyler", "Blair"
            };
            string[] emails = {
                "alex@example.com", "jamie@example.com", "jordan@example.com", "sam@example.com", "pat@example.com",
                "taylor@example.com", "casey@example.com", "morgan@example.com", "terry@example.com", "cameron@example.com",
                "reese@example.com", "quinn@example.com", "drew@example.com", "skyler@example.com", "blair@example.com"
            };
            string[] phones = {
                "1234567890", "0987654321", "1122334455", "5566778899", "6677889900",
                "1112223333", "4445556666", "7778889999", "0001112222", "3334445555",
                "6667778888", "9990001111", "2223334444", "5556667777", "8889990000"
            };

            int[] extraDays = { -1, 0, 1 };

            int[] services = { 1, 2, 3, 4, 5 };
            int[] priorities = { 1, 2, 3 };

            // Randomly select one item from each list
            Random rnd = new Random();
            string name = names[rnd.Next(names.Length)];
            string email = emails[rnd.Next(emails.Length)];
            string phone = phones[rnd.Next(phones.Length)];
            DateTime created = DateTime.Now.AddDays(extraDays[rnd.Next(extraDays.Length)]);

            // Create a new Order
            var order = new Order
            {
                ServiceId = services[rnd.Next(services.Length)],
                PriorityId = priorities[rnd.Next(priorities.Length)],
                StateId = 1,
                Name = name,
                Email = email,
                Phone = phone,
                Created = created,
                IsDeleted = false
            };

            // Add the new Order to the database
            await _context.Orders.AddAsync(order);
        }
    }
}