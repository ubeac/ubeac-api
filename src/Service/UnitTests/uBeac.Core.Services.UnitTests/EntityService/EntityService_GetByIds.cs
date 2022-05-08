using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task GetByIds_EntitiesShouldFetchesFromGetByIdsMethodOfRepository()
    {
        var result = await _entityService.GetByIds(_testEntityIds, _validToken);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(_testEntities, result);
    }

    [Fact]
    public async Task GetByIds_CanceledToken_ShouldThrowsExceptionAndCancelsCallingGetByIdsMethodOfRepository()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityService.GetByIds(_testEntityIds, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.GetByIds(_testEntityIds, It.IsAny<CancellationToken>()), Times.Never);
    }
}