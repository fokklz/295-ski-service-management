using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Update State Request DTO
    /// </summary>
    public class UpdateStateRequest
    {
        [StringLength(20)]
        public string? Name { get; set; }
    }
}
