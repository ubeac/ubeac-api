﻿namespace uBeac.Identity;

public class Unit<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual string Type { get; set; }
    public virtual string Description { get; set; }
    public virtual TKey ParentUnitId { get; set; }

    private Unit<TKey> _parent;

    public void SetParentUnit(Unit<TKey> parent)
    {
        _parent = parent;
    }

    public Unit<TKey> GetParentUnit()
    {
        return _parent;
    }
}

public class Unit : Unit<Guid>, IEntity
{
}