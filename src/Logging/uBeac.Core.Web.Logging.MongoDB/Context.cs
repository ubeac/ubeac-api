using MongoDB.Driver;

namespace uBeac.Web.Logging.MongoDB;

public interface IHttpLoggingMongoDbContext
{
    IMongoDatabase HttpLog2xxDatabase { get; }
    IMongoDatabase HttpLog4xxDatabase { get; }
    IMongoDatabase HttpLog5xxDatabase { get; }
}

public class HttpLoggingMongoDbContext : IHttpLoggingMongoDbContext
{
    public HttpLoggingMongoDbContext(HttpLoggingMongoDbOptions options)
    {
        HttpLog2xxDatabase = GetDatabase(options.HttpLog2xxConnectionString);
        HttpLog4xxDatabase = GetDatabase(options.HttpLog4xxConnectionString);
        HttpLog5xxDatabase = GetDatabase(options.HttpLog5xxConnectionString);
    }

    public IMongoDatabase HttpLog2xxDatabase { get; }
    public IMongoDatabase HttpLog4xxDatabase { get; }
    public IMongoDatabase HttpLog5xxDatabase { get; }

    private static IMongoDatabase GetDatabase(string connectionString)
    {
        var mongoUrl = new MongoUrl(connectionString);
        var client = new MongoClient(mongoUrl);
        return client.GetDatabase(mongoUrl.DatabaseName);
    }
}