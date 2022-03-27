namespace uBeac;

public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}

public interface IEntity : IEntity<Guid>
{
}

public class Entity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
}

public class Entity : Entity<Guid>, IEntity
{
}
