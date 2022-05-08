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
    public async Task GetAll_EntitiesShouldFetchesFromFindMethodOfMongoCollection()
    {
        var result = await _entityRepository.GetAll(_validToken);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(_testEntities, result);
    }

    [Fact]
    public async Task GetAll_CanceledToken_ShouldThrowsExceptionAndCancelsCallingFindMethodOfMongoCollection()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityRepository.GetAll(_canceledToken));

        _mongoCollectionMock.Verify(mongoCollection => mongoCollection.FindAsync(It.IsAny<FilterDefinition<TestEntity>>(), It.IsAny<FindOptions<TestEntity>>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}