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

    public async Task<FileModel> Get(GetFileRequest request, CancellationToken cancellationToken = default)
    {
        var entity = (await Search(new SearchFileRequest
        {
            Category = request.Category,
            Names = new[] { request.Name }
        }, cancellationToken)).First();

        return new FileModel
        {
            Stream = await Provider.Get(entity.Name, cancellationToken),
            Category = request.Category,
            Extension = entity.Extension
        };
    }

    public async Task Create(FileModel model, TEntity entity, CancellationToken cancellationToken = default)
    {
        ThrowExceptionIfNotValid(model);

        var fileName = Path.GetRandomFileName();

        entity.Name = fileName;
        entity.Extension = model.Extension;
        entity.Provider = Provider.Name;
        entity.Category = model.Category;

        await Provider.Create(model.Stream, fileName, cancellationToken);
        await Repository.Create(entity, cancellationToken);
    }

    public async Task Create(FileModel model, CancellationToken cancellationToken = default)
    {
        await Create(model, new TEntity(), cancellationToken);
    }

    protected void ThrowExceptionIfNotValid(FileModel model) => Validators.ForEach(validator =>
    {
        var result = validator.Validate(model);
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