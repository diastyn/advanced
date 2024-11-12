namespace Advanced.Shared.Settings;

public class MongoDbSetting
{
    //appsettings section
    public const string MongoDb = nameof(MongoDb);
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}