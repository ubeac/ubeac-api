using uBeac.Extensions;

namespace uBeac.Identity;

public class UnitRoleValidator<TUnitRoleKey, TUnitRole> : IValidator<TUnitRole>
    where TUnitRoleKey : IEquatable<TUnitRoleKey>
    where TUnitRole : UnitRole<TUnitRoleKey>
{
    protected readonly IUnitRoleRepository<TUnitRoleKey, TUnitRole> Repository;

    public UnitRoleValidator(IUnitRoleRepository<TUnitRoleKey, TUnitRole> repository)
    {
        Repository = repository;
    }

    public async Task<ValidationResult> ValidateAsync(TUnitRole obj)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(obj.UnitCode)) errors.Add(new ValidationError(string.Empty, "UnitCode is required."));
        if (string.IsNullOrWhiteSpace(obj.UnitType)) errors.Add(new ValidationError(string.Empty, "UnitType is required."));
        if (string.IsNullOrWhiteSpace(obj.UserName)) errors.Add(new ValidationError(string.Empty, "UserName is required."));
        if (string.IsNullOrWhiteSpace(obj.Role)) errors.Add(new ValidationError(string.Empty, "Role is required."));
        if (await Repository.Any(obj)) errors.Add(new ValidationError(string.Empty, "Unit Role is already exists."));

        return errors.CreateResult();
    }
}

public class UnitRoleValidator<TUnitRole> : UnitRoleValidator<Guid, TUnitRole>
    where TUnitRole : UnitRole
{
    public UnitRoleValidator(IUnitRoleRepository<TUnitRole> repository) : base(repository)
    {
    }
}