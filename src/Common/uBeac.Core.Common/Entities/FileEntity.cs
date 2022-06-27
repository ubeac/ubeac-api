namespace uBeac;

public interface IFileEntity<TKey> : IAuditEntity<TKey>
    where TKey : IEquatable<TKey>
{
    string Name { get; set; }
    string Extension { get; set; }
    string Provider { get; set; }
    string Path { get; set; }
    string Category { get; set; }
}

public interface IFileEntity : IFileEntity<Guid>, IAuditEntity
{
}

public class FileEntity<TKey> : AuditEntity<TKey>, IFileEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public string Provider { get; set; }
    public string Path { get; set; }
    public string Category { get; set; }
}

public class FileEntity : FileEntity<Guid>, IFileEntity
{
}