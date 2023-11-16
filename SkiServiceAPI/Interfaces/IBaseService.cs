using SkiServiceAPI.Common;
using SkiServiceAPI.DTOs.Responses;

namespace SkiServiceAPI.Interfaces
{
    public interface IBaseService<T, TResponse, TUpdate, TCreate>
        where T : class, IGenericModel
        where TResponse : class
        where TUpdate : class
        where TCreate : class
    {
        Task<TaskResult<List<TResponse>>> GetAllAsync();
        Task<TaskResult<TResponse>> GetAsync(int id);
        Task<TaskResult<TResponse>> CreateAsync(TCreate entity);
        Task<TaskResult<TResponse>> UpdateAsync(int id, TUpdate entity);
        Task<TaskResult<DeleteResponse>> DeleteAsync(int id);
    }
}
