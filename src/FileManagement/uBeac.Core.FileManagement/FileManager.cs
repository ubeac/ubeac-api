namespace uBeac.FileManagement;

public class FileManager : IFileManager
{
    protected readonly List<IFileCategory> Categories;

    public FileManager(IEnumerable<IFileCategory> categories)
    {
        Categories = categories.ToList();
    }

    public async Task Create(FileStream fileStream, string category, CancellationToken cancellationToken = default)
    {
        var service = GetService(category);
        await service.Create(fileStream, category, cancellationToken);
    }

    public async Task Create<TKey, TEntity>(FileStream fileStream, string category, TEntity entity, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>
    {
        var service = GetService(category);
        if (service is not IFileService<TKey, TEntity> entityService) throw new Exception("No file service registered for your entity.");
        await entityService.Create(fileStream, category, entity, cancellationToken);
    }

    public async Task Create<TEntity>(FileStream fileStream, string category, TEntity entity, CancellationToken cancellationToken = default) where TEntity : IFileEntity
    {
        await Create<Guid, TEntity>(fileStream, category, entity, cancellationToken);
    }

    protected IFileService GetService(string category) => Categories.Single(c => c.CategoryName == category).Service;
}