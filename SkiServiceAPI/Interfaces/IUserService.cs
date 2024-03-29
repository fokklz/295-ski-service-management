﻿using SkiServiceAPI.Common;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Enums;
using SkiServiceModels.Models.EF;

namespace SkiServiceAPI.Interfaces
{
    public interface IUserService : IBaseService<User, UserResponse, UserResponseAdmin, UpdateUserRequest, CreateUserRequest>
    {
        Task<TaskResult<object>> GetMe();
        Task<TaskResult<LoginResponse>> Login(LoginRequest model);
        Task<TaskResult<LoginResponse>> Refresh(RefreshRequest model);
        Task<TaskResult<object>> RevokeRefreshToken();
        Task CreateSeed(string username, string password, RoleNames role = RoleNames.User);
        Task<TaskResult<object>> UnlockAsync(int id);
        Task<LoginResult> VerifyPasswordAsync(string username, string password);
    }
}   