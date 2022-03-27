namespace uBeac.Repositories.MongoDB
{
    public class MongoDBOptions<T> where T : IMongoDBContext
    {
        public string ConnectionString { get; }
        public bool DropExistDatabase { get; set; } = false;

        public MongoDBOptions(string connectionString, bool dropExistDatabase = false)
        {
            ConnectionString = connectionString;
            DropExistDatabase = dropExistDatabase;
        }
    }
}
