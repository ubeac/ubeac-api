﻿namespace uBeac;

public interface IAuditEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    string CreatedBy { get; set; } // UserName
    DateTime CreatedAt { get; set; }

    string LastUpdatedBy { get; set; } // UserName
    DateTime LastUpdatedAt { get; set; }
}

public interface IAuditEntity : IAuditEntity<Guid>, IEntity
{
}

public class AuditEntity<TKey> : IAuditEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }

    public string CreatedBy { get; set; } // UserName
    public DateTime CreatedAt { get; set; }

    public string LastUpdatedBy { get; set; } // UserName
    public DateTime LastUpdatedAt { get; set; }
}

public class AuditEntity : AuditEntity<Guid>, IAuditEntity
{
}
