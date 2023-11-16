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

namespace SkiServiceAPI.Services
{

    public class UserService : GenericService<User, UserResponse, UpdateUserRequest, CreateUserRequest>, IUserService
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;

        public UserService(IApplicationDBContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public override async Task<TaskResult<UserResponse>> UpdateAsync(int id, UpdateUserRequest entity)
        {
            var all = await _context.Users.ToListAsync();
            var current = await _context.Users.FindAsync(id);
            if (current == null) return TaskResult<UserResponse>.Error("Entry not Found");


            if (!string.IsNullOrWhiteSpace(entity.Password))
            {
                CreatePasswordHash(entity.Password, out byte[] passwordHash, out byte[] passwordSalt);
                current.PasswordHash = passwordHash;
                current.PasswordSalt = passwordSalt;
            }

            _mapper.Map(entity, current);
            await _context.SaveChangesAsync();
            return TaskResult<UserResponse>.Success(_mapper.Map<UserResponse>(current));
        }

        /// <summary>
        /// Overrides the CreateAsync Method from GenericService
        /// </summary>
        /// <param name="id">Id for the Entry</param>
        /// <param name="entity">Entity Data</param>
        /// <returns>UserResponse as TaskResult</returns>
        public override async Task<TaskResult<UserResponse>> CreateAsync(CreateUserRequest entity)
        {
            var user = _mapper.Map<User>(entity);

            if (!string.IsNullOrWhiteSpace(entity.Password))
            {
                CreatePasswordHash(entity.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            bool valid = await user.ValidateAsync();
            if (!valid) return TaskResult<UserResponse>.Error("Invalid User");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return TaskResult<UserResponse>.Success(_mapper.Map<UserResponse>(user));
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
        /// <returns>The user if valid else NULL</returns>
        public async Task<User?> VerifyPasswordAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            if(VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return user;
            }
            else
            {
                return null;
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
