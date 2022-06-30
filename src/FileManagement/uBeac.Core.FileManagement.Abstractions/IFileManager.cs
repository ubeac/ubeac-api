namespace uBeac.FileManagement;

public interface IFileManager
{
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