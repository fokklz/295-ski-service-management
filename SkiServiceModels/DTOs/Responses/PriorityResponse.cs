using SkiServiceModels.Interfaces;

namespace SkiServiceModels.DTOs.Responses
{
    public class PriorityResponse : IResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Days { get; set; }
    }

    public class PriorityResponseAdmin : PriorityResponse, IResponseDTO
    {
        public bool IsDeleted { get; set; }
    }
}
