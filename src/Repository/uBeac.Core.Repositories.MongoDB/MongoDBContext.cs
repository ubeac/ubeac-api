using MongoDB.Driver;

namespace uBeac.Repositories.MongoDB;

public interface IMongoDBContext
{
    IMongoDatabase Database { get; }
}

public class MongoDBContext : IMongoDBContext
{
    public MongoDBContext(MongoDBOptions<MongoDBContext> options)
    {
        var mongoUrl = new MongoUrl(options.ConnectionString);
        var client = new MongoClient(mongoUrl);
        try
        {
            if (options.DropExistDatabase) client.DropDatabase(mongoUrl.DatabaseName);
        }
        catch
        {
            // ignored
        }
        Database = client.GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoDatabase Database { get; }
}

