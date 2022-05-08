using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task Update_ShouldCallsUpdateMethodOfRepository()
    {
        await _entityService.Update(_testEntity, _validToken);

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Update(_testEntity, _validToken), Times.Once);
    }

    [Fact]
    public async Task Update_CanceledToken_ShouldThrowsExceptionAndCancelsCallingUpdateMethodOfRepository()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityService.Update(_testEntity, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Update(_testEntity, It.IsAny<CancellationToken>()), Times.Never);
    }
}