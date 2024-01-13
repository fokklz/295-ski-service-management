using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.Base
{
    public class OrderBase : IGenericModel
    {
        [StringLength(50)]
        public required string Name { get; set; }

        [StringLength(100)]
        public required string Email { get; set; }

        [StringLength(20)]
        public required string Phone { get; set; }

        [StringLength(1000)]
        public string? Note { get; set; } = null;

        public DateTime Created { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

    }
}
