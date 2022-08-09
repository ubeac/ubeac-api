using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Web.Logging.MongoDB;

public class MongoDbHttpLogRepository<TContext> : IHttpLogRepository
    where TContext : IMongoDBContext
{
    protected readonly TContext Context;
    protected readonly MongoDbHttpLogOptions Options;
    private readonly MemoryCache _memoryCache;
    private const string CacheKey = "FailedLog";

    public MongoDbHttpLogRepository(TContext context, MongoDbHttpLogOptions options, HttpLogCache memoryCache)
    {
        Context = context;
        Options = options;
        _memoryCache = memoryCache.Cache;
    }

    public async Task Create(HttpLog log, CancellationToken cancellationToken = default)
    {
        var collectionName = Options.GetCollectionName(log.StatusCode);
        var collection = GetCollection(Context.Database, collectionName).WithWriteConcern(WriteConcern.Unacknowledged);        

        if (!_memoryCache.TryGetValue(CacheKey, out bool result))
        {
            try
            {
                await collection.InsertOneAsync(log, new InsertOneOptions(), cancellationToken);
                return;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(TimeoutException))
                {
                    _memoryCache.Set(CacheKey, true, TimeSpan.FromSeconds(Options.BypassLogTimeOut));
                    new Exception("HttpLog: A timeout occurred during connection to the Database!");
                }

                throw new Exception("HttpLog: Error in insert log to Database!");
            }
        }
        else
            throw new Exception("HttpLog: Log wasn't inserted in database during wait time!");
    }

    protected virtual IMongoCollection<HttpLog> GetCollection(IMongoDatabase database, string collectionName)
        => database.GetCollection<HttpLog>(collectionName);
}