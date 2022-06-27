namespace uBeac.FileManagement.LocalStorage;

public class LocalStorageFileProvider : IFileProvider
{
    protected readonly FileManagementLocalStorageOptions Options;

    public LocalStorageFileProvider(FileManagementLocalStorageOptions options)
    {
        Options = options;
    }

    public string Name => nameof(LocalStorageFileProvider);

    public async Task<CreateFileResult> Create(FileStream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        var path = GetFinalPath(fileName);
        await using var createStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        await fileStream.CopyToAsync(createStream, cancellationToken);
        return new CreateFileResult { FilePath = path };
    }

    public async Task<FileStream> Get(string fileName, CancellationToken cancellationToken = default)
    {
        var path = GetFinalPath(fileName);
        await using var readStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return readStream;
    }

    protected string GetFinalPath(string fileName) => Path.Combine(Options.DirectoryPath, fileName);
}