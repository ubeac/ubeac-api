using System.Threading.Tasks;
using Xunit;

namespace uBeac.Repositories.MongoDB;

public partial class MongoEntityRepositoryTests
{
    [Fact]
    public async Task Should_Fetch_Entity_When_Called_GetById()
    {
        var repository = new MongoEntityRepository<TestEntity, IMongoDBContext>(_mongoDbContextMock.Object, _applicationContextMock.Object);

        var result = await repository.GetById(_testId, _validToken);

        Assert.NotNull(result);
        Assert.Equal(_testId, result.Id);
    }
}