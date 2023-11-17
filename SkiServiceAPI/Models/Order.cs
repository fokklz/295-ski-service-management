using SkiServiceAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class Order : IGenericModel
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

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(1000)]
        public string? Note { get; set; } = null;

        public DateTime Created { get; set; } = DateTime.Now;

        public bool Deleted { get; set; } = false;

    }
}
