using MongoDB.Driver;

namespace uBeac.Repositories.MongoDB;

public interface IMongoDBContext
{
    IMongoDatabase Database { get; }

    bool HistoryEnabled { get; }
    IMongoDatabase HistoryDatabase { get; }
}

public class MongoDBContext : IMongoDBContext
{
    public MongoDBContext(MongoDBOptions options)
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

        if (options.HistoryEnabled)
        {
            HistoryEnabled = options.HistoryEnabled;
            var historyMongoUrl = new MongoUrl(options.HistoryConnectionString);
            var historyClient = new MongoClient(historyMongoUrl);
            HistoryDatabase = historyClient.GetDatabase(mongoUrl.DatabaseName);
        }
    }

    public IMongoDatabase Database { get; }

    public bool HistoryEnabled { get; }
    public IMongoDatabase HistoryDatabase { get; }
}

