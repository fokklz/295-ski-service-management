using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Create Priority Request DTO
    /// </summary>
    public class CreatePriorityRequest : IRequestDTO
    {

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(1, 365)]
        public int Days { get; set; }
    }
}
