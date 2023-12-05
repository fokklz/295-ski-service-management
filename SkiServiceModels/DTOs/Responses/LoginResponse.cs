using SkiServiceModels.Interfaces;

namespace SkiServiceModels.DTOs.Responses
{
    /// <summary>
    /// Login Response DTO
    /// </summary>
    public class LoginResponse : UserResponse, IResponseDTO
    {
        public TokenData Auth { get; set; }
    }
}
