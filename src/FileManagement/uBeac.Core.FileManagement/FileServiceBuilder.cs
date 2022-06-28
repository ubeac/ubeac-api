using uBeac;
using uBeac.FileManagement;

namespace Microsoft.Extensions.DependencyInjection;

public class FileServiceBuilder<TKey, TEntity> : IFileServiceBuilder<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>, new()
{
    public List<Func<IServiceProvider, IFileValidator>> Validators { get; } = new();
    public Func<IServiceProvider, IFileRepository<TKey, TEntity>> Repository { get; set; }
    public Func<IServiceProvider, IFileProvider> Provider { get; set; }

    public void AddValidator(IFileValidator validator)
    {
        Validators.Add(_ => validator);
    }

    internal IFileService<TKey, TEntity> Build(IServiceProvider serviceProvider)
    {
        var validators = Validators.Select(validator => validator(serviceProvider));
        var repositories = Repository(serviceProvider);
        var providers = Provider(serviceProvider);

        return ActivatorUtilities.CreateInstance<FileService<TKey, TEntity>>(serviceProvider, repositories, providers, validators);
    }
}