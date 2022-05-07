using System;
using System.Collections.Generic;
using Xunit;

namespace uBeac.Repositories.MongoDB;

public class MongoDBContextTests
{
    [Fact]
    public void Database_Should_Not_Be_Null_After_Context_Construction()
    {
        var mongoDbOptions = new MongoDBOptions($"mongodb://localhost:27017/test-db{new Random().Next()}");
        var bsonSerializationOptions = new BsonSerializationOptions();

        var context = new MongoDBContext(mongoDbOptions, bsonSerializationOptions);

        Assert.NotNull(context);
        Assert.NotNull(context.Database);
    }
}