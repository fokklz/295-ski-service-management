using SkiServiceModels.Interfaces;
using SkiServiceModels.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.EF
{
    public class Order : OrderBase, IGenericEFModel
    {
        [Key]
        public int Id { get; set; }

        // Foreign keys
        public int ServiceId { get; set; }
        public int PriorityId { get; set; }
        public int StateId { get; set; }
        public int? UserId { get; set; } = null;

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Service Service { get; set; }
        public virtual Priority Priority { get; set; } 
        public virtual State State { get; set; }
    }
}
