namespace SkiServiceModels.DTOs
{
    /// <summary>
    /// Token Data DTO
    /// </summary>
    public class TokenData
    {
        public string Token { get; set; }

        public string TokenType { get; set; } = "Bearer";

        public DateTime Issued { get; set; } = DateTime.UtcNow;

        public DateTime Expires { get; set; }
    }
}
