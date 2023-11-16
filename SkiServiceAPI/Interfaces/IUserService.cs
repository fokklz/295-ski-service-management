using SkiServiceAPI.Common;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Interfaces
{
    public interface IUserService : IBaseService<User, UserResponse, UpdateUserRequest, CreateUserRequest>
    {
        Task CreateSeed(string username, string password, RoleNames role = RoleNames.Mitarbeiter);
        Task<RoleNames> GetRoleAsync(string username);
        Task<TaskResult<UserResponse>> UnlockAsync(int id);
        Task<User?> VerifyPasswordAsync(string username, string password);
    }
}