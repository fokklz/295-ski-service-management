﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Data;
using System.ComponentModel.DataAnnotations;
using SkiServiceAPI.Common;
using SkiServiceAPI.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace SkiServiceAPI.Controllers
{
    /// <summary>
    /// User Controller for CRUD operations
    /// Implements the Generic Controller for Users to infer the basic CRUD operations
    /// </summary>
    public class UsersController : GenericController<User, UserResponse, UpdateUserRequest, CreateUserRequest>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UsersController(IUserService userService, ITokenService tokenService): base(userService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Allow a user to get their own information based on the submitted token
        /// </summary>
        /// <returns>UserResponse</returns>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.NameId)?.Value;
            var userInfo = await _userService.GetAsync(int.Parse(userId));

            return userInfo.IsOk ? Ok(userInfo.Response) : NotFound(userInfo.Message);
        }

        /// <summary>
        /// Login a user and return a token along with their information
        /// </summary>
        /// <param name="model">LoginRequest</param>
        /// <returns>LoginResponse</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var result = await _userService.VerifyPasswordAsync(model.Username, model.Password);
            if (!result.Result)
            {
                if (result.User != null && result.User.Locked)
                {
                    return Unauthorized("User Locked");
                }
                else
                {
                    return Unauthorized("Invalid Credentials");
                }
            } 

            var user = result.User;
            var token = _tokenService.CreateToken(user.Id.ToString(), user.Username, user.Role);
            return Ok(new LoginResponse()
            {
                Id = user.Id,
                Username = user.Username,
                Locked = user.Locked,
                Role = user.Role,
                Auth = token
            });
        }
        
        /// <summary>
        /// Allow others to unlock locked users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/unlock")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Unlock(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == id.ToString()) return Unauthorized("You cannot unlock yourself");

            var result = await _userService.UnlockAsync(id);
            return result.IsOk ? Ok() : BadRequest(result.Message);
        }
    }
}
