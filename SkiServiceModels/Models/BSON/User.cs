using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SkiServiceModels.Interfaces;
using SkiServiceModels.Models.Base;

namespace SkiServiceModels.Models.BSON
{
    [BsonIgnoreExtraElements]
    public class User : UserBase, IGenericBSONModel
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
    }
}
