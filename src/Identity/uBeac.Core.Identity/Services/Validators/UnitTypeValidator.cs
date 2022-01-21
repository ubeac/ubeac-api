﻿using uBeac.Extensions;

namespace uBeac.Identity;

public class UnitTypeValidator<TKey, TUnitType> : IValidator<TUnitType>
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    protected readonly IUnitTypeRepository<TKey, TUnitType> Repository;

    public UnitTypeValidator(IUnitTypeRepository<TKey, TUnitType> repository)
    {
        Repository = repository;
    }

    public async Task<ValidationResult> ValidateAsync(TUnitType obj)
    {
        var errors = new List<ValidationError>();
        var identifiers = GetIdentifiers(obj);

        if (string.IsNullOrWhiteSpace(obj.Name)) errors.Add(new ValidationError(string.Empty, "Unit Type name is required."));
        if (string.IsNullOrWhiteSpace(obj.Code)) errors.Add(new ValidationError(string.Empty, "Unit Type code is required."));
        if (await Repository.Any(identifiers)) errors.Add(new ValidationError(string.Empty, "Unit Type code is already exists."));

        return errors.CreateResult();
    }

    public UnitTypeIdentifiers GetIdentifiers(TUnitType unitType) => unitType.GetIdentifiers<TKey, TUnitType>();
}

public class UnitTypeValidator<TUnitType> : UnitTypeValidator<Guid, TUnitType>
    where TUnitType : UnitType
{
    public UnitTypeValidator(IUnitTypeRepository<TUnitType> repository) : base(repository)
    {
    }
}