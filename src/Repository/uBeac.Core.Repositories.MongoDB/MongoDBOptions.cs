namespace uBeac.Repositories.MongoDB;

public class MongoDBOptions
{
    public string ConnectionString { get; }

    public MongoDBOptions(string connectionString)
    {
        ConnectionString = connectionString;
    }
}

public class MongoDBOptions<TContext> : MongoDBOptions where TContext : IMongoDBContext
{
    public MongoDBOptions(string connectionString) : base(connectionString)
    {
    }
}

