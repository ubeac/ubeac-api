using System.Threading.Tasks;
using Xunit;

namespace uBeac.Repositories.MongoDB;

public partial class MongoEntityRepositoryTests
{
    [Fact]
    public async Task Should_Fetch_Entity_When_Call_GetById()
    {
        var result = await _entityRepository.GetById(_testId, _validToken);

        Assert.NotNull(result);
        Assert.Equal(_testId, result.Id);
    }
}