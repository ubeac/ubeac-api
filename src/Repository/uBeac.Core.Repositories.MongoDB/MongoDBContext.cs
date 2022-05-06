using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace uBeac.Repositories.MongoDB;

public interface IMongoDBContext
{
    IMongoDatabase Database { get; }
}

public class MongoDBContext : IMongoDBContext
{
    public MongoDBContext(MongoDBOptions dbOptions, BsonSerializationOptions bsonSerializationOptions)
    {
        ConfigureBsonSerialization(bsonSerializationOptions);
        ConfigureDatabase(dbOptions);
    }

    private void ConfigureDatabase(MongoDBOptions options)
    {
        var mongoUrl = new MongoUrl(options.ConnectionString);
        var client = new MongoClient(mongoUrl);
        Database = client.GetDatabase(mongoUrl.DatabaseName);
    }

    private void ConfigureBsonSerialization(BsonSerializationOptions options)
    {
        BsonDefaults.GuidRepresentationMode = options.GuidRepresentationMode;

        try
        {
            if (options.Serializers?.Any() == true)
                foreach (var (type, serializer) in options.Serializers)
                    if (BsonSerializer.LookupSerializer(type) == null)
                        BsonSerializer.RegisterSerializer(type, serializer);
        }
        catch
        {
            // ignored
        }
    }

    public IMongoDatabase Database { get; private set; }
}

