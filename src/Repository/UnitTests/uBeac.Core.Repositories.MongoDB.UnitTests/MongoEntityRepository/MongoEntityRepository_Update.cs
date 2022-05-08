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
    public async Task Update_ShouldCallsFindOneAndReplaceMethodOfMongoCollection()
    {
        await _entityRepository.Update(_testEntity, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<TestEntity>>(), _testEntity, It.IsAny<FindOneAndReplaceOptions<TestEntity>>(), _validToken), Times.Once);
    }

    [Fact]
    public async Task Update_ActionName_ShouldCallsFindOneAndReplaceMethodOfMongoCollection()
    {
        await _entityRepository.Update(_testEntity, _testActionName, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<TestEntity>>(), _testEntity, It.IsAny<FindOneAndReplaceOptions<TestEntity>>(), _validToken), Times.Once);
    }

    [Fact]
    public async Task Update_CanceledToken_ShouldThrowsExceptionAndCancelsCallingFindOneAndReplaceMethodOfCollection()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityRepository.Update(_testEntity, _canceledToken));

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<TestEntity>>(), _testEntity, It.IsAny<FindOneAndReplaceOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Update_AuditEntityPropertiesShouldNotBeNull()
    {
        await _entityRepository.Update(_testEntity, _testActionName, _validToken);

        var today = DateTime.Today;
        var applicationContext = _applicationContextMock.Object;
        var userName = applicationContext.UserName;
        var userIp = applicationContext.UserIp;

        Assert.Equal(today, _testEntity.LastUpdatedAt.Date);
        Assert.Equal(userName, _testEntity.LastUpdatedBy);
        Assert.Equal(userIp, _testEntity.LastUpdatedByIp);
        Assert.Equal(applicationContext, _testEntity.Context);
    }
}