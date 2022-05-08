using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task Should_Fetch_Entity_From_Repository_When_Call_GetById_Method()
    {
        var result = await _entityService.GetById(_testId, _validToken);

        Assert.NotNull(result);
        Assert.Equal(_testEntity, result);
    }

    [Fact]
    public async Task Should_Throw_Exception_And_Cancel_When_Call_GetById_Method_With_CanceledToken()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _entityService.GetById(_testId, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.GetById(_testId, It.IsAny<CancellationToken>()), Times.Never);
    }
}