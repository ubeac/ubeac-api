using uBeac.Repositories.MongoDB;

namespace uBeac.Repositories.History.MongoDB;

public class HistoryMongoDBContext : BaseMongoDBContext<HistoryMongoDBContext>
{
    public HistoryMongoDBContext(MongoDBOptions<HistoryMongoDBContext> dbOptions, BsonSerializationOptions bsonSerializationOptions) : base(dbOptions, bsonSerializationOptions)
    {
    }
}