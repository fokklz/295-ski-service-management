using AutoMapper;
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
    }
}