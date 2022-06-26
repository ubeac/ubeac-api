namespace uBeac.Providers.FileManagement.LocalStorage;

public class LocalStorageFileProvider : IFileProvider
{
    protected readonly LocalStorageOptions Options;

    public LocalStorageFileProvider(LocalStorageOptions options)
    {
        Options = options;
    }

    public string Name => nameof(LocalStorageFileProvider);

    public async Task Create(FileStream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        var path = GetFinalPath(fileName);
        await using var createStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        await fileStream.CopyToAsync(createStream, cancellationToken);
    }

    public async Task<FileStream> Get(string fileName, CancellationToken cancellationToken = default)
    {
        var path = GetFinalPath(fileName);
        await using var readStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return readStream;
    }

    protected string GetFinalPath(string fileName) => Path.Combine(Options.DirectoryPath, fileName);
}