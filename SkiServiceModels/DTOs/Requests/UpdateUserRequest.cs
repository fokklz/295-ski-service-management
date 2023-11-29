using System.ComponentModel.DataAnnotations;
using SkiServiceModels.Enums;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Update User Request DTO
    /// </summary>
    public class UpdateUserRequest
    {
        [StringLength(50)]
        public string? Username { get; set; }

        [MinLength(8)]
        public string? Password { get; set; }

        public bool? Locked { get; set; }

        public RoleNames? Role { get; set; }
    }
}
