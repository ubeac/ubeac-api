using System;
using System.Threading.Tasks;
using Xunit;

namespace uBeac.Repositories.MongoDB;

public partial class MongoEntityRepositoryTests
{
    [Fact]
    public async Task Should_Fetch_Entities_When_Called_GetByIds()
    {
        var repository = new MongoEntityRepository<TestEntity, IMongoDBContext>(_mongoDbContextMock.Object, _applicationContextMock.Object);

        var result = await repository.GetByIds(_testIds, _validToken);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(_testEntities, result);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Called_GetByIds_Method_With_CanceledToken()
    {
        var repository = new MongoEntityRepository<TestEntity, IMongoDBContext>(_mongoDbContextMock.Object, _applicationContextMock.Object);

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await repository.GetByIds(_testIds, _canceledToken));
    }
}