using MongoDB.Driver;

namespace uBeac.Repositories.MongoDB
{
    public interface IMongoDBContext
    {
        IMongoDatabase Database { get; }
    }

    public class MongoDBContext : IMongoDBContext
    {
        public MongoDBContext(MongoDBOptions<MongoDBContext> option)
        {
            var mongoUrl = new MongoUrl(option.ConnectionString);
            var client = new MongoClient(mongoUrl);
            Database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}
