using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.Base
{
    public class PriorityBase : IGenericModel
    {
        [StringLength(20)]
        public required string Name { get; set; }

        public int Days { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
