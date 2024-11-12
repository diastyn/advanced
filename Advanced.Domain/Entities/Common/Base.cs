using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Advanced.Domain.Entities.Common;

public abstract class Base
{
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } = new();
}