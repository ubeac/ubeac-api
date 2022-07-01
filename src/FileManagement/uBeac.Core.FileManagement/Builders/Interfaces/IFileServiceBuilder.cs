namespace uBeac.FileManagement;

public interface IFileServiceBuilder<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    IFileServiceBuilder<TKey, TEntity> AddValidator(IFileValidator validator);

    IFileServiceBuilder<TKey, TEntity> StoreInfoIn<TRepository>() where TRepository : IFileRepository<TKey, TEntity>;
    IFileServiceBuilder<TKey, TEntity> StoreInfoIn<TRepository>(TRepository repository) where TRepository : IFileRepository<TKey, TEntity>;
    IFileServiceBuilder<TKey, TEntity> StoreInfoIn(Func<IServiceProvider, IFileRepository<TKey, TEntity>> builder);

    IFileServiceBuilder<TKey, TEntity> StoreFilesIn<TStorageProvider>() where TStorageProvider : IFileStorageProvider;
    IFileServiceBuilder<TKey, TEntity> StoreFilesIn<TStorageProvider>(TStorageProvider provider) where TStorageProvider : IFileStorageProvider;
    IFileServiceBuilder<TKey, TEntity> StoreFilesIn(Func<IServiceProvider, IFileStorageProvider> builder);
}