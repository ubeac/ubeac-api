namespace uBeac.Identity;

public class DefaultOptions<TKey, TEntity> where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public List<TEntity> Values { get; set; } = new();
}

public class DefaultOptions<TEntity> : DefaultOptions<Guid, TEntity> where TEntity : IEntity
{
}