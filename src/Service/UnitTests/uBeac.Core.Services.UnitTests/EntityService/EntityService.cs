using System;
using System.Collections.Generic;
using System.Threading;
using Moq;
using uBeac.Repositories;

namespace uBeac.Services;

public partial class EntityServiceTests
{
    private readonly TestEntity _testEntity;
    private readonly IEnumerable<TestEntity> _testEntities;
    private readonly Guid _testId;
    private readonly IEnumerable<Guid> _testIds;

    private readonly CancellationToken _validToken;
    private readonly CancellationToken _canceledToken;

    private readonly Mock<IEntityRepository<TestEntity>> _entityRepositoryMock;

    public EntityServiceTests()
    {
        _testEntity = new TestEntity { Id = _testId, FirstName = "Test FirstName", LastName = "Test LastName" };
        _testEntities = new List<TestEntity> { _testEntity };
        _testId = Guid.NewGuid();
        _testIds = new List<Guid> { _testId };

        _validToken = CancellationToken.None;
        _canceledToken = new CancellationToken(canceled: true);

        _entityRepositoryMock = new Mock<IEntityRepository<TestEntity>>();
        _entityRepositoryMock.Setup(entityRepository => entityRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(_testEntities);
        _entityRepositoryMock.Setup(entityRepository => entityRepository.GetById(_testId, It.IsAny<CancellationToken>())).ReturnsAsync(_testEntity);
        _entityRepositoryMock.Setup(entityRepository => entityRepository.GetByIds(_testIds, It.IsAny<CancellationToken>())).ReturnsAsync(_testEntities);
    }
}

public class TestEntity : AuditEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}