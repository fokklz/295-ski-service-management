using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Update Service Request DTO
    /// </summary>
    public class UpdateServiceRequest : IRequestDTO
    {
        [StringLength(50)]
        public string? Name { get; set; } = null;

        [StringLength(1000)]
        public string? Description { get; set; } = null;

        [Range(1, 1000)]
        public int? Price { get; set; } = null;
    }
}
