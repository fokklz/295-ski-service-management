using MongoDB.Bson.Serialization.Attributes;
using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.Base
{
    public class ServiceBase : IGenericModel
    {
        [StringLength(50)]
        [BsonElement("name")]
        public required string Name { get; set; }

        [StringLength(1000)]
        [BsonElement("description")]
        public required string Description { get; set; }

        [BsonElement("price")]
        public int Price { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;

    }
}
