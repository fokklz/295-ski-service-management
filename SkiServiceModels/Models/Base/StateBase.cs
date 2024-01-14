using MongoDB.Bson.Serialization.Attributes;
using SkiServiceModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.Base
{
    public class StateBase : IGenericModel
    {
        [StringLength(20)]
        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;

    }
}
