using AutoMapper;
using Azure.Core;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SkiServiceAPI.Common;
using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;
using SkiServiceAPI.DTOs.Responses;

namespace SkiServiceAPI.Services
{

    public class UserService : GenericService<User, UserResponse, UserResponseAdmin, UpdateUserRequest, CreateUserRequest>, IUserService
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IApplicationDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Unlocks a User
        /// </summary>
        /// <param name="id">Id of the Entry</param>
        /// <returns>User</returns>
        public async Task<TaskResult<UserResponse>> UnlockAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return TaskResult<UserResponse>.Error("Entry not Found");
            if (!user.Locked) return TaskResult<UserResponse>.Error("User is not locked");


            user.Locked = false;
            await _context.SaveChangesAsync();
            return TaskResult<UserResponse>.Success(null);
        }

        /// <summary>
        /// Overrides the UpdateAsync Method from GenericService
        /// </summary>
        /// <param name="id">Id for the Entry</param>
        /// <param name="entity">Entity Data</param>
        /// <returns>UserResponse as TaskResult</returns>
        public override async Task<TaskResult<object>> UpdateAsync(int id, UpdateUserRequest entity)
        {
            var all = await _context.Users.ToListAsync();
            var current = await _context.Users.FindAsync(id);
            if (current == null) return TaskResult<object>.Error("Entry not Found");


            if (!string.IsNullOrWhiteSpace(entity.Password))
            {
                CreatePasswordHash(entity.Password, out byte[] passwordHash, out byte[] passwordSalt);
                current.PasswordHash = passwordHash;
                current.PasswordSalt = passwordSalt;
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
        public async Task CreateSeed(string username, string password, RoleNames role = RoleNames.Mitarbeiter)
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
            if (user == null) return new LoginResult {
                User = null,
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
