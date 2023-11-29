using SkiServiceAPI.Common;
using SkiServiceModels;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Enums;

namespace SkiServiceAPI.Interfaces
{
    public interface IUserService : IBaseService<User, UserResponse, UserResponseAdmin, UpdateUserRequest, CreateUserRequest>
    {
        Task<TaskResult<object>> GetMe();
        Task CreateSeed(string username, string password, RoleNames role = RoleNames.User);
        Task<TaskResult<object>> UnlockAsync(int id);
        Task<LoginResult> VerifyPasswordAsync(string username, string password);
    }
}   