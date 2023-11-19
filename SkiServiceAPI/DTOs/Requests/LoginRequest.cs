using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Requests
{
    /// <summary>
    /// Login Request DTO
    /// </summary>
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
