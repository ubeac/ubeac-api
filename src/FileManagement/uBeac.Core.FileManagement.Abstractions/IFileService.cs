using uBeac.Services;

namespace uBeac.FileManagement;

public interface IFileService : IService
{
    Task<IEnumerable<IFileEntity>> Search(SearchFileRequest request, CancellationToken cancellationToken = default);

    Task Create(CreateFileRequest request, CancellationToken cancellationToken = default);
}

public interface IFileService<TKey, TEntity> : IFileService
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    Task<IEnumerable<TEntity>> Search(SearchFileRequest<TKey> request, CancellationToken cancellationToken = default);

    Task Create(CreateFileRequest request, TEntity entity, CancellationToken cancellationToken = default);
}

public interface IFileService<TEntity> : IFileService<Guid, TEntity>
    where TEntity : IFileEntity
{
}