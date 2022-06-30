namespace uBeac.FileManagement;

public interface IFileManager
{
    Task<IEnumerable<IFileEntity>> Search(SearchFileRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> Search<TKey, TEntity>(SearchFileRequest<TKey> request, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>;
    Task<IEnumerable<TEntity>> Search<TEntity>(SearchFileRequest request, CancellationToken cancellationToken = default) where TEntity : IFileEntity;

    Task Create(FileModel model, CancellationToken cancellationToken = default);
    Task Create<TKey, TEntity>(FileModel model, TEntity entity, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>;
    Task Create<TEntity>(FileModel model, TEntity entity, CancellationToken cancellationToken = default) where TEntity : IFileEntity;

    Task<FileModel> Get(GetFileRequest request, CancellationToken cancellationToken = default);
}

public class GetFileRequest
{
    public string Name { get; set; }
    public string Category { get; set; }
}

public class FileModel : IDisposable, IAsyncDisposable
{
    public Stream Stream { get; set; }
    public string Extension { get; set; }
    public string Category { get; set; }

    public void Dispose() => Stream.Dispose();

    public async ValueTask DisposeAsync() => await Stream.DisposeAsync();
}