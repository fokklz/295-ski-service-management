using SkiServiceAPI.Common;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Interfaces
{
    public interface IUserService : IBaseService<User, UserResponse, UserResponseAdmin, UpdateUserRequest, CreateUserRequest>
    {
        Task CreateSeed(string username, string password, RoleNames role = RoleNames.Mitarbeiter);
        Task<TaskResult<UserResponse>> UnlockAsync(int id);
        Task<LoginResult> VerifyPasswordAsync(string username, string password);
    }
}   