using SkiServiceAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool Locked {  get; set; } = false;

        public RoleNames Role { get; set; } = RoleNames.Mitarbeiter; 
    }
}
