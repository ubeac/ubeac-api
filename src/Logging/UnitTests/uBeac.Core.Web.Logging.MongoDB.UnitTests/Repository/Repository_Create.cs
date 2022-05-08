using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace uBeac.Web.Logging.MongoDB;

public partial class HttpLoggingMongoDbRepositoryTests
{
    [Fact]
    public async Task Create_CanceledToken_ShouldThrowsExceptionAndCancelCallingInsertOneMethodOfMongoCollection()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _repository.Create(_testLog, _canceledToken));

        _collection2xxMock.Verify(collection => collection.InsertOneAsync(It.IsAny<HttpLog>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Never);
        _collection4xxMock.Verify(collection => collection.InsertOneAsync(It.IsAny<HttpLog>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Never);
        _collection5xxMock.Verify(collection => collection.InsertOneAsync(It.IsAny<HttpLog>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [ClassData(typeof(StatusCode2xxTestData))]
    public async Task Create_ShouldCallsInsertOneMethodOfMongoCollection2xx(int statusCode)
    {
        _testLog.StatusCode = statusCode;

        await _repository.Create(_testLog, _validToken);

        _collection2xxMock.Verify(collection => collection.InsertOneAsync(_testLog, It.IsAny<InsertOneOptions>(), _validToken), Times.Once);
        _collection4xxMock.Verify(collection => collection.InsertOneAsync(_testLog, It.IsAny<InsertOneOptions>(), _validToken), Times.Never);
        _collection5xxMock.Verify(collection => collection.InsertOneAsync(_testLog, It.IsAny<InsertOneOptions>(), _validToken), Times.Never);
    }

    [Theory]
    [ClassData(typeof(StatusCode4xxTestData))]
    public async Task Create_ShouldCallsInsertOneMethodOfMongoCollection4xx(int statusCode)
    {
        _testLog.StatusCode = statusCode;

        await _repository.Create(_testLog, _validToken);

        _collection4xxMock.Verify(collection => collection.InsertOneAsync(_testLog, It.IsAny<InsertOneOptions>(), _validToken), Times.Once);
        _collection2xxMock.Verify(collection => collection.InsertOneAsync(_testLog, It.IsAny<InsertOneOptions>(), _validToken), Times.Never);
        _collection5xxMock.Verify(collection => collection.InsertOneAsync(_testLog, It.IsAny<InsertOneOptions>(), _validToken), Times.Never);
    }

    [Theory]
    [ClassData(typeof(StatusCode5xxTestData))]
    public async Task Create_ShouldCallsInsertOneMethodOfMongoCollection5xx(int statusCode)
    {
        _testLog.StatusCode = statusCode;

        await _repository.Create(_testLog, _validToken);

        _collection5xxMock.Verify(collection => collection.InsertOneAsync(_testLog, It.IsAny<InsertOneOptions>(), _validToken), Times.Once);
        _collection4xxMock.Verify(collection => collection.InsertOneAsync(_testLog, It.IsAny<InsertOneOptions>(), _validToken), Times.Never);
        _collection2xxMock.Verify(collection => collection.InsertOneAsync(_testLog, It.IsAny<InsertOneOptions>(), _validToken), Times.Never);
    }
}