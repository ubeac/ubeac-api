using MongoDB.Driver;

namespace uBeac.Web.Logging.MongoDB;

public class HttpLoggingMongoDbRepository : IHttpLoggingRepository
{
    protected readonly IHttpLoggingMongoDbContext Context;
    protected readonly HttpLoggingMongoDbOptions Options;

    public HttpLoggingMongoDbRepository(IHttpLoggingMongoDbContext context, HttpLoggingMongoDbOptions options)
    {
        Context = context;
        Options = options;
    }

    public async Task Create(HttpLog log, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var collectionName = GetCollectionName(log.StatusCode);

        var database = GetDatabase(log.StatusCode);
        var collection = database.GetCollection<HttpLog>(collectionName);

        await collection.InsertOneAsync(log, new InsertOneOptions(), cancellationToken);
    }

    private IMongoDatabase GetDatabase(int statusCode) => statusCode switch
    {
        < 500 and >= 400 => Context.HttpLog4xxDatabase,
        >= 500 => Context.HttpLog5xxDatabase,
        _ => Context.HttpLog2xxDatabase
    };

    private string GetCollectionName(int statusCode) => statusCode switch
    {
        < 500 and >= 400 => Options.HttpLog4xxCollectionName,
        >= 500 => Options.HttpLog5xxCollectionName,
        _ => Options.HttpLog2xxCollectionName
    };
}