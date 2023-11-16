using SkiServiceAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class Priority : IGenericModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public int Days { get; set; }

        // just for the moment, should be removed later since this is not SOLID
        // did not find a satisfying solution yet - did not search eather haha
        public Task<bool> ValidateAsync()
        {
            if(string.IsNullOrEmpty(Name) || Name.Length > 20)
            {
                return Task.FromResult(false);
            }

            if(Days <= 0)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
