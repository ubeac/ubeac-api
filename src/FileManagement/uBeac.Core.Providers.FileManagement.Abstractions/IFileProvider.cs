namespace uBeac.Providers.FileManagement;

public interface IFileProvider
{
    Task Create(FileStream fileStream, string fileName, CancellationToken cancellationToken = default);
    Task<FileStream> Get(string fileName, CancellationToken cancellationToken = default);
}