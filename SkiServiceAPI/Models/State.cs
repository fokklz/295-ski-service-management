using SkiServiceAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class State : IGenericModel
    {

        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

    }
}
