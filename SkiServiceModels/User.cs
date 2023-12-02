using System.ComponentModel.DataAnnotations;
using SkiServiceModels.Enums;
using SkiServiceModels.Interfaces;

#nullable disable

namespace SkiServiceModels
{
    public class User : IGenericModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool Locked { get; set; } = false;

        public RoleNames Role { get; set; } = RoleNames.User;

        public int LoginAttempts { get; set; } = 0;

        public bool IsDeleted { get; set; } = false;

        public string RefreshToken { get; set; } = null;
    }
}
