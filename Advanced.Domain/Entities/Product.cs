using Advanced.Domain.Entities.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Advanced.Domain.Entities;

public class Product : BaseEntity
{
    [BsonElement("Name")]
    public string Name { get; set; }
    
    [BsonElement("Description")]
    public string Description { get; set; }
    
    [BsonElement("Category")]
    public string? Category { get; set; }
    
    [BsonElement("Price")]
    public double Price { get; set; }
    
    // Link to image in GridFS
    [BsonElement("ImageId")]
    public ObjectId? ImageId { get; set; }

    public Product(string name, string description, string category, double price)
    {
        Name = name;
        Description = description;
        Category = category;
        Price = price;
    }
}