using SkiServiceAPI.DTOs.Requests;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.DTOs.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }

        // Foreign keys
        public int ServiceId { get; set; }
        public int PriorityId { get; set; }
        public int StateId { get; set; }
        public int? UserId { get; set; }

        // Navigation properties
        public UserResponse User { get; set; }
        public Service Service { get; set; }
        public Priority Priority { get; set; }
        public State State { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string? Note { get; set; }

        public DateTime Created { get; set; }

        public bool Deleted { get; set; }
    }
}
