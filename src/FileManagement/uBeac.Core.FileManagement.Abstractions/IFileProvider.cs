namespace uBeac.FileManagement;

public interface IFileProvider
{
    string Name { get; }

    Task<CreateFileResult> Create(FileStream fileStream, string fileName, CancellationToken cancellationToken = default);
    Task<FileStream> Get(string fileName, CancellationToken cancellationToken = default);
}

public class CreateFileResult
{
    public string FilePath { get; set; }
}