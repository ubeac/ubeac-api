using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Driver;
using Moq;

namespace uBeac.Web.Logging.MongoDB;

public partial class HttpLoggingMongoDbRepositoryTests
{
    private readonly HttpLog _testLog;

    private readonly CancellationToken _validToken;
    private readonly CancellationToken _canceledToken;

    private readonly Mock<IMongoCollection<HttpLog>> _collection2xxMock;
    private readonly Mock<IMongoCollection<HttpLog>> _collection4xxMock;
    private readonly Mock<IMongoCollection<HttpLog>> _collection5xxMock;

    private readonly HttpLoggingMongoDbRepository _repository;

    public HttpLoggingMongoDbRepositoryTests()
    {
        _testLog = new HttpLog();

        _validToken = CancellationToken.None;
        _canceledToken = new CancellationToken(canceled: true);

        var options = new HttpLoggingMongoDbOptions();

        _collection2xxMock = new Mock<IMongoCollection<HttpLog>>();
        _collection4xxMock = new Mock<IMongoCollection<HttpLog>>();
        _collection5xxMock = new Mock<IMongoCollection<HttpLog>>();

        var database2XxMock = new Mock<IMongoDatabase>();
        database2XxMock.Setup(database2xx => database2xx.GetCollection<HttpLog>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_collection2xxMock.Object);

        var database4XxMock = new Mock<IMongoDatabase>();
        database4XxMock.Setup(database4xx => database4xx.GetCollection<HttpLog>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_collection4xxMock.Object);

        var database5XxMock = new Mock<IMongoDatabase>();
        database5XxMock.Setup(database5xx => database5xx.GetCollection<HttpLog>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_collection5xxMock.Object);

        var dbContextMock = new Mock<IHttpLoggingMongoDbContext>();
        dbContextMock.Setup(dbContext => dbContext.HttpLog2xxDatabase).Returns(database2XxMock.Object);
        dbContextMock.Setup(dbContext => dbContext.HttpLog4xxDatabase).Returns(database4XxMock.Object);
        dbContextMock.Setup(dbContext => dbContext.HttpLog5xxDatabase).Returns(database5XxMock.Object);

        _repository = new HttpLoggingMongoDbRepository(dbContextMock.Object, options);
    }

    private class StatusCode2xxTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            for (var i = 100; i < 400; i++) yield return new object[] { i };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private class StatusCode4xxTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            for (var i = 400; i < 500; i++) yield return new object[] { i };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private class StatusCode5xxTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            for (var i = 500; i < 600; i++) yield return new object[] { i };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}