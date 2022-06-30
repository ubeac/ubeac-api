using uBeac.Services;

namespace uBeac.FileManagement;

public interface IFileService : IService
{
    Task Create(CreateFileRequest request, CancellationToken cancellationToken = default);
}

public interface IFileService<TKey, TEntity> : IFileService
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    Task Create(CreateFileRequest request, TEntity entity, CancellationToken cancellationToken = default);
}

public interface IFileService<TEntity> : IFileService<Guid, TEntity>
    where TEntity : IFileEntity
{
}