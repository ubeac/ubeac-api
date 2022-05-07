﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    [Fact]
    public async Task Should_Create_Entity_When_Call_Create_Method()
    {
        var entityService = new EntityService<TestEntity>(_entityRepositoryMock.Object);

        await entityService.Create(_testEntity, _validToken);

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Create(_testEntity, _validToken), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_Exception_And_Cancel_When_Call_Create_Method_With_CanceledToken()
    {
        var entityService = new EntityService<TestEntity>(_entityRepositoryMock.Object);

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await entityService.Create(_testEntity, _canceledToken));

        _entityRepositoryMock.Verify(entityRepository => entityRepository.Create(_testEntity, It.IsAny<CancellationToken>()), Times.Never);
    }
}