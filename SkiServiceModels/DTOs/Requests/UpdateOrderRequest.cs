using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    public class UpdateOrderRequest : IRequestDTO
    {
        public int? ServiceId { get; set; } = null;

        public int? PriorityId { get; set; } = null;

        public int? StateId { get; set; } = null;

        public int? UserId { get; set; } = null;

        [StringLength(50)]
        public string? Name { get; set; } = null;

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; } = null;

        [StringLength(20)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; } = null;

        [StringLength(1000)]
        public string? Note { get; set; } = null;
    }
}
