namespace uBeac.Identity;

public interface IOptions<TKey, TEntity> where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>
{ 
    IEnumerable<TEntity>? DefaultValues { get; set; }
}

public interface IOptions<TEntity> : IOptions<Guid, TEntity> where TEntity : IEntity
{
}