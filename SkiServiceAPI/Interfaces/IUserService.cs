using SkiServiceAPI.Common;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Interfaces
{
    public interface IUserService : IBaseService<User, UserResponse, UpdateUserRequest, CreateUserRequest>
    {
        Task CreateSeed(string username, string password, RoleNames role = RoleNames.Mitarbeiter);
        Task<TaskResult<UserResponse>> UnlockAsync(int id);
        Task<LoginResult> VerifyPasswordAsync(string username, string password);
    }
}   