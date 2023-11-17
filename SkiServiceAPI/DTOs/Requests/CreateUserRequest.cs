using SkiServiceAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Requests
{
    /// <summary>
    /// Create User Request DTO
    /// </summary>
    public class CreateUserRequest
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool? Locked { get; set; }

        public RoleNames? Role { get; set; }
    }
}
