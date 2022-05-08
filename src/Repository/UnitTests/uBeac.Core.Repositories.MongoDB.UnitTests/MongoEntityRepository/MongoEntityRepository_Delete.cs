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
    public async Task Delete_ShouldCallsFindOneAndDeleteMethodOfMongoCollection()
    {
        await _entityRepository.Delete(_testEntityId, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndDeleteAsync(It.IsAny<FilterDefinition<TestEntity>>(), It.IsAny<FindOneAndDeleteOptions<TestEntity>>(), _validToken), Times.Once);
    }

    [Fact]
    public async Task Delete_ActionName_ShouldCallsFindOneAndDeleteMethodOfMongoCollection()
    {
        await _entityRepository.Delete(_testEntityId, _testActionName, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndDeleteAsync(It.IsAny<FilterDefinition<TestEntity>>(), It.IsAny<FindOneAndDeleteOptions<TestEntity>>(), _validToken), Times.Once);
    }

    [Fact]
    public async Task Delete_CanceledToken_ShouldThrowsExceptionAndCancelsCallingFindOneAndDeleteMethodOfMongoCollection()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityRepository.Delete(_testEntityId, _canceledToken));

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndDeleteAsync(It.IsAny<FilterDefinition<TestEntity>>(), It.IsAny<FindOneAndDeleteOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}