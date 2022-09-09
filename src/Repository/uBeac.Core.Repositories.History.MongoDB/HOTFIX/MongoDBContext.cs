using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace uBeac.Repositories.MongoDB;

public interface IMongoDBContext
{
    IMongoDatabase Database { get; }
}

public abstract class BaseMongoDBContext<TContext> : IMongoDBContext where TContext : class, IMongoDBContext
{
    protected BaseMongoDBContext(MongoDBOptions<TContext> dbOptions, BsonSerializationOptions bsonSerializationOptions)
    {
        ConfigureBsonSerialization(bsonSerializationOptions);
        ConfigureDatabase(dbOptions);
    }

    private void ConfigureDatabase(MongoDBOptions<TContext> options)
    {
        var mongoUrl = new MongoUrl(options.ConnectionString);
        var client = new MongoClient(mongoUrl);
        Database = client.GetDatabase(mongoUrl.DatabaseName);
    }

    private void ConfigureBsonSerialization(BsonSerializationOptions options)
    {
        BsonDefaults.GuidRepresentationMode = options.GuidRepresentationMode;

        if (options.Serializers?.Any() != true) return;

        foreach (var (type, serializer) in options.Serializers)
            try { BsonSerializer.RegisterSerializer(type, serializer); }
            catch { /* Ignore if the serializer was already registered  */ }
    }

    public IMongoDatabase Database { get; private set; }
}

public class MongoDBContext : BaseMongoDBContext<MongoDBContext>
{
    public MongoDBContext(MongoDBOptions<MongoDBContext> dbOptions, BsonSerializationOptions bsonSerializationOptions) : base(dbOptions, bsonSerializationOptions)
    {
    }
}