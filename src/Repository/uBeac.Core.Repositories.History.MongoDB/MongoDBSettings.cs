namespace uBeac.Repositories.History.MongoDB;

public class MongoDBSettings
{
    public string ConnectionString { get; set; }
    public string CollectionSuffix { get; set; } = "_History";
}
