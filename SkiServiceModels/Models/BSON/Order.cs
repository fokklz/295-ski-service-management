using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SkiServiceModels.Interfaces;
using SkiServiceModels.Models.Base;

namespace SkiServiceModels.Models.BSON
{
    [BsonIgnoreExtraElements]
    public class Order : OrderBase, IGenericBSONModel
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("priority_id")]
        public ObjectId PriorityId { get; set; }

        [BsonElement("service_id")]
        public ObjectId ServiceId { get; set; }

        [BsonElement("state_id")]
        public ObjectId StateId { get; set; }

        [BsonElement("user_id")]
        public ObjectId? UserId { get; set; } = null;

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