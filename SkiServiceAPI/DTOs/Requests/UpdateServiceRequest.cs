using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Requests
{
    /// <summary>
    /// Update Service Request DTO
    /// </summary>
    public class UpdateServiceRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? Price { get; set; }
    }
}
