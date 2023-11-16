using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Requests
{
    /// <summary>
    /// Create State Request DTO
    /// </summary>
    public class CreateStateRequest
    {

        [Required]
        public string Name { get; set; }
    }
}
