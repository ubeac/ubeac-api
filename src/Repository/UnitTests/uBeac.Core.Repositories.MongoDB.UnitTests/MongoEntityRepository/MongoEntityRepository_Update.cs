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
    public async Task Should_Replace_When_Call_Update_Method()
    {
        await _entityRepository.Update(_testEntity, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<TestEntity>>(), _testEntity, It.IsAny<FindOneAndReplaceOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Replace_When_Call_Update_Method_With_ActionName()
    {
        await _entityRepository.Update(_testEntity, _testActionName, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<TestEntity>>(), _testEntity, It.IsAny<FindOneAndReplaceOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_Exception_And_Cancel_When_Call_Update_Method_With_CanceledToken()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityRepository.Update(_testEntity, _canceledToken));

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<TestEntity>>(), _testEntity, It.IsAny<FindOneAndReplaceOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Should_Set_Audit_Properties_When_Call_Update_Method()
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