﻿using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace uBeac.Repositories.MongoDB;

public class AppContextBsonSerializer : SerializerBase<IApplicationContext>
{
    protected readonly Type Type = typeof(ApplicationContextModel);

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, IApplicationContext value)
    {
        if (value == null)
        {
            context.Writer.WriteNull();
            return;
        }

        BsonSerializer.Serialize(context.Writer, Type, value);
    }

    public override IApplicationContext Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return BsonSerializer.Deserialize(context.Reader, Type) as IApplicationContext;
    }
}