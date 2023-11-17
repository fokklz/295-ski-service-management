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

        public int LoginAttempts { get; set; } = 0;
    }
}
