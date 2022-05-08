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
    public async Task GetById_EntityShouldFetchesFromFindMethodOfMongoCollection()
    {
        var result = await _entityRepository.GetById(_testEntityId, _validToken);

        Assert.NotNull(result);
        Assert.Equal(_testEntityId, result.Id);
    }

    [Fact]
    public async Task GetById_CanceledToken_ShouldThrowsExceptionAndCancelsCallingFindMethodOfMongoCollection()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityRepository.GetById(_testEntityId, _canceledToken));

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindAsync(It.IsAny<FilterDefinition<TestEntity>>(), It.IsAny<FindOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}