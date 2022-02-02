﻿using uBeac.Services;

namespace uBeac.Identity;

public interface IUnitService<TKey, TUnit> : IEntityService<TKey, TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    Task Insert(InsertUnit unit, CancellationToken cancellationToken = default);
    Task Replace(ReplaceUnit<TKey> unit, CancellationToken cancellationToken = default);
    Task<bool> Exists(string code, string type, CancellationToken cancellationToken = default);
}

public interface IUnitService<TUnit> : IUnitService<Guid, TUnit>
    where TUnit : Unit
{
}