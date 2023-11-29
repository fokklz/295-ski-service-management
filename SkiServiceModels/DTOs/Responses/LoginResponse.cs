namespace SkiServiceModels.DTOs.Responses
{
    /// <summary>
    /// Login Response DTO
    /// </summary>
    public class LoginResponse : UserResponse
    {
        public TokenData Auth { get; set; }
    }
}
