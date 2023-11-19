using SkiServiceAPI.Interfaces;

namespace SkiServiceAPI.DTOs.Responses
{
    public class StateResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    
    public class StateResponseAdmin : StateResponse
    {
        public bool IsDeleted { get; set; }
    }
}
