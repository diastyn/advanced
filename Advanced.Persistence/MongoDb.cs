using Advanced.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Advanced.Persistence;

public sealed class MongoDb<T>
{
    private readonly IMongoDatabase _database;

    public MongoDb(IOptions<MongoDbSetting> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
        Bucket = new GridFSBucket(_database);
    }

    public IMongoCollection<T> GetCollection(string collectionName) 
        => _database.GetCollection<T>(collectionName);

    public IGridFSBucket Bucket { get; }
}