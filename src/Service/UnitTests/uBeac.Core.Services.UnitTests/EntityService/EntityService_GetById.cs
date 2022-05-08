using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task GetById_EntityShouldFetchesFromGetByIdMethodOfRepository()
    {
        var result = await _entityService.GetById(_testEntityId, _validToken);

        Assert.NotNull(result);
        Assert.Equal(_testEntity, result);
    }

    [Fact]
    public async Task GetById_CanceledToken_ShouldThrowsExceptionAndCancelsCallingGetByIdMethodOfRepository()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityService.GetById(_testEntityId, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.GetById(_testEntityId, It.IsAny<CancellationToken>()), Times.Never);
    }
}