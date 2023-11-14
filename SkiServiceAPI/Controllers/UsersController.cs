using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.DTOs.Responses;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UsersController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            if (!_userService.VerifyPassword(model.Username, model.Password))
            {
                return Unauthorized("Invalid Credentials");
            }

            var token = _tokenService.CreateToken(model.Username);
            return Ok(new LoginResponse()
            {
                Username = model.Username,
                Token = token
            });
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult create([FromBody] LoginRequest model)
        {
            _userService.CreateUser(model.Username, model.Password);

            var token = _tokenService.CreateToken(model.Username);
            return Ok(new LoginResponse()
            {
                Username = model.Username,
                Token = token
            });
        }
    }
}
