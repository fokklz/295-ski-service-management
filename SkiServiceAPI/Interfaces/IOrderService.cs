using SkiServiceAPI.Common;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Interfaces
{
    public interface IOrderService : IBaseService<Order, OrderResponse, UpdateOrderRequest, CreateOrderRequest>
    {
        Task<TaskResult<List<OrderResponse>>> GetByUserAsync(int userId);
        Task<TaskResult<List<OrderResponse>>> GetByPriorityAsync(int priorityId);
        Task<TaskResult<List<OrderResponse>>> GetByStateAsync(int stateId);
        Task<TaskResult<List<OrderResponse>>> GetByServiceAsync(int serviceId);
    }
}