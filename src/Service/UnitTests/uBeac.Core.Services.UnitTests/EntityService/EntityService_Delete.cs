using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task Should_Delete_Entity_When_Call_Delete_Method()
    {
        var entityService = new EntityService<TestEntity>(_entityRepositoryMock.Object);

        await entityService.Delete(_testId, _validToken);

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Delete(_testId, _validToken), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_Exception_And_Cancel_When_Call_Delete_Method_With_CanceledToken()
    {
        var entityService = new EntityService<TestEntity>(_entityRepositoryMock.Object);

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await entityService.Delete(_testId, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Delete(_testId, It.IsAny<CancellationToken>()), Times.Never);
    }
}