using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Create State Request DTO
    /// </summary>
    public class CreateStateRequest
    {

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
