using SkiServiceAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class State : IGenericModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public Task<bool> ValidateAsync()
        {
            if(string.IsNullOrEmpty(Name) || Name.Length > 20)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
