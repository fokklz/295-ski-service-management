using SkiServiceModels.Interfaces;

namespace SkiServiceModels.DTOs.Responses
{
    public class StateResponse : IResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    
    public class StateResponseAdmin : StateResponse, IResponseDTO
    {
        public bool IsDeleted { get; set; }
    }
}
