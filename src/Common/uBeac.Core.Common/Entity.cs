namespace uBeac;

public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}

public interface IEntity : IEntity<Guid>
{
}

public interface IAuditEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public string CreatedBy { get; set; } // UserName
    public DateTime CreatedAt { get; set; }
    public string LastUpdatedBy { get; set; } // UserName
    public DateTime LastUpdatedAt { get; set; }
}

public interface IAuditEntity : IAuditEntity<Guid>, IEntity
{
}

public class Entity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
}

public class Entity : Entity<Guid>, IEntity
{
}

public class AuditEntity<TKey> : IAuditEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

public class AuditEntity : AuditEntity<Guid>, IAuditEntity
{
}