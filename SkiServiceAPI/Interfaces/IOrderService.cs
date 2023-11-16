using SkiServiceAPI.Common;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Interfaces
{
    public interface IOrderService : IBaseService<Order, OrderResponse, UpdateOrderRequest, CreateOrderRequest>
    { }
}