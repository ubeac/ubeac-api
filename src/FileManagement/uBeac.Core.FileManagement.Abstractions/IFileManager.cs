namespace uBeac.FileManagement;

public interface IFileManager
{
    Task Create(FileStream fileStream, string category, CancellationToken cancellationToken = default);
}