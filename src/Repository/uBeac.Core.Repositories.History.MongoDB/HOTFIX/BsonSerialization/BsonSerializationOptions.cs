using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace uBeac.Repositories.MongoDB;

public class BsonSerializationOptions
{
    public GuidRepresentationMode GuidRepresentationMode { get; set; }
    public IDictionary<Type, IBsonSerializer> Serializers { get; set; }
}