namespace uBeac.Repositories.MongoDB
{
    public class MongoDBOptions<T> where T : IMongoDBContext
    {
        public string ConnectionString { get; }
        public MongoDBOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
