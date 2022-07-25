namespace uBeac.FileManagement;

public class FileModel : IDisposable, IAsyncDisposable
{
    public Stream Stream { get; set; }
    public string Extension { get; set; }
    public string Category { get; set; }

    public void Dispose() => Stream.Dispose();

    public async ValueTask DisposeAsync() => await Stream.DisposeAsync();
}