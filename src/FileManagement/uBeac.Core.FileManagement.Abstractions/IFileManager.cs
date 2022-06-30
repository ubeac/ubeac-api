namespace uBeac.FileManagement;

public interface IFileManager
{
    Task<IEnumerable<IFileEntity>> Search(SearchFileRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> Search<TKey, TEntity>(SearchFileRequest<TKey> request, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>;
    Task<IEnumerable<TEntity>> Search<TEntity>(SearchFileRequest request, CancellationToken cancellationToken = default) where TEntity : IFileEntity;

    Task Create(CreateFileRequest request, CancellationToken cancellationToken = default);
    Task Create<TKey, TEntity>(CreateFileRequest request, TEntity entity, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>;
    Task Create<TEntity>(CreateFileRequest request, TEntity entity, CancellationToken cancellationToken = default) where TEntity : IFileEntity;
}

public class CreateFileRequest
{
    public Stream Stream { get; set; }
    public string Extension { get; set; }
    public string Category { get; set; }
}