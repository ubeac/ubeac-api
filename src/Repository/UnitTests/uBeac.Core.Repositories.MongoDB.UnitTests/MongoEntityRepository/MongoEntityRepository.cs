using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Moq;
using uBeac.Repositories.History;

namespace uBeac.Repositories.MongoDB;

public partial class MongoEntityRepositoryTests
{
    private readonly Guid _testEntityId;
    private readonly IEnumerable<Guid> _testEntityIds;

    private readonly TestEntity _testEntity;
    private readonly IEnumerable<TestEntity> _testEntities;

    private readonly string _testActionName;

    private readonly CancellationToken _validToken;
    private readonly CancellationToken _canceledToken;

    private readonly Mock<IMongoCollection<TestEntity>> _mongoCollectionMock;
    private readonly Mock<IApplicationContext> _applicationContextMock;

    private readonly MongoEntityRepository<TestEntity, IMongoDBContext> _entityRepository;

    public MongoEntityRepositoryTests()
    {
        _testEntityId = Guid.NewGuid();
        _testEntityIds = new List<Guid> { _testEntityId };

        _testEntity = new TestEntity { Id = _testEntityId, FirstName = "Test FirstName", LastName = "Test Lastname" };
        _testEntities = new List<TestEntity> { _testEntity };

        _testActionName = "Test ActionName";

        _validToken = CancellationToken.None;
        _canceledToken = new CancellationToken(canceled: true);

        var asyncCursorMock = new Mock<IAsyncCursor<TestEntity>>();
        asyncCursorMock.Setup(asyncCursor => asyncCursor.Current).Returns(_testEntities);
        asyncCursorMock.SetupSequence(asyncCursor => asyncCursor.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
        asyncCursorMock.SetupSequence(asyncCursor => asyncCursor.MoveNextAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true)).Returns(Task.FromResult(false));

        _mongoCollectionMock = new Mock<IMongoCollection<TestEntity>>();
        _mongoCollectionMock.Setup(mongoCollection => mongoCollection.FindAsync(It.IsAny<FilterDefinition<TestEntity>>(), It.IsAny<FindOptions<TestEntity>>(), It.IsAny<CancellationToken>())).ReturnsAsync(asyncCursorMock.Object);

        var mongoDatabaseMock = new Mock<IMongoDatabase>();
        mongoDatabaseMock.Setup(mongoDatabase => mongoDatabase.GetCollection<TestEntity>(It.IsAny<string>(), null)).Returns(_mongoCollectionMock.Object);

        var mongoDbContextMock = new Mock<IMongoDBContext>();
        mongoDbContextMock.Setup(context => context.Database).Returns(mongoDatabaseMock.Object);

        _applicationContextMock = new Mock<IApplicationContext>();
        _applicationContextMock.Setup(applicationContext => applicationContext.UserName).Returns("TestUserName");
        _applicationContextMock.Setup(applicationContext => applicationContext.UserIp).Returns("127.0.0.1");

        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(serviceProvider => serviceProvider.GetService(typeof(RegisteredTypesForHistory))).Returns(new RegisteredTypesForHistory());

        var historyFactoryMock = new Mock<HistoryFactory>(serviceProviderMock.Object);

        _entityRepository = new MongoEntityRepository<TestEntity, IMongoDBContext>(mongoDbContextMock.Object, _applicationContextMock.Object, historyFactoryMock.Object);
    }
}

public class TestEntity : AuditEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}