namespace uBeac;

public interface IAuditEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    string CreatedBy { get; set; } // UserName
    string CreatedByIp { get; set; } // IpAddress
    DateTime CreatedAt { get; set; }

    string LastUpdatedBy { get; set; } // UserName
    string LastUpdatedByIp { get; set; } // IpAddress
    DateTime LastUpdatedAt { get; set; }

    IApplicationContext Context { get; set; }
}

public interface IAuditEntity : IAuditEntity<Guid>, IEntity
{
}

public class AuditEntity<TKey> : IAuditEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }

    public string CreatedBy { get; set; } // UserName
    public string CreatedByIp { get; set; } // IpAddress
    public DateTime CreatedAt { get; set; }

    public string LastUpdatedBy { get; set; } // UserName
    public string LastUpdatedByIp { get; set; } // IpAddress
    public DateTime LastUpdatedAt { get; set; }

    public IApplicationContext Context { get; set; }
}

public class AuditEntity : AuditEntity<Guid>, IAuditEntity
{
}
