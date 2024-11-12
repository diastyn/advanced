using Advanced.Domain.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Advanced.Domain.Entities;

public class UserProfile : BaseEntity
{
    [BsonElement("Name")]
    public string? Name { get; set; }
    
    [BsonElement("Surname")]
    public string? Surname { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId UserId { get; set; }
}