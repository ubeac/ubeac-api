using uBeac.Services;

namespace uBeac.FileManagement;

public interface IFileService : IService
{
    Task Create(FileStream fileStream, string category, CancellationToken cancellationToken = default);
}

public interface IFileService<TKey, TEntity> : IFileService
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    Task Create(FileStream fileStream, string category, TEntity entity, CancellationToken cancellationToken = default);
}

public interface IFileService<TEntity> : IFileService<Guid, TEntity>
    where TEntity : IFileEntity
{
}