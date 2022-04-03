namespace uBeac.Repositories.MongoDB;

public class MongoDBOptions
{
    public string ConnectionString { get; }
    public bool DropExistDatabase { get; set; }

    public bool HistoryEnabled { get; set; }
    public string HistoryConnectionString { get; set; }

    public MongoDBOptions(string connectionString, bool dropExistDatabase = false, bool historyEnabled = false, string historyConnectionString = null)
    {
        ConnectionString = connectionString;
        DropExistDatabase = dropExistDatabase;

        HistoryEnabled = historyEnabled;
        HistoryConnectionString = historyConnectionString;
    }
}

public class MongoDBOptions<TContext> : MongoDBOptions where TContext : IMongoDBContext
{
    public MongoDBOptions(string connectionString, bool dropExistDatabase = false, bool historyEnabled = false, string historyConnectionString = null)
        : base(connectionString, dropExistDatabase, historyEnabled, historyConnectionString)
    {
    }
}

