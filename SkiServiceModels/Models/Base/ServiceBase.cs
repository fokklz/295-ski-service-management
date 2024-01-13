using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.Base
{
    public class ServiceBase : IGenericModel
    {
        [StringLength(50)]
        public required string Name { get; set; }

        [StringLength(1000)]
        public required string Description { get; set; }

        public int Price { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
