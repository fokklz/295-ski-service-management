using SkiServiceAPI.Common;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Interfaces;

namespace SkiServiceAPI.Interfaces
{
    public interface IBaseService<T, TResponseBase, TResponseAdmin, TUpdate, TCreate>
        where T : class, IGenericModel
        where TResponseBase : class
        where TResponseAdmin : class, TResponseBase
        where TUpdate : class
        where TCreate : class
    {
        Task<TaskResult<IEnumerable<object>>> GetAllAsync();
        Task<TaskResult<object>> GetAsync(int id);
        Task<TaskResult<object>> CreateAsync(TCreate entity);
        Task<TaskResult<object>> UpdateAsync(int id, TUpdate entity);
        Task<TaskResult<DeleteResponse>> DeleteAsync(int id);
    }
}
