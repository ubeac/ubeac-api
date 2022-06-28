namespace uBeac.FileManagement.LocalStorage;

public class LocalDiskFileProvider : IFileProvider
{
    protected readonly FileManagementLocalDiskOptions Options;
    protected readonly string DirPath;

    public LocalDiskFileProvider(FileManagementLocalDiskOptions options)
    {
        Options = options;

        DirPath = Path.Combine(Directory.GetCurrentDirectory(), Options.DirectoryPath);
        if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);
    }

    public string Name => nameof(LocalDiskFileProvider);

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