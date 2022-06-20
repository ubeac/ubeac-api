using MongoDB.Driver;

namespace uBeac.Web.Logging.MongoDB;

public class MongoDbHttpLogRepository : IHttpLogRepository
{
    protected readonly MongoDbHttpLogOptions Options;

    public MongoDbHttpLogRepository(MongoDbHttpLogOptions options)
    {
        Options = options;
    }

    public async Task Create(HttpLog log, CancellationToken cancellationToken = default)
    {
        var connectionString = Options.ConnectionString;
        var collectionName = Options.GetCollectionName(log.StatusCode);

        var database = GetDatabase(connectionString);
        var collection = GetCollection(database, collectionName);

        await collection.InsertOneAsync(log, new InsertOneOptions(), cancellationToken);
    }

    protected virtual IMongoDatabase GetDatabase(string connectionString)
    {
        var mongoUrl = new MongoUrl(connectionString);
        var client = new MongoClient(mongoUrl);
        return client.GetDatabase(mongoUrl.DatabaseName);
    }

    protected virtual IMongoCollection<HttpLog> GetCollection(IMongoDatabase database, string collectionName)
        => database.GetCollection<HttpLog>(collectionName);
}