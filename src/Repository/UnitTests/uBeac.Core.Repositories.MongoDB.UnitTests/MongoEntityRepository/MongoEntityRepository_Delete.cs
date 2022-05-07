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
    public async Task Should_Delete_When_Called_Delete_Method()
    {
        var repository = new MongoEntityRepository<TestEntity, IMongoDBContext>(_mongoDbContextMock.Object, _applicationContextMock.Object);

        await repository.Delete(_testId, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndDeleteAsync(It.IsAny<FilterDefinition<TestEntity>>(), It.IsAny<FindOneAndDeleteOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Delete_When_Called_Delete_Method_WithActionName()
    {
        var repository = new MongoEntityRepository<TestEntity, IMongoDBContext>(_mongoDbContextMock.Object, _applicationContextMock.Object);

        await repository.Delete(_testId, _testActionName, _validToken);

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndDeleteAsync(It.IsAny<FilterDefinition<TestEntity>>(), It.IsAny<FindOneAndDeleteOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_Exception_And_Cancel_When_Called_Delete_Method_With_CanceledToken()
    {
        var repository = new MongoEntityRepository<TestEntity, IMongoDBContext>(_mongoDbContextMock.Object, _applicationContextMock.Object);

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await repository.Delete(_testId, _canceledToken));

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindOneAndDeleteAsync(It.IsAny<FilterDefinition<TestEntity>>(), It.IsAny<FindOneAndDeleteOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}