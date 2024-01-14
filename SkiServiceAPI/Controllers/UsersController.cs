using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiServiceAPI.Common;
using SkiServiceAPI.Interfaces;
using SkiServiceModels.DTOs.Requests;
using SkiServiceModels.DTOs.Responses;
using SkiServiceModels.Models.EF;

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
            return userInfo.IsOk ? Ok(userInfo.Response) : NotFound(userInfo.ErrorContent);
        }

        /// <summary>
        /// Revoke the refresh token of the current user
        /// </summary>
        /// <returns>Empty Success Response</returns>
        [HttpPost("revoke")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Revoke()
        {
            var result = await _userService.RevokeRefreshToken();
            return result.IsOk ? Ok() : BadRequest(result.ErrorContent);
        }

        /// <summary>
        /// Refresh the login by providing a valid refresh token and a valid access token
        /// </summary>
        /// <param name="model">RefreshRequest</param>
        /// <returns>LoginResponse</returns>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest model)
        {
            var result = await _userService.Refresh(model);
            return result.IsOk ? Ok(result.Response) : Unauthorized(result.ErrorContent);
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
            var result = await _userService.Login(model);
            return result.IsOk ? Ok(result.Response) : Unauthorized(result.ErrorContent);
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
            var result = await _userService.UnlockAsync(id);
            return result.IsOk ? Ok(result.Response) : BadRequest(result.ErrorContent);
        }
    }
}
