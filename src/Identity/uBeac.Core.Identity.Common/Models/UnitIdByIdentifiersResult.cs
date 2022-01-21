﻿namespace uBeac.Identity;

public class UnitIdByIdentifiersResult<TKey> where TKey : IEquatable<TKey>
{
    public UnitIdByIdentifiersResult(TKey id, UnitIdentifiers identifiers)
    {
        Id = id;
        Identifiers = identifiers;
    }

    public TKey Id { get; set; }
    public UnitIdentifiers Identifiers { get; set; }
}

public class UnitIdByIdentifiersResult : UnitIdByIdentifiersResult<Guid>
{
    public UnitIdByIdentifiersResult(Guid id, UnitIdentifiers identifiers) : base(id, identifiers)
    {
    }
}