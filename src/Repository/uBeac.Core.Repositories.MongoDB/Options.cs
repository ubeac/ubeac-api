namespace uBeac.Repositories.MongoDB;

public class MongoDBOptions
{
    public string ConnectionString { get; }
    public bool DropExistDatabase { get; set; } = false;

    public MongoDBOptions(string connectionString, bool dropExistDatabase = false)
    {
        ConnectionString = connectionString;
        DropExistDatabase = dropExistDatabase;
    }
}

public class MongoDBOptions<TContext> : MongoDBOptions where T : IMongoDBContext
{
    public MongoDBOptions(string connectionString, bool dropExistDatabase = false) : base(connectionString, dropExistDatabase)
    {
    }
}

