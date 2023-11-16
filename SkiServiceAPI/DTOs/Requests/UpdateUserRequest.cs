using SkiServiceAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Requests
{
    /// <summary>
    /// Update User Request DTO
    /// </summary>
    public class UpdateUserRequest
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public bool? Locked { get; set; }

        public RoleNames? Role { get; set; }
    }
}
