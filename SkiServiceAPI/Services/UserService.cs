﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Common;
using SkiServiceAPI.Interfaces;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Enums;
using SkiServiceModels.Models.EF;
using System.Security.Claims;

namespace SkiServiceAPI.Services
{

    public class UserService : GenericService<User, UserResponse, UserResponseAdmin, UpdateUserRequest, CreateUserRequest>, IUserService
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;

        public UserService(IApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, ITokenService tokenService) : base(context, mapper, httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Login a User with the given Credentials
        /// </summary>
        /// <param name="model">The Credentials of the user</param>
        /// <returns>a login Response with all information of the loggedin user as well as a token</returns>
        public async Task<TaskResult<LoginResponse>> Login(LoginRequest model)
        {
            var result = await VerifyPasswordAsync(model.Username, model.Password);
            if (!result.Result || result.User == null)
            {

                if (result.User != null && result.User.Locked)
                {
                    return TaskResult<LoginResponse>.Error(LocalizationKey.USER_LOCKED);
                }
                else
                {
                    return TaskResult<LoginResponse>.Error(LocalizationKey.INVALID_CREDENTIALS);
                }
            }

            var user = result.User;
            var token = await _tokenService.CreateToken(user, model.RememberMe);

            return TaskResult<LoginResponse>.Success(new LoginResponse()
            {
                Id = user.Id,
                Username = user.Username,
                Locked = user.Locked,
                Role = user.Role,
                Auth = token
            });
        }

        /// <summary>
        /// Refreshes the Token of a User
        /// </summary>
        /// <param name="model">The RefreshRequest</param>
        /// <returns>a login Response with all information of the loggedin user as well as a token</returns>
        public async Task<TaskResult<LoginResponse>> Refresh(RefreshRequest model)
        {
            var result = await _tokenService.RefreshToken(model.Token, model.RefreshToken);
            if (result.Response == null)
            {
                return TaskResult<LoginResponse>.Error(LocalizationKey.INVALID_CREDENTIALS);
            }

            var user = result.Response.User;

            return TaskResult<LoginResponse>.Success(new LoginResponse()
            {
                Id = user.Id,
                Username = user.Username,
                Locked = user.Locked,
                Role = user.Role,
                Auth = result.Response.TokenData
            });
        }

        /// <summary>
        /// Will revoke the refresh token of the current user
        /// </summary>
        /// <returns>The user information</returns>
        public async Task<TaskResult<object>> RevokeRefreshToken()
        {
            var result = await GetMe();
            if (result.Response == null) return TaskResult<object>.Error(LocalizationKey.ENTRY_NOT_FOUND);

            var user = result.Response as User;
            if (user == null) return TaskResult<object>.Error(LocalizationKey.ENTRY_NOT_FOUND);

            user.RefreshToken = null;
            await _context.SaveChangesAsync();

            return Resolve(user);
        }

        /// <summary>
        /// Allow users to request their own information based on the submitted token
        /// </summary>
        /// <returns>the user information</returns>
        public async Task<TaskResult<object>> GetMe()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return TaskResult<object>.Error(LocalizationKey.ENTRY_NOT_FOUND);

            var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claim == null) return TaskResult<object>.Error(LocalizationKey.ENTRY_NOT_FOUND);

            var id = int.Parse(claim);
            return await GetAsync(id);
        }

        /// <summary>
        /// Unlocks a User
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <returns>User</returns>
        public async Task<TaskResult<object>> UnlockAsync(int id)
        {
            var userInfo = await GetMe();
            if (!userInfo.IsOk) return TaskResult<object>.Error(LocalizationKey.ENTRY_NOT_FOUND);
            if ((userInfo.Response as UserResponse).Id == id) return TaskResult<object>.Error(LocalizationKey.CANNOT_UNLOCK_SELF);

            var user = await _context.Users.FindAsync(id);
            if (user == null) return TaskResult<object>.Error(LocalizationKey.ENTRY_NOT_FOUND);
            if (!user.Locked) return TaskResult<object>.Error(LocalizationKey.USER_NOT_LOCKED);


            user.Locked = false;
            user.LoginAttempts = 0;
            await _context.SaveChangesAsync();
            return Resolve(user);
        }

        /// <summary>
        /// Overrides the UpdateAsync Method from GenericService
        /// </summary>
        /// <param name="id">Id for the Entry</param>
        /// <param name="entity">Entity Data</param>
        /// <returns>UserResponse as TaskResult</returns>
        public override async Task<TaskResult<object>> UpdateAsync(int id, UpdateUserRequest entity)
        {
            var current = await _context.Users.FindAsync(id);
            if (current == null) return TaskResult<object>.Error(LocalizationKey.ENTRY_NOT_FOUND);


            if (!string.IsNullOrWhiteSpace(entity.Password))
            {
                CreatePasswordHash(entity.Password, out byte[] passwordHash, out byte[] passwordSalt);
                current.PasswordHash = passwordHash;
                current.PasswordSalt = passwordSalt;
                current.RefreshToken = null;
            }

            _mapper.Map(entity, current);
            await _context.SaveChangesAsync();

            return Resolve(current);
        }

        /// <summary>
        /// Overrides the CreateAsync Method from GenericService
        /// </summary>
        /// <param name="id">Id for the Entry</param>
        /// <param name="entity">Entity Data</param>
        /// <returns>UserResponse as TaskResult</returns>
        public override async Task<TaskResult<object>> CreateAsync(CreateUserRequest entity)
        {
            var user = _mapper.Map<User>(entity);

            if (!string.IsNullOrWhiteSpace(entity.Password))
            {
                CreatePasswordHash(entity.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            return Resolve(user);
        }

        /// <summary>
        /// Helper Method to create Seed Data
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The Password</param>
        /// <param name="role">The Role</param>
        /// <returns></returns>
        public async Task CreateSeed(string username, string password, RoleNames role = RoleNames.User)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Verifies the Password of a User
        /// </summary>
        /// <param name="username">The Username</param>
        /// <param name="password">The Password</param>
        /// <returns>LoginResult(User User?, bool Result)</returns>
        public async Task<LoginResult> VerifyPasswordAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return new LoginResult 
            {
                User = null,
                Result = false
            };
            if (user.Locked) return new LoginResult 
            {
                User = user,
                Result = false
            };

            if(VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                user.LoginAttempts = 0;
                await _context.SaveChangesAsync();
                return new LoginResult {
                    User = user,
                    Result = true
                };
            }
            else
            {
                user.LoginAttempts++;
                if (user.LoginAttempts >= 3)
                {
                    user.Locked = true;
                }
                await _context.SaveChangesAsync();
                return new LoginResult
                {
                    User = user,
                    Result = false
                };
            }
        }

        /// <summary>
        /// Generate a Password Hash
        /// </summary>
        /// <param name="password">The Password</param>
        /// <param name="passwordHash">out Hash</param>
        /// <param name="passwordSalt">out Salt</param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Helper Method to verify a Password
        /// to encapsulate the logic using hmac
        /// </summary>
        /// <param name="password">The Password</param>
        /// <param name="passwordHash">The Hash</param>
        /// <param name="passwordSalt">The Salt</param>
        /// <returns>True if it maches</returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
