namespace uBeac;

public interface IHistoryEntity<TKey> : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    object Data { get; set; }
    string ActionName { get; set; }
    DateTime CreatedAt { get; set; }
    IApplicationContext Context { get; set; }
}

public interface IHistoryEntity : IEntity, IHistoryEntity<Guid>
{
}

public class HistoryEntity<TKey> : Entity<TKey>, IHistoryEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public virtual object Data { get; set; }
    public virtual string ActionName { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual IApplicationContext Context { get; set; }
}

public class HistoryEntity : HistoryEntity<Guid>, IHistoryEntity
{
}