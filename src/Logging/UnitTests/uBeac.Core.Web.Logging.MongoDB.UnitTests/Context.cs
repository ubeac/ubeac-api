using Xunit;

namespace uBeac.Web.Logging.MongoDB;

public class HttpLoggingMongoDbContextTests
{
    private const string ConnectionString = "mongodb://localhost:27017/test-db";

    [Fact]
    public void Constructor_MultipleConnectionStrings_DatabasesShouldNotBeNull()
    {
        var options = new HttpLoggingMongoDbOptions
        {
            HttpLog2xxConnectionString = $"{ConnectionString}-2xx",
            HttpLog4xxConnectionString = $"{ConnectionString}-4xx",
            HttpLog5xxConnectionString = $"{ConnectionString}-5xx",
        };

        var context = new HttpLoggingMongoDbContext(options);

        Assert.NotNull(context);
        Assert.NotNull(context.HttpLog2xxDatabase);
        Assert.NotNull(context.HttpLog4xxDatabase);
        Assert.NotNull(context.HttpLog5xxDatabase);
    }

    [Fact]
    public void Constructor_OneConnectionString_DatabasesShouldNotBeNull()
    {
        var options = new HttpLoggingMongoDbOptions
        {
            HttpLog2xxConnectionString = ConnectionString,
            HttpLog4xxConnectionString = ConnectionString,
            HttpLog5xxConnectionString = ConnectionString
        };

        var context = new HttpLoggingMongoDbContext(options);

        Assert.NotNull(context);
        Assert.NotNull(context.HttpLog2xxDatabase);
        Assert.NotNull(context.HttpLog4xxDatabase);
        Assert.NotNull(context.HttpLog5xxDatabase);
    }
}