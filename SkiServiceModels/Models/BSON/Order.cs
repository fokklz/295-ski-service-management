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
        public ObjectId Id { get; set; }

        [BsonElement("priority_id")]
        public ObjectId PriorityId { get; set; }

        [BsonElement("service_id")]
        public ObjectId ServiceId { get; set; }

        [BsonElement("state_id")]
        public ObjectId StateId { get; set; }

        [BsonElement("user_id")]
        public ObjectId? UserId { get; set; } = null;

        [BsonElement("priority")]
        public virtual Priority Priority { get; set; }
        public bool ShouldSerializePriority() => false;

        [BsonElement("service")]
        public virtual Service Service { get; set; }
        public bool ShouldSerializeService() => false;

        [BsonElement("state")]
        public virtual State State { get; set; }
        public bool ShouldSerializeState() => false;

        [BsonElement("user")]
        public virtual User? User { get; set; }
        public bool ShouldSerializeUser() => false;

    }
}