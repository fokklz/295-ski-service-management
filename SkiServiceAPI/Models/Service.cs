using SkiServiceAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class Service : IGenericModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public int Price { get; set; }

        // just for the moment, should be removed later since this is not SOLID
        // did not find a satisfying solution yet - did not search eather haha
        public Task<bool> ValidateAsync()
        {
            if(string.IsNullOrEmpty(Name) || Name.Length > 50)
            {
                return Task.FromResult(false);
            }

            if(string.IsNullOrEmpty(Description) || Description.Length > 1000)
            {
                return Task.FromResult(false);
            }

            if(Price <= 0)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
