using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.FileManagement.MongoDB;

public class MongoFileRepository<TKey, TEntity, TContext> : MongoEntityRepository<TKey, TEntity, TContext>, IFileRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
    where TContext : IMongoDBContext
{
    public MongoFileRepository(TContext mongoDbContext, IApplicationContext applicationContext) : base(mongoDbContext, applicationContext)
    {
    }

    public async Task<IEnumerable<TEntity>> Search(SearchFileRequest<TKey> request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var filterBuilder = Builders<TEntity>.Filter;
        var filters = FilterDefinition<TEntity>.Empty;
        if (!string.IsNullOrWhiteSpace(request.Category)) filters &= filterBuilder.Eq(file => file.Category, request.Category);
        if (request.Ids?.Any() == true) filters &= filterBuilder.In(file => file.Id, request.Ids);
        if (request.Extensions?.Any() == true) filters &= filterBuilder.In(file => file.Extension, request.Extensions);
        if (request.Providers?.Any() == true) filters &= filterBuilder.In(file => file.Provider, request.Providers);

        var result = await Collection.FindAsync(filters, new FindOptions<TEntity>(), cancellationToken);
        return result.ToEnumerable(cancellationToken);
    }
}

public class MongoFileRepository<TEntity, TContext> : MongoFileRepository<Guid, TEntity, TContext>, IFileRepository<TEntity>
    where TEntity : IFileEntity
    where TContext : IMongoDBContext
{
    public MongoFileRepository(TContext mongoDbContext, IApplicationContext applicationContext) : base(mongoDbContext, applicationContext)
    {
    }
}