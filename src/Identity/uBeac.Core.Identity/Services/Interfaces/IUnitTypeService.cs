﻿using uBeac.Services;

namespace uBeac.Identity;

public interface IUnitTypeService<TKey, TUnitType> : IEntityService<TKey, TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    Task<bool> Exists(string code, CancellationToken cancellationToken = default);
    Task Insert(InsertUnitType unit, CancellationToken cancellationToken = default);
    Task Replace(ReplaceUnitType<TKey> unit, CancellationToken cancellationToken = default);
}

public interface IUnitTypeService<TUnitType> : IUnitTypeService<Guid, TUnitType>
    where TUnitType : UnitType
{
}