using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Requests
{
    /// <summary>
    /// Update Priority Request DTO
    /// </summary>
    public class UpdatePriorityRequest
    {
        public string? Name { get; set; }

        public int? Days { get; set; }
    }
}
