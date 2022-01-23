namespace uBeac.Identity;

public class DefaultOptions<TKey, TEntity> where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public List<TEntity> Values { get; set; } = new();
}
public class DefaultOptions<TEntity> : DefaultOptions<Guid, TEntity> where TEntity : IEntity
{
}

public class DefaultUnitTypes<TUnitTypeKey, TUnitType> : DefaultOptions<TUnitTypeKey, TUnitType>
    where TUnitType : UnitType<TUnitTypeKey>
    where TUnitTypeKey : IEquatable<TUnitTypeKey>
{
}

public class DefaultUnitTypes<TUnitType> : DefaultUnitTypes<Guid, TUnitType>
    where TUnitType : UnitType
{
}

