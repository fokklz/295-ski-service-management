using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class State
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

    }
}
