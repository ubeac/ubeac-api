using Xunit;

namespace uBeac.Repositories.MongoDB;

public class MongoDBContextTests
{
    private const string ConnectionString = "mongodb://localhost:27017/test-db";

    private readonly MongoDBContext _mongoDbContext;

    public MongoDBContextTests()
    {
        var mongoDbOptions = new MongoDBOptions(ConnectionString);
        var bsonSerializationOptions = new BsonSerializationOptions();

        _mongoDbContext = new MongoDBContext(mongoDbOptions, bsonSerializationOptions);
    }

    [Fact]
    public void Constructor_DatabaseShouldNotBeNull()
    {
        Assert.NotNull(_mongoDbContext);
        Assert.NotNull(_mongoDbContext.Database);
    }
}