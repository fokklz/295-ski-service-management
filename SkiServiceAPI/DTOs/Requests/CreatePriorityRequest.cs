using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Requests
{
    /// <summary>
    /// Create Priority Request DTO
    /// </summary>
    public class CreatePriorityRequest
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public int Days { get; set; }
    }
}
