using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.DTOs.Responses
{
    public class ServiceResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }
    }
}
