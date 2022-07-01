using uBeac.Repositories;

namespace uBeac.FileManagement;

public interface IFileRepository<TKey, TEntity> : IEntityRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    Task<IEnumerable<TEntity>> Search(SearchFileRequest<TKey> request, CancellationToken cancellationToken = default);
}

public interface IFileRepository<TEntity> : IFileRepository<Guid, TEntity>, IEntityRepository<TEntity>
    where TEntity : IFileEntity
{
}