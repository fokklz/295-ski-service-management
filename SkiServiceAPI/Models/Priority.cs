using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class Priority
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public int Days { get; set; }
    }
}
