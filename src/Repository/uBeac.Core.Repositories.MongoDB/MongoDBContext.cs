using MongoDB.Driver;

namespace uBeac.Repositories.MongoDB
{
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
            if (options.DropExistDatabase) client.DropDatabase(mongoUrl.DatabaseName);
            Database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}
