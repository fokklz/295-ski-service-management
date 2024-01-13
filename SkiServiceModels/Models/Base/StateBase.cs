using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.Base
{
    public class StateBase : IGenericModel
    {
        [StringLength(20)]
        public required string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
