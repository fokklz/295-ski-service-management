using SkiServiceModels.Interfaces;

namespace SkiServiceModels.DTOs.Responses
{
    /// <summary>
    /// Delete Response DTO
    /// </summary>
    public class DeleteResponse : IResponseDTO
    {
        public int Count { get; set; }
    }
}
