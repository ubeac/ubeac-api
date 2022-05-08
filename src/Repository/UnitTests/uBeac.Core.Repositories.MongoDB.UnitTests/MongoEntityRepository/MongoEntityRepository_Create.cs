using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace uBeac.Repositories.MongoDB;

public partial class MongoEntityRepositoryTests
{
    [Fact]
    public async Task Create_ShouldCallsInsertMethodOfMongoCollection()
    {
        await _entityRepository.Create(_testEntity, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.InsertOneAsync(_testEntity, It.IsAny<InsertOneOptions>(), _validToken), Times.Once);
    }

    [Fact]
    public async Task Create_ActionName_ShouldCallsInsertMethodOfMongoCollection()
    {
        await _entityRepository.Create(_testEntity, _testActionName, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.InsertOneAsync(_testEntity, It.IsAny<InsertOneOptions>(), _validToken), Times.Once);
    }

    [Fact]
    public async Task Create_CanceledToken_ShouldThrowsExceptionAndCancelsCallingInsertMethodOfCollection()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityRepository.Create(_testEntity, _canceledToken));

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.InsertOneAsync(_testEntity, It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Create_AuditEntityPropertiesShouldNotBeNull()
    {
        await _entityRepository.Create(_testEntity, _testActionName, _validToken);

        var today = DateTime.Today;
        var applicationContext = _applicationContextMock.Object;
        var userName = applicationContext.UserName;
        var userIp = applicationContext.UserIp;

        Assert.Equal(today, _testEntity.CreatedAt.Date);
        Assert.Equal(userName, _testEntity.CreatedBy);
        Assert.Equal(userIp, _testEntity.CreatedByIp);
        Assert.Equal(today, _testEntity.LastUpdatedAt.Date);
        Assert.Equal(userName, _testEntity.LastUpdatedBy);
        Assert.Equal(userIp, _testEntity.LastUpdatedByIp);
        Assert.Equal(applicationContext, _testEntity.Context);
    }
}