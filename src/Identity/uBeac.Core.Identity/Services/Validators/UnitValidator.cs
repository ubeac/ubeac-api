using uBeac.Extensions;

namespace uBeac.Identity;

public class UnitValidator<TKey, TUnit> : IValidator<TUnit>
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    protected readonly IUnitRepository<TKey, TUnit> Repository;

    public UnitValidator(IUnitRepository<TKey, TUnit> repository)
    {
        Repository = repository;
    }

    public async Task<ValidationResult> ValidateAsync(TUnit obj)
    {
        var errors = new List<ValidationError>();
        var identifiers = GetIdentifiers(obj);

        if (string.IsNullOrWhiteSpace(obj.Name)) errors.Add(new ValidationError(string.Empty, "Unit name is required."));
        if (string.IsNullOrWhiteSpace(obj.Code)) errors.Add(new ValidationError(string.Empty, "Unit code is required."));
        if (string.IsNullOrWhiteSpace(obj.Type)) errors.Add(new ValidationError(string.Empty, "Unit type is required."));
        if (await Repository.Any(identifiers)) errors.Add(new ValidationError(string.Empty, "Unit code/type is already exists."));

        return errors.CreateResult();
    }

    public UnitIdentifiers GetIdentifiers(TUnit unit) => unit.GetIdentifiers<TKey, TUnit>();
}

public class UnitValidator<TUnit> : UnitValidator<Guid, TUnit>
    where TUnit : Unit
{
    public UnitValidator(IUnitRepository<TUnit> repository) : base(repository)
    {
    }
}