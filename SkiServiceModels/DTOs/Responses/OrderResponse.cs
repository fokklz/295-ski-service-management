namespace SkiServiceModels.DTOs.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string? Note { get; set; }

        public DateTime Created { get; set; }

        // Navigation properties
        public UserResponse User { get; set; }
        public ServiceResponse Service { get; set; }
        public PriorityResponse Priority { get; set; }
        public StateResponse State { get; set; }
    }

    public class OrderResponseAdmin : OrderResponse
    {
        public bool IsDeleted { get; set; }

        // Navigation properties
        new public UserResponseAdmin User { get; set; }
        new public ServiceResponseAdmin Service { get; set; }
        new public PriorityResponseAdmin Priority { get; set; }
        new public StateResponseAdmin State { get; set; }
    }
}
