using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels
{
    public class State : IGenericModel
    {

        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
