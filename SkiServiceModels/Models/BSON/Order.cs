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


        [BsonElement("priority_id")]
        public string PriorityId { get; set; }

        [BsonElement("service_id")]
        public string ServiceId { get; set; }

        [BsonElement("state_id")]
        public string StateId { get; set; }

        [BsonElement("user_id")]
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