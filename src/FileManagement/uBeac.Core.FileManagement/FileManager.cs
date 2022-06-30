namespace uBeac.FileManagement;

public class FileManager : IFileManager
{
    protected readonly List<IFileCategory> Categories;

    public FileManager(IEnumerable<IFileCategory> categories)
    {
        Categories = categories.ToList();
    }

    public async Task<IEnumerable<IFileEntity>> Search(SearchFileRequest request, CancellationToken cancellationToken = default)
    {
        var service = GetService(request.Category);
        return await service.Search(request, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> Search<TKey, TEntity>(SearchFileRequest<TKey> request, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>
    {
        var service = GetService(request.Category);
        if (service is not IFileService<TKey, TEntity> entityService) throw new Exception("No file service registered for your entity.");
        return await entityService.Search(request, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> Search<TEntity>(SearchFileRequest request, CancellationToken cancellationToken = default) where TEntity : IFileEntity
    {
        return await Search<Guid, TEntity>(request, cancellationToken);
    }

    public async Task Create(FileModel model, CancellationToken cancellationToken = default)
    {
        var service = GetService(model.Category);
        await service.Create(model, cancellationToken);
    }

    public async Task Create<TKey, TEntity>(FileModel model, TEntity entity, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>
    {
        var service = GetService(model.Category);
        if (service is not IFileService<TKey, TEntity> entityService) throw new Exception("No file service registered for your entity.");
        await entityService.Create(model, entity, cancellationToken);
    }

    public async Task Create<TEntity>(FileModel model, TEntity entity, CancellationToken cancellationToken = default) where TEntity : IFileEntity
    {
        await Create<Guid, TEntity>(model, entity, cancellationToken);
    }

    public async Task<FileModel> Get(GetFileRequest request, CancellationToken cancellationToken = default)
    {
        var service = GetService(request.Category);
        return await service.Get(request, cancellationToken);
    }

    protected IFileService GetService(string category) => Categories.Single(c => c.CategoryName == category).Service;
}