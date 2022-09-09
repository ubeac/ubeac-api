using uBeac.Repositories.MongoDB;

namespace uBeac.Web.Logging.MongoDB;

public class HttpLogMongoDBContext : BaseMongoDBContext<HttpLogMongoDBContext>
{
    public HttpLogMongoDBContext(MongoDBOptions<HttpLogMongoDBContext> dbOptions, BsonSerializationOptions bsonSerializationOptions) : base(dbOptions, bsonSerializationOptions)
    {
    }
}

