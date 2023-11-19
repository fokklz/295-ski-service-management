using SkiServiceAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class Service : IGenericModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int Price { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
