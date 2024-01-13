using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SkiServiceModels.Interfaces;
using SkiServiceModels.Models.Base;

namespace SkiServiceModels.Models.BSON
{
    public class Order : OrderBase, IGenericBSONModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string PriorityId { get; set; }
        public string ServiceId { get; set; }
        public string StateId { get; set; }
        public string? UserId { get; set; } = null;

        [BsonIgnore]
        public virtual Priority Priority { get; set; }
        [BsonIgnore]
        public virtual Service Service { get; set; }
        [BsonIgnore]
        public virtual State State { get; set; }
        [BsonIgnore]
        public virtual User? User { get; set; }

    }
}