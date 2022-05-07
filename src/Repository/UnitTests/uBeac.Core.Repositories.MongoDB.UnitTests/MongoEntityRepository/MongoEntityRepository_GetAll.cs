using System;
using System.Threading.Tasks;
using Xunit;

namespace uBeac.Repositories.MongoDB;

public partial class MongoEntityRepositoryTests
{
    [Fact]
    public async Task Should_Fetch_Entities_When_Call_GetAll()
    {
        var result = await _entityRepository.GetAll(_validToken);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(_testEntities, result);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Call_GetAll_Method_With_CanceledToken()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityRepository.GetAll(_canceledToken));
    }
}