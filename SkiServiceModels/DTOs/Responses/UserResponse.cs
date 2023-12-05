using SkiServiceModels.Enums;
using SkiServiceModels.Interfaces;

namespace SkiServiceModels.DTOs.Responses
{
    /// <summary>
    /// User Response DTO
    /// </summary>
    public class UserResponse : IResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool Locked { get; set; }
        public RoleNames Role { get; set; }

    }

    public class UserResponseAdmin : UserResponse, IResponseDTO
    {
        public bool IsDeleted { get; set; }
        public int LoginAttempts { get; set; } = 0;
    }
}
