using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Requests
{
    public class UpdateOrderRequest
    {
        public int? ServiceId { get; set; }

        public int? PriorityId { get; set; }

        public int? StateId { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }

        [StringLength(1000)]
        public string? Note { get; set; } = null;
    }
}
