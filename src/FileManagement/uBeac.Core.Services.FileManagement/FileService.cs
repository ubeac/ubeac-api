using uBeac.Providers.FileManagement;
using uBeac.Repositories.FileManagement;

namespace uBeac.Services.FileManagement;

public class FileService<TKey, TEntity> : IFileService<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    protected readonly IFileRepository<TKey, TEntity> Repository;
    protected readonly IFileProvider Provider;

    public FileService(IFileRepository<TKey, TEntity> repository, IFileProvider provider)
    {
        Repository = repository;
        Provider = provider;
    }

    public async Task Create(FileStream fileStream, CancellationToken cancellationToken = default)
    {
        var fileName = GetRandomFileName();
        var fileExtension = GetFileExtension(fileStream);
        var entity = CreateEntity(fileStream, fileName, fileExtension);

        await Provider.Create(fileStream, fileName, cancellationToken);
        await Repository.Create(entity, cancellationToken);
    }

    protected virtual TEntity CreateEntity(FileStream fileStream, string fileName, string fileExtension)
    {
        var entity = Activator.CreateInstance<TEntity>();
        entity.Name = fileName;
        entity.Extension = fileExtension;
        entity.Provider = Provider.Name;
        return entity;
    }

    protected virtual string GetRandomFileName() => Path.GetRandomFileName();

    protected virtual string GetFileExtension(FileStream fileStream) => Path.GetExtension(fileStream.Name);
}

public class FileService<TEntity> : FileService<Guid, TEntity>, IFileService<TEntity>
    where TEntity : IFileEntity
{
    public FileService(IFileRepository<TEntity> repository, IFileProvider provider) : base(repository, provider)
    {
    }
}