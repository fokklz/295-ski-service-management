using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Requests
{
    /// <summary>
    /// Create Priority Request DTO
    /// </summary>
    public class CreatePriorityRequest
    {

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(1, 365)]
        public int Days { get; set; }
    }
}
