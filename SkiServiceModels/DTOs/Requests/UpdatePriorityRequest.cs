using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Update Priority Request DTO
    /// </summary>
    public class UpdatePriorityRequest
    {
        [StringLength(20)]
        public string? Name { get; set; }

        [Range(1, 365)]
        public int? Days { get; set; }
    }
}
