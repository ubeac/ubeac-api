using System;
using System.Threading.Tasks;
using Xunit;

namespace uBeac.Repositories.MongoDB;

public partial class MongoEntityRepositoryTests
{
    [Fact]
    public async Task Should_Fetch_Entities_When_Call_GetByIds()
    {
        var result = await _entityRepository.GetByIds(_testIds, _validToken);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(_testEntities, result);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Call_GetByIds_Method_With_CanceledToken()
    {
        var repository = new MongoEntityRepository<TestEntity, IMongoDBContext>(_mongoDbContextMock.Object, _applicationContextMock.Object);

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await repository.GetByIds(_testIds, _canceledToken));
    }
}