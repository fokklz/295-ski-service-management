﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SkiServiceModels.Interfaces;
using SkiServiceModels.Models.Base;

namespace SkiServiceModels.Models.BSON
{
    [BsonIgnoreExtraElements]
    public class State : StateBase, IGenericBSONModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
