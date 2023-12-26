using System.ComponentModel.DataAnnotations;
using SkiServiceModels.Enums;
using SkiServiceModels.Interfaces;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Update User Request DTO
    /// </summary>
    public class UpdateUserRequest : IRequestDTO
    {
        [StringLength(50)]
        public string? Username { get; set; } = null;

        [MinLength(8)]
        public string? Password { get; set; } = null;

        public bool? Locked { get; set; } = null;

        public RoleNames? Role { get; set; } = null;
    }
}
