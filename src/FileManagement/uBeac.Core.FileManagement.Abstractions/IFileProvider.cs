namespace uBeac.FileManagement;

public interface IFileProvider
{
    string Name { get; }

    Task Create(Stream stream, string fileName, CancellationToken cancellationToken = default);
    Task<FileStream> Get(string fileName, CancellationToken cancellationToken = default);
}