using uBeac.Extensions;

namespace uBeac.Identity.Validators;

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

        if (string.IsNullOrWhiteSpace(obj.Name)) errors.Add(new ValidationError(string.Empty, "Unit name is required."));
        if (string.IsNullOrWhiteSpace(obj.Code)) errors.Add(new ValidationError(string.Empty, "Unit code is required."));
        if (string.IsNullOrWhiteSpace(obj.Type)) errors.Add(new ValidationError(string.Empty, "Unit type is required."));
        if (await Repository.Any(obj.Code, obj.Type)) errors.Add(new ValidationError(string.Empty, "Unit code/type is already exists."));

        return errors.CreateResult();
    }
}

public class UnitValidator<TUnit> : UnitValidator<Guid, TUnit>
    where TUnit : Unit
{
    public UnitValidator(IUnitRepository<TUnit> repository) : base(repository)
    {
    }
}