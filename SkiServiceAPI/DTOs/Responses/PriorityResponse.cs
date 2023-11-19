using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Responses
{
    public class PriorityResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Days { get; set; }
    }
}
