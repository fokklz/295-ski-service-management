using MongoDB.Bson.Serialization.Attributes;
using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.Base
{
    public class PriorityBase : IGenericModel
    {
        [StringLength(20)]
        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("days")]
        public int Days { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;

    }
}
