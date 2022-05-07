using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task Should_Fetch_Entities_From_Repository_When_Call_GetByIds_Method()
    {
        var entityService = new EntityService<TestEntity>(_entityRepositoryMock.Object);

        var result = await entityService.GetByIds(_testIds, _validToken);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(_testEntities, result);
    }

    [Fact]
    public async Task Should_Throw_Exception_And_Cancel_When_Call_GetByIds_Method_With_CanceledToken()
    {
        var entityService = new EntityService<TestEntity>(_entityRepositoryMock.Object);

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await entityService.GetByIds(_testIds, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.GetByIds(_testIds, It.IsAny<CancellationToken>()), Times.Never);
    }
}