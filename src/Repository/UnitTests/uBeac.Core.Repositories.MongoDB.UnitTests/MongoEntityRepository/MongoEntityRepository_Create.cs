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
    public async Task Should_Insert_When_Call_Create_Method()
    {
        await _entityRepository.Create(_testEntity, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.InsertOneAsync(_testEntity, It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Insert_When_Call_Create_Method_With_ActionName()
    {
        await _entityRepository.Create(_testEntity, _testActionName, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.InsertOneAsync(_testEntity, It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_Exception_And_Cancel_When_Call_Create_Method_With_CanceledToken()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityRepository.Create(_testEntity, _canceledToken));

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.InsertOneAsync(_testEntity, It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Should_Set_Audit_Properties_When_Call_Create_Method()
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