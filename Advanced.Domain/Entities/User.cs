using Advanced.Domain.Entities.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace Advanced.Domain.Entities;

public class User : BaseEntity
{
    [BsonElement("Email")]
    public string Email { get; set; } 
    [BsonElement("Password")]
    public string Password { get; set; }
    
    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Surname")]
    public string Surname { get; set; }
}