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

    public async Task Create(FileStream fileStream, string category, TEntity entity, CancellationToken cancellationToken = default)
    {
        ThrowExceptionIfNotValid(fileStream);

        var fileName = Path.GetRandomFileName();
        var fileExtension = Path.GetExtension(fileStream.Name);

        entity.Name = fileName;
        entity.Extension = fileExtension;
        entity.Provider = Provider.Name;
        entity.Category = category;

        await Provider.Create(fileStream, fileName, cancellationToken);
        await Repository.Create(entity, cancellationToken);
    }

    public async Task Create(FileStream fileStream, string category, CancellationToken cancellationToken = default)
    {
        await Create(fileStream, category, new TEntity(), cancellationToken);
    }

    protected void ThrowExceptionIfNotValid(FileStream fileStream) => Validators.ForEach(validator =>
    {
        var result = validator.Validate(fileStream);
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