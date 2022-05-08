using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task Delete_ShouldCallsDeleteMethodOfRepository()
    {
        await _entityService.Delete(_testEntityId, _validToken);

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Delete(_testEntityId, _validToken), Times.Once);
    }

    [Fact]
    public async Task Delete_CanceledToken_ShouldThrowsExceptionAndCancelsCallingDeleteMethodOfRepository()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityService.Delete(_testEntityId, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Delete(_testEntityId, It.IsAny<CancellationToken>()), Times.Never);
    }
}