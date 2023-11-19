using SkiServiceAPI.Common;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Interfaces
{
    public interface IOrderService : IBaseService<Order, OrderResponse, OrderResponseAdmin, UpdateOrderRequest, CreateOrderRequest>
    {
        Task<TaskResult<IEnumerable<object>>> GetByUserAsync(int userId);
        Task<TaskResult<IEnumerable<object>>> GetByPriorityAsync(int priorityId);
        Task<TaskResult<IEnumerable<object>>> GetByStateAsync(int stateId);
        Task<TaskResult<IEnumerable<object>>> GetByServiceAsync(int serviceId);
    }
}