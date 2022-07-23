using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Web.Logging.MongoDB;

public class MongoDbHttpLogRepository<TContext> : IHttpLogRepository
    where TContext : IMongoDBContext
{
    protected readonly TContext Context;
    protected readonly MongoDbHttpLogOptions Options;

    public MongoDbHttpLogRepository(TContext context, MongoDbHttpLogOptions options)
    {
        Context = context;
        Options = options;
    }

    public async Task Create(HttpLog log, CancellationToken cancellationToken = default)
    {
        var collectionName = Options.GetCollectionName(log.StatusCode);
        var collection = GetCollection(Context.Database, collectionName);
        await collection.InsertOneAsync(log, new InsertOneOptions(), cancellationToken);
    }

    protected virtual IMongoCollection<HttpLog> GetCollection(IMongoDatabase database, string collectionName)
        => database.GetCollection<HttpLog>(collectionName);
}