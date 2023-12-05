using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Update Priority Request DTO
    /// </summary>
    public class UpdatePriorityRequest : IRequestDTO
    {
        [StringLength(20)]
        public string? Name { get; set; }

        [Range(1, 365)]
        public int? Days { get; set; }
    }
}
