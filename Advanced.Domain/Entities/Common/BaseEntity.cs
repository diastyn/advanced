using MongoDB.Bson.Serialization.Attributes;

namespace Advanced.Domain.Entities.Common;

public class BaseEntity : Base
{
    [BsonElement("CreatedAt")] public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [BsonElement("UpdateAt")]
    public DateTime UpdateAt { get; set; } = DateTime.Now;

    [BsonElement("IsDeleted")] public bool IsDeleted { get; set; } = false;
    
    [BsonElement("DeletedAt")]
    public DateTime? DeletedAt { get; set; }
}