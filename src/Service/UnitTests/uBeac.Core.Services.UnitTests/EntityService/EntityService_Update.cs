using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task Should_Update_Entity_When_Call_Update_Method()
    {
        var entityService = new EntityService<TestEntity>(_entityRepositoryMock.Object);

        await entityService.Update(_testEntity, _validToken);

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Update(_testEntity, _validToken), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_Exception_And_Cancel_When_Call_Update_Method_With_CanceledToken()
    {
        var entityService = new EntityService<TestEntity>(_entityRepositoryMock.Object);

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await entityService.Update(_testEntity, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Update(_testEntity, It.IsAny<CancellationToken>()), Times.Never);
    }
}