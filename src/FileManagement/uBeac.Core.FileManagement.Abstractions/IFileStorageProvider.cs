namespace uBeac.FileManagement;

public interface IFileStorageProvider
{
    string Name { get; }

    Task Create(Stream stream, string fileName, CancellationToken cancellationToken = default);
    Task<FileStream> Get(string fileName, CancellationToken cancellationToken = default);
}