using System;
using Xunit;

namespace uBeac.Web.Logging.MongoDB;

public class HttpLoggingMongoDbContextTests
{
    [Fact]
    public void Databases_Should_Not_Be_Null_When_Construction_With_Different_Connection_Strings()
    {
        // Different connection strings
        var options = new HttpLoggingMongoDbOptions
        {
            HttpLog2xxConnectionString = $"mongodb://localhost:27017/test-db{new Random().Next()}",
            HttpLog2xxCollectionName = "Http2xx",
            HttpLog4xxConnectionString = $"mongodb://localhost:27017/test-db{new Random().Next()}",
            HttpLog4xxCollectionName = "Http4xx",
            HttpLog5xxConnectionString = $"mongodb://localhost:27017/test-db{new Random().Next()}",
            HttpLog5xxCollectionName = "Http5xx"
        };

        var context = new HttpLoggingMongoDbContext(options);

        Assert.NotNull(context);
        Assert.NotNull(context.HttpLog2xxDatabase);
        Assert.NotNull(context.HttpLog4xxDatabase);
        Assert.NotNull(context.HttpLog5xxDatabase);
    }

    [Fact]
    public void Databases_Should_Not_Be_Null_When_Construction_With_Same_Connection_Strings()
    {
        // Same connection strings
        var connectionString = $"mongodb://localhost:27017/test-db{new Random().Next()}";
        var options = new HttpLoggingMongoDbOptions
        {
            HttpLog2xxConnectionString = connectionString,
            HttpLog4xxConnectionString = connectionString,
            HttpLog5xxConnectionString = connectionString
        };

        var context = new HttpLoggingMongoDbContext(options);

        Assert.NotNull(context);
        Assert.NotNull(context.HttpLog2xxDatabase);
        Assert.NotNull(context.HttpLog4xxDatabase);
        Assert.NotNull(context.HttpLog5xxDatabase);
    }
}