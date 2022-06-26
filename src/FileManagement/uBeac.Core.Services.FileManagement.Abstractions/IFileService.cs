namespace uBeac.Services.FileManagement;

public interface IFileService<TKey, TEntity> : IService
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    Task Create(FileStream fileStream, CancellationToken cancellationToken = default);
}

public interface IFileService<TEntity> : IFileService<Guid, TEntity>
    where TEntity : IFileEntity
{
}