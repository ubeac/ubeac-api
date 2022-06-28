namespace uBeac.FileManagement;

public interface IFileManager
{
    Task Create(FileStream fileStream, string category, CancellationToken cancellationToken = default);
    Task Create<TKey, TEntity>(FileStream fileStream, string category, TEntity entity, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>;
    Task Create<TEntity>(FileStream fileStream, string category, TEntity entity, CancellationToken cancellationToken = default) where TEntity : IFileEntity;
}