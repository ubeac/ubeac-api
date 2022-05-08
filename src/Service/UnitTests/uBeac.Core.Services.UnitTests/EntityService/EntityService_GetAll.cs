using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task GetAll_EntitiesShouldFetchesFromGetAllMethodOfRepository()
    {
        var result = await _entityService.GetAll(_validToken);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(_testEntities, result);
    }

    [Fact]
    public async Task GetAll_CanceledToken_ShouldThrowsExceptionAndCancelsCallingGetAllMethodOfRepository()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityService.GetAll(_canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.GetAll(It.IsAny<CancellationToken>()), Times.Never);
    }
}