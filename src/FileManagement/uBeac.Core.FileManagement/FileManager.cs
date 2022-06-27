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

    protected IFileService GetService(string category) => Categories.Single(c => c.CategoryName == category).Service;
}