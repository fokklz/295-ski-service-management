using System.ComponentModel.DataAnnotations;
using SkiServiceModels.Enums;
using SkiServiceModels.Interfaces;

namespace SkiServiceModels.Models.Base
{
    public class UserBase : IGenericModel
    {
        [StringLength(50)]
        public required string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool Locked { get; set; } = false;

        public RoleNames Role { get; set; } = RoleNames.User;

        public int LoginAttempts { get; set; } = 0;

        public string RefreshToken { get; set; } = null;

        public bool IsDeleted { get; set; } = false;

    }
}
