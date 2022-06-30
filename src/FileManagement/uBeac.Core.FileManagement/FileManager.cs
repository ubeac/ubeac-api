﻿namespace uBeac.FileManagement;

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

    public async Task Create(CreateFileRequest request, CancellationToken cancellationToken = default)
    {
        var service = GetService(request.Category);
        await service.Create(request, cancellationToken);
    }

    public async Task Create<TKey, TEntity>(CreateFileRequest request, TEntity entity, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey> where TEntity : IFileEntity<TKey>
    {
        var service = GetService(request.Category);
        if (service is not IFileService<TKey, TEntity> entityService) throw new Exception("No file service registered for your entity.");
        await entityService.Create(request, entity, cancellationToken);
    }

    public async Task Create<TEntity>(CreateFileRequest request, TEntity entity, CancellationToken cancellationToken = default) where TEntity : IFileEntity
    {
        await Create<Guid, TEntity>(request, entity, cancellationToken);
    }

    protected IFileService GetService(string category) => Categories.Single(c => c.CategoryName == category).Service;
}