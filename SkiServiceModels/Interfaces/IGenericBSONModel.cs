using MongoDB.Bson;

namespace SkiServiceModels.Interfaces
{
    public interface IGenericBSONModel : IGenericModel
    {
        public ObjectId Id { get; set; }
    }
}
