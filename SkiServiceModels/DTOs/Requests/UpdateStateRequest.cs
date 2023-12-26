using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    /// <summary>
    /// Update State Request DTO
    /// </summary>
    public class UpdateStateRequest : IRequestDTO
    {
        [StringLength(20)]
        public string? Name { get; set; } = null;
    }
}
