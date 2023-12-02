using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiServiceAPI.Common;
using SkiServiceAPI.Interfaces;
using SkiServiceModels;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;

namespace SkiServiceAPI.Controllers
{
    /// <summary>
    /// User Controller for CRUD operations
    /// Implements the Generic Controller for Users to infer the basic CRUD operations
    /// </summary>
    public class UsersController : GenericController<User, UserResponse, UserResponseAdmin, UpdateUserRequest, CreateUserRequest>
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseAdmin))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Me()
        {
            var userInfo = await _userService.GetMe();
            return userInfo.IsOk ? Ok(userInfo.Response) : NotFound(userInfo.Message);
        }

        [HttpPost("revoke")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Revoke()
        {
            var result = await _userService.RevokeRefreshToken();
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest model)
        {
            var result = await _tokenService.RefreshToken(model.Token, model.RefreshToken);
            if (result.Response == null)
            {
                return Unauthorized(result.Message);
            }

            var user = result.Response.User;

            return Ok(new LoginResponse()
            {
                Id = user.Id,
                Username = user.Username,
                Locked = user.Locked,
                Role = user.Role,
                Auth = result.Response.TokenData
            });
        }   


        /// <summary>
        /// Login a user and return a token along with their information
        /// </summary>
        /// <param name="model">LoginRequest</param>
        /// <returns>LoginResponse</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var result = await _userService.VerifyPasswordAsync(model.Username, model.Password);
            if (!result.Result || result.User == null)
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
            var token = await _tokenService.CreateToken(user, model.RememberMe);

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
            var userInfo = await _userService.GetMe();
            if (!userInfo.IsOk) return Unauthorized("User not found"); // should never happen due to [Authorize]
            if ((userInfo.Response as UserResponse).Id == id) return Unauthorized("You cannot unlock yourself");

            var result = await _userService.UnlockAsync(id);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.Message);
        }
    }
}
