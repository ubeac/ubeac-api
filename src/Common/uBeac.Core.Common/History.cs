namespace uBeac;

public interface IHistory<TKey> : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    object Data { get; set; }
    DateTime CreatedAt { get; set; }
    object Context { get; set; }
}

public interface IHistory : IEntity, IHistory<Guid>
{
}

public class History<TKey> : Entity<TKey>, IHistory<TKey>
    where TKey : IEquatable<TKey>
{
    public virtual object Data { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual object Context { get; set; }
}

public class History : History<Guid>, IHistory
{
}