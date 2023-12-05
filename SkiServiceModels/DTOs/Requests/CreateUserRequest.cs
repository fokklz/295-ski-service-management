using System.ComponentModel.DataAnnotations;
using SkiServiceModels.Enums;
using SkiServiceModels.Interfaces;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Create User Request DTO
    /// </summary>
    public class CreateUserRequest : IRequestDTO
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public bool? Locked { get; set; }

        public RoleNames? Role { get; set; }
    }
}
