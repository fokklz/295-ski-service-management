using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Create State Request DTO
    /// </summary>
    public class CreateStateRequest : IRequestDTO
    {

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
