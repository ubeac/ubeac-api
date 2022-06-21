namespace uBeac.Repositories.MongoDB;

public class MongoDBOptions<TContext> where TContext : IMongoDBContext
{
    public string ConnectionString { get; }

    public MongoDBOptions(string connectionString)
    {
        ConnectionString = connectionString;
    }
}