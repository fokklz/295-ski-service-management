using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.DTOs.Requests
{
    public class CreateOrderRequest : IRequestDTO
    {
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public int PriorityId { get; set; }
        [Required]
        public int StateId { get; set; }

        public int? UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [StringLength(1000)]
        public string? Note { get; set; } = null;
    }
}
