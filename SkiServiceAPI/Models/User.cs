using SkiServiceAPI.Data;
using System.ComponentModel.DataAnnotations;
using SkiServiceAPI.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SkiServiceAPI.Models
{
    public class User : IGenericModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool Locked {  get; set; } = false;

        public RoleNames Role { get; set; } = RoleNames.Mitarbeiter;

        // just for the moment, should be removed later since this is not SOLID
        // did not find a satisfying solution yet - did not search eather haha
        public Task<bool> ValidateAsync()
        {
            if(string.IsNullOrEmpty(Username) || Username.Length > 50)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
