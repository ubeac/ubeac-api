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
    private readonly Guid _testEntityId;
    private readonly IEnumerable<Guid> _testEntityIds;

    private readonly CancellationToken _validToken;
    private readonly CancellationToken _canceledToken;

    private readonly Mock<IEntityRepository<TestEntity>> _entityRepositoryMock;

    private readonly IEntityService<TestEntity> _entityService;

    public EntityServiceTests()
    {
        _testEntity = new TestEntity { Id = _testEntityId, FirstName = "Test FirstName", LastName = "Test LastName" };
        _testEntities = new List<TestEntity> { _testEntity };
        _testEntityId = Guid.NewGuid();
        _testEntityIds = new List<Guid> { _testEntityId };

        _validToken = CancellationToken.None;
        _canceledToken = new CancellationToken(canceled: true);

        _entityRepositoryMock = new Mock<IEntityRepository<TestEntity>>();
        _entityRepositoryMock.Setup(entityRepository => entityRepository.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(_testEntities);
        _entityRepositoryMock.Setup(entityRepository => entityRepository.GetById(_testEntityId, It.IsAny<CancellationToken>())).ReturnsAsync(_testEntity);
        _entityRepositoryMock.Setup(entityRepository => entityRepository.GetByIds(_testEntityIds, It.IsAny<CancellationToken>())).ReturnsAsync(_testEntities);

        _entityService = new EntityService<TestEntity>(_entityRepositoryMock.Object);
    }
}

public class TestEntity : AuditEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}