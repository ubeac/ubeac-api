namespace uBeac.FileManagement.LocalStorage;

public class LocalStorageFileProvider : IFileProvider
{
    protected readonly FileManagementLocalStorageOptions Options;
    protected readonly string DirPath;

    public LocalStorageFileProvider(FileManagementLocalStorageOptions options)
    {
        Options = options;

        DirPath = Path.Combine(Directory.GetCurrentDirectory(), Options.DirectoryPath);
        if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);
    }

    public string Name => nameof(LocalStorageFileProvider);

    public async Task Create(FileStream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        var path = GetFilePath(fileName);

        await using var createStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        await fileStream.CopyToAsync(createStream, cancellationToken);

    }

    public async Task<FileStream> Get(string fileName, CancellationToken cancellationToken = default)
    {
        var path = GetFilePath(fileName);

        await using var readStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return readStream;
    }

    protected string GetFilePath(string fileName) => Path.Combine(DirPath, fileName);
}