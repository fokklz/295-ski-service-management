using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SkiServiceModels.Enums;
using SkiServiceModels.Interfaces;

namespace SkiServiceModels.Models.Base
{
    public class UserBase : IGenericModel
    {
        [StringLength(50)]
        [BsonElement("username")]
        public required string Username { get; set; }

        [BsonElement("password_hash")]
        public byte[] PasswordHash { get; set; }

        [BsonElement("password_salt")]
        public byte[] PasswordSalt { get; set; }

        [BsonElement("locked")]
        public bool Locked { get; set; } = false;

        [BsonElement("role")]
        [BsonRepresentation(BsonType.String)]
        public RoleNames Role { get; set; } = RoleNames.User;

        [BsonElement("login_attempts")]
        public int LoginAttempts { get; set; } = 0;

        [BsonElement("refresh_token")]
        public string? RefreshToken { get; set; } = null;

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;

    }
}
