namespace uBeac.FileManagement;


public class SearchFileRequest<TKey> where TKey : IEquatable<TKey>
{
    public string Category { get; set; }
    public IEnumerable<TKey> Ids { get; set; }
    public IEnumerable<string> Names { get; set; }
    public IEnumerable<string> Extensions { get; set; }
    public IEnumerable<string> Providers { get; set; }
}

public class SearchFileRequest : SearchFileRequest<Guid>
{
}