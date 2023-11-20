using SkiServiceAPI.Common;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Interfaces
{
    public interface IUserService : IBaseService<User, UserResponse, UserResponseAdmin, UpdateUserRequest, CreateUserRequest>
    {
        Task<TaskResult<object>> GetMe();
        Task CreateSeed(string username, string password, RoleNames role = RoleNames.User);
        Task<TaskResult<UserResponse>> UnlockAsync(int id);
        Task<LoginResult> VerifyPasswordAsync(string username, string password);
    }
}   