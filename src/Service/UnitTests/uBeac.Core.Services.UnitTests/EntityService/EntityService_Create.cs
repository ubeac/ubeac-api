using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task Create_ShouldCallsCreateMethodOfRepository()
    {
        await _entityService.Create(_testEntity, _validToken);

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Create(_testEntity, _validToken), Times.Once);
    }

    [Fact]
    public async Task Create_CanceledToken_ShouldThrowsExceptionAndCancelsCallingCreateMethodOfRepository()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityService.Create(_testEntity, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Create(_testEntity, It.IsAny<CancellationToken>()), Times.Never);
    }
}