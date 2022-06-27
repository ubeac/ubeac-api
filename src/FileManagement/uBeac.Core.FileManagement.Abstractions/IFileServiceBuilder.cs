namespace uBeac.FileManagement;

public interface IFileServiceBuilder<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    List<Func<IServiceProvider, IFileValidator>> Validators { get; }
    Func<IServiceProvider, IFileRepository<TKey, TEntity>> Repository { get; set; }
    Func<IServiceProvider, IFileProvider> Provider { get; set; }

    void AddValidator(IFileValidator validator);
}