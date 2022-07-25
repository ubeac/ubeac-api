namespace uBeac.FileManagement;

public interface IFileManager
{
    Task<IEnumerable<IFileEntity>> Search(SearchFileRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> Search<TKey, TEntity>(SearchFileRequest<TKey> request, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>;
    Task<IEnumerable<TEntity>> Search<TEntity>(SearchFileRequest request, CancellationToken cancellationToken = default) where TEntity : IFileEntity;

    Task<IFileEntity> Create(FileModel model, CancellationToken cancellationToken = default);
    Task<TEntity> Create<TKey, TEntity>(FileModel model, TEntity entity, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>;
    Task<TEntity> Create<TEntity>(FileModel model, TEntity entity, CancellationToken cancellationToken = default) where TEntity : IFileEntity;

    Task<FileModel> Get(GetFileRequest request, CancellationToken cancellationToken = default);
}