using SkiServiceModels.Enums;

namespace SkiServiceModels.DTOs.Responses
{
    /// <summary>
    /// User Response DTO
    /// </summary>
    public class UserResponse 
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool Locked { get; set; }
        public RoleNames Role { get; set; }

    }

    public class UserResponseAdmin : UserResponse
    {
        public bool IsDeleted { get; set; }
        public int LoginAttempts { get; set; } = 0;
    }
}
