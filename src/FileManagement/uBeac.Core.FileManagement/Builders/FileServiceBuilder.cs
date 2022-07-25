using uBeac;
using uBeac.FileManagement;

namespace Microsoft.Extensions.DependencyInjection;

public class FileServiceBuilder<TKey, TEntity> : IFileServiceBuilder<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>, new()
{
    public List<Func<IServiceProvider, IFileValidator>> Validators { get; } = new();
    public Func<IServiceProvider, IFileRepository<TKey, TEntity>> Repository { get; set; }
    public Func<IServiceProvider, IFileStorageProvider> Provider { get; set; }

    public IFileServiceBuilder<TKey, TEntity> AddValidator(IFileValidator validator)
    {
        Validators.Add(_ => validator);
        return this;
    }

    public IFileServiceBuilder<TKey, TEntity> StoreInfoIn<TRepository>() where TRepository : IFileRepository<TKey, TEntity>
    {
        Repository = serviceProvider => serviceProvider.GetRequiredService<TRepository>();
        return this;
    }

    public IFileServiceBuilder<TKey, TEntity> StoreInfoIn<TRepository>(TRepository repository) where TRepository : IFileRepository<TKey, TEntity>
    {
        Repository = _ => repository;
        return this;
    }

    public IFileServiceBuilder<TKey, TEntity> StoreInfoIn(Func<IServiceProvider, IFileRepository<TKey, TEntity>> builder)
    {
        Repository = builder;
        return this;
    }

    public IFileServiceBuilder<TKey, TEntity> StoreFilesIn<TStorageProvider>() where TStorageProvider : IFileStorageProvider
    {
        Provider = serviceProvider => serviceProvider.GetRequiredService<TStorageProvider>();
        return this;
    }

    public IFileServiceBuilder<TKey, TEntity> StoreFilesIn<TStorageProvider>(TStorageProvider provider) where TStorageProvider : IFileStorageProvider
    {
        Provider = _ => provider;
        return this;
    }

    public IFileServiceBuilder<TKey, TEntity> StoreFilesIn(Func<IServiceProvider, IFileStorageProvider> builder)
    {
        Provider = builder;
        return this;
    }

    internal IFileService<TKey, TEntity> Build(IServiceProvider serviceProvider)
    {
        var validators = Validators.Select(validator => validator(serviceProvider));
        var repository = Repository(serviceProvider);
        var provider = Provider(serviceProvider);

        return new FileService<TKey, TEntity>(validators, repository, provider);
    }
}