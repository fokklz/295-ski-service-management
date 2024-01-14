using MongoDB.Bson.Serialization.Attributes;
using SkiServiceModels.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.Base
{
    public class OrderBase : IGenericModel
    {
        [StringLength(50)]
        [BsonElement("name")]
        public required string Name { get; set; }

        [StringLength(100)]
        [BsonElement("email")]
        public required string Email { get; set; }

        [StringLength(20)]
        [BsonElement("phone")]
        public required string Phone { get; set; }

        [StringLength(1000)]
        [BsonElement("note")]
        public string? Note { get; set; } = null;

        [BsonElement("created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;

    }
}
