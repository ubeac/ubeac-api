using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;

namespace uBeac.Repositories.MongoDB;

public class AppContextSerializer : SerializerBase<IApplicationContext>
{
    protected Type Type;

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, IApplicationContext value)
    {
        Type ??= value.GetType();
        var json = JsonConvert.SerializeObject(value);
        context.Writer.WriteString(json);
    }

    public override IApplicationContext Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var json = context.Reader.ReadString();
        return JsonConvert.DeserializeObject(json, Type) as IApplicationContext;
    }
}