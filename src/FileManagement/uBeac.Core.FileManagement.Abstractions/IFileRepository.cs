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

public class SearchFileRequest<TKey> where TKey : IEquatable<TKey>
{
    public string Category { get; set; }
    public IEnumerable<TKey> Ids { get; set; }
    public IEnumerable<string> Names { get; set; }
    public IEnumerable<string> Extensions { get; set; }
    public IEnumerable<string> Providers { get; set; }
}

public class SearchFileRequest : SearchFileRequest<Guid>
{
}