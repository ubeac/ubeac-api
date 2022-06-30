namespace uBeac.FileManagement;

public class FileService<TKey, TEntity> : IFileService<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>, new()
{
    protected readonly List<IFileValidator> Validators;
    protected readonly IFileRepository<TKey, TEntity> Repository;
    protected readonly IFileProvider Provider;

    public FileService(IEnumerable<IFileValidator> validators, IFileRepository<TKey, TEntity> repository, IFileProvider provider)
    {
        Validators = validators.ToList();
        Repository = repository;
        Provider = provider;
    }

    public async Task<IEnumerable<TEntity>> Search(SearchFileRequest<TKey> request, CancellationToken cancellationToken = default)
        => await Repository.Search(request, cancellationToken);

    public async Task<IEnumerable<IFileEntity>> Search(SearchFileRequest request, CancellationToken cancellationToken = default)
        => (IEnumerable<IFileEntity>) await Search(request as SearchFileRequest<TKey>, cancellationToken);

    public async Task Create(CreateFileRequest request, TEntity entity, CancellationToken cancellationToken = default)
    {
        ThrowExceptionIfNotValid(request);

        var fileName = Path.GetRandomFileName();

        entity.Name = fileName;
        entity.Extension = request.Extension;
        entity.Provider = Provider.Name;
        entity.Category = request.Category;

        await Provider.Create(request.Stream, fileName, cancellationToken);
        await Repository.Create(entity, cancellationToken);
    }

    public async Task Create(CreateFileRequest request, CancellationToken cancellationToken = default)
    {
        await Create(request, new TEntity(), cancellationToken);
    }

    protected void ThrowExceptionIfNotValid(CreateFileRequest request) => Validators.ForEach(validator =>
    {
        var result = validator.Validate(request);
        if (!result.Validated) throw result.Exception;
    });
}

public class FileService<TEntity> : FileService<Guid, TEntity>, IFileService<TEntity>
    where TEntity : IFileEntity, new()
{
    public FileService(IEnumerable<IFileValidator> validators, IFileRepository<TEntity> repository, IFileProvider provider) : base(validators, repository, provider)
    {
    }
}